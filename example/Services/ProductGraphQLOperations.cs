using System;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using PipServices3.Commons.Refer;

namespace PipServices3.GraphQL.Services
{
	public class ProductGraphQLOperations : GraphQLOperations
    {
		private IProductController _productController;

		public ProductGraphQLOperations()
        {
			_dependencyResolver.Put("product_controller", new Descriptor("pip-services3-product", "controller", "default", "*", "*"));
		}
        
        public new void SetReferences(IReferences references)
        {
            base.SetReferences(references);

			_productController = _dependencyResolver.GetOneRequired<IProductController>("product_controller");
		}
        
        public async Task<object> GetByIdAsync(IResolveFieldContext context)
        {
            var correlationId = GetCorrelationId(context);
            var id = context.GetArgument<long>("id");
            
            var result = await _productController.GetOneByIdAsync(correlationId, id);

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