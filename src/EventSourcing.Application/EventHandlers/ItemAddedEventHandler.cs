﻿using EventSourcing.Domain.Contracts.Events;
using EventSourcing.Domain.Core.Common;

namespace EventSourcing.Application.EventHandlers
{
    public class ItemAddedEventHandler 
        : IEventHandler<ItemAddedEvent>
    {
        public Task Handle(ItemAddedEvent @event)
        {
            // TODO aqui é onde será realizada a integração por AMPQ (RabbitMQ) para a base de dados de leitura.   
            return Task.CompletedTask;
        }
    }
}