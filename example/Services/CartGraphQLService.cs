using PipServices3.Commons.Refer;
using PipServices3.GraphQL.Services;

namespace PipServices3.GraphQL.Federation.Services
{
	public class CartGraphQLService : GraphQLService
	{
		private CartGraphQLOperations _operations = new CartGraphQLOperations();

		public CartGraphQLService()
			: base("schema-cart.graphql")
		{ }

		public override void SetReferences(IReferences references)
		{
			base.SetReferences(references);

			_operations.SetReferences(references);
		}

		public override void Register()
		{
			RegisterQuery("cart", _operations.GetByIdAsync);

			base.Register();
		}
	}
}
