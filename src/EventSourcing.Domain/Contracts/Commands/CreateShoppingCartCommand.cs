using EventSourcing.Domain.AggregateModels.ShoppingCartAggregate;
using MediatR;

namespace EventSourcing.Domain.Contracts.Commands
{
    public class CreateShoppingCartCommand : IRequest<ShoppingCart>
    {
        public string CustomerName { get; private set; }

        public CreateShoppingCartCommand(string customerName)
        {
            CustomerName = customerName;
        }
    }
}
