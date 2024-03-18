using EventSourcing.Application.CommandHandlers;
using EventSourcing.Application.EventHandlers;
using EventSourcing.Application.QueryHandlers;
using EventSourcing.Domain.AggregateModels.ShoppingCartAggregate;
using EventSourcing.Domain.Contracts.Commands;
using EventSourcing.Domain.Contracts.Events;
using EventSourcing.Domain.Contracts.Queries;
using EventSourcing.Domain.Core.Common;
using EventSourcing.Infrastructure.Data.EventStores;
using EventSourcing.Infrastructure.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace EventSourcing.Infrastructure.IoC
{
    public class InjectorDependency
    {
        public static void Register(IServiceCollection container)
        {
            // MediatR

            container.AddMediatR(o => o.RegisterServicesFromAssembly(typeof(InjectorDependency).Assembly));

            // Application

            container.AddTransient<IRequestHandler<GetShoppingCartQuery, ShoppingCart>, GetShoppingCartQueryHandler>();
            container.AddTransient<IRequestHandler<CreateShoppingCartCommand, ShoppingCart>, CreateShoppingCartCommandHandler>();

            container.AddScoped<INotificationHandler<ShoppingCartCreatedEvent>, ShoppingCartCreatedEventHandler>();
            container.AddScoped<INotificationHandler<ItemAddedEvent>, ItemAddedEventHandler>();

            // Infrastructure

            container.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            container.AddTransient<IEventStore, EventStore>();
        }
    }
}
