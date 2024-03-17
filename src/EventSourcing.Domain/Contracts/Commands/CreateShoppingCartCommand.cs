using EventSourcing.Domain.AggregateModels.ShoppingCartAggregate;
using MediatR;

namespace EventSourcing.Domain.Contracts.Commands
{
    public class CreateShoppingCartCommand : IRequest<ShoppingCart>
    {
        public string CustomerName { get; private set; }
        public List<string> Items { get; private set; }

        public CreateShoppingCartCommand(string customerName, List<string> items)
        {
            CustomerName = customerName;
            Items = items;
        }
    }
}
