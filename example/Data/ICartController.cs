using PipServices3.Commons.Data;
using System.Threading.Tasks;

namespace PipServices3.GraphQL
{
    public interface ICartController
    {
		Task<DataPage<Cart>> GetPageByFilterAsync(string correlationId, FilterParams filter, PagingParams paging);
		Task<Cart> GetOneByIdAsync(string correlationId, long id);
		Task<Cart> CreateAsync(string correlationId, Cart entity);
		Task<Cart> UpdateAsync(string correlationId, Cart entity);
		Task<Cart> DeleteByIdAsync(string correlationId, long id);
	}
}
