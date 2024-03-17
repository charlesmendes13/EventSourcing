using EventSourcing.Application.EventHandlers;
using EventSourcing.Domain.AggregateModels;
using EventSourcing.Domain.Contracts.Events;
using EventSourcing.Domain.Core.Common;
using EventSourcing.Infrastructure.Data.EventStores;
using EventSourcing.Infrastructure.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace EventSourcing.Infrastructure.Ioc
{
    public class InjectorDependency
    {
        public static void Register(IServiceCollection services)
        {
            // Application

            services.AddTransient<IEventHandler<ShoppingCartCreatedEvent>, ShoppingCartCreatedEventHandler>();
            services.AddTransient<IEventHandler<ItemAddedEvent>, ItemAddedEventHandler>();

            // Infrastructure

            services.AddTransient<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddTransient<IEventStore, InMemoryEventStore>();
        }
    }
}
