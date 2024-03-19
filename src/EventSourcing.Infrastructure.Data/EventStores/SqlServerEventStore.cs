using EventSourcing.Domain.Core.Common;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace EventSourcing.Infrastructure.Data.EventStores
{
    public class SqlServerEventStore : IEventStore
    {
        private readonly IMediator _mediator;
        private readonly string _connectionString;

        public SqlServerEventStore(IMediator mediator,
            IConfiguration configuration)
        {
            _mediator = mediator;   
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Event> GetEvents(Guid aggregateId)
        {
            var events = new List<Event>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT Class, Dll, Data FROM Event WHERE AggregateId = @AggregateId ORDER BY Timestamp DESC";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AggregateId", aggregateId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var eventType = Type.GetType($"{reader.GetString(0)},{reader.GetString(1)}");
                            var eventData = JsonConvert.DeserializeObject(reader.GetString(2), eventType);

                            events.Add((Event)eventData);
                        }
                    }
                }
            }

            return events;
        }

        public void SaveEvents(Guid aggregateId, IEnumerable<Event> events)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                foreach (var @event in events)
                {
                    var type = @event.GetType();
                    var assembly = type.Assembly.Location;

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = "INSERT INTO Event (AggregateId, Class, Dll, Event, Data, Timestamp) VALUES (@AggregateId, @Class, @Dll, @Event, @Data, @Timestamp)";                        

                        command.Parameters.AddWithValue("@AggregateId", aggregateId);                        
                        command.Parameters.AddWithValue("@Class", type.FullName);
                        command.Parameters.AddWithValue("@Dll", Path.GetFileNameWithoutExtension(assembly));
                        command.Parameters.AddWithValue("@Event", type.Name);
                        command.Parameters.AddWithValue("@Data", JsonConvert.SerializeObject(@event));
                        command.Parameters.AddWithValue("@Timestamp", @event.Timestamp);

                        command.ExecuteNonQuery();
                    }

                    PublishEvent(@event);
                }
            }
        }

        private void PublishEvent(Event @event)
        {
            _mediator.Publish(@event);
        }
    }
}
