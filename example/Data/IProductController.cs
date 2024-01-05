using PipServices3.Commons.Data;
using System.Threading.Tasks;

namespace PipServices3.GraphQL
{
    public interface IProductController
	{
		Task<DataPage<Product>> GetPageByFilterAsync(string correlationId, FilterParams filter, PagingParams paging);
		Task<Product> GetOneByIdAsync(string correlationId, long id);
		Task<Product> CreateAsync(string correlationId, Product entity);
		Task<Product> UpdateAsync(string correlationId, Product entity);
		Task<Product> DeleteByIdAsync(string correlationId, long id);
	}
}
