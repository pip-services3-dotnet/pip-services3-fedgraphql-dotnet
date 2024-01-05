using System.Collections.Generic;

namespace PipServices3.GraphQL
{
	public class Cart
	{
		public long Id { get; set; }
		public List<CartItem> Items { get; set; } = new List<CartItem>();
        public decimal SubTotal { get; set; }
    }

	public class CartItem
	{
        public int ProductId { get; set; }
        public int Quantity { get; set; }
		public decimal Total { get; set; }
	}
}
