using PipServices3.Commons.Refer;
using PipServices3.GraphQL.Services;

namespace PipServices3.GraphQL.Federation.Services
{
	public class ProductGraphQLService : GraphQLService
	{
		private ProductGraphQLOperations _operations = new ProductGraphQLOperations();

		public ProductGraphQLService()
			: base("schema-product.graphql")
		{ }

		public override void SetReferences(IReferences references)
		{
			base.SetReferences(references);

			_operations.SetReferences(references);
		}

		public override void Register()
		{
			RegisterQuery("product", _operations.GetByIdAsync);
			RegisterQuery("Product.images", _operations.GetImagesByProductAsync);

			base.Register();
		}
	}
}
