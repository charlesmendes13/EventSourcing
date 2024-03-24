using EventSourcing.Domain.AggregateModels.ShoppingCartAggregate;
using MediatR;

namespace EventSourcing.Domain.Contracts.Commands
{
	public class AdditemShoppingCartCommand : IRequest
    {
        public Guid ShoppingCartId { get; private set; }
        public string ItemName { get; private set; }
        public double Price { get; private set; }

        public AdditemShoppingCartCommand(Guid shoppingCartId,  string itemName, double price)
		{
            ShoppingCartId = shoppingCartId;
            ItemName = itemName;
            Price = price;
		}
	}
}

