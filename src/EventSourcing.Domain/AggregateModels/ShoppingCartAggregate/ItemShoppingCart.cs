namespace EventSourcing.Domain.AggregateModels.ShoppingCartAggregate
{
	public class ItemShoppingCart
	{
        public Guid ItemId { get; private set; }
        public string ItemName { get; private set; }
        public double Price { get; private set; }

        public ItemShoppingCart(Guid itemId, string itemName, double price)
		{
			ItemId = itemId;
			ItemName = itemName;
			Price = price;
		}
	}
}

