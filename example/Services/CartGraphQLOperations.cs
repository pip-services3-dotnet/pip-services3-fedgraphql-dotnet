using System;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using PipServices3.Commons.Refer;

namespace PipServices3.GraphQL.Services
{
	public class CartGraphQLOperations: GraphQLOperations
    {
        private ICartController _cartController;
		private IProductController _productController;

		public CartGraphQLOperations()
        {
            _dependencyResolver.Put("cart_controller", new Descriptor("pip-services3-cart", "controller", "default", "*", "*"));
			_dependencyResolver.Put("product_controller", new Descriptor("pip-services3-product", "controller", "default", "*", "*"));
		}
        
        public new void SetReferences(IReferences references)
        {
            base.SetReferences(references);

            _cartController = _dependencyResolver.GetOneRequired<ICartController>("cart_controller");
			_productController = _dependencyResolver.GetOneRequired<IProductController>("product_controller");
		}
        
        public async Task<object> GetByIdAsync(IResolveFieldContext context)
        {
            var correlationId = GetCorrelationId(context);
            var id = context.GetArgument<long>("id");
            
            var result = await _cartController.GetOneByIdAsync(correlationId, id);

			return result;
        }

		public async Task<object> GetProductByIdAsync(IResolveFieldContext context)
		{
			var correlationId = GetCorrelationId(context);
			if (context.Source is not CartItem cartItem) return null;

			var result = await _productController.GetOneByIdAsync(correlationId, cartItem.ProductId);

			return result;
		}

		public async Task<object> GetImagesByProductAsync(IResolveFieldContext context)
		{
			var correlationId = GetCorrelationId(context);
			var limit = context.GetArgument<int>("limit");

			if (context.Source is not Product product) return null;

			return await Task.FromResult(limit > 0 ? product.Images.Take(limit).ToList() : product.Images);
		}
	}
}