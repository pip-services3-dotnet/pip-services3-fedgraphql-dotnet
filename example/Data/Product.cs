using System.Collections.Generic;

namespace PipServices3.GraphQL
{
	public class Product
	{
        public long Id { get; set; }
		public string Title { get; set; }
		public decimal Price { get; set; }
		public string Description { get; set; }
		public string Sku { get; set;}
        public List<string> Images { get; set; } = new List<string>();
    }
}
