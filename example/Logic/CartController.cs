using System.Collections.Generic;
using System.Threading.Tasks;
using PipServices3.Commons.Data;
using PipServices3.Commons.Random;

namespace PipServices3.GraphQL
{
    public sealed class CartController : ICartController
	{
        private readonly object _lock = new object();
        private readonly IList<Cart> _entities = new List<Cart>();

        public CartController() 
        {


		}

		public async Task<DataPage<Cart>> GetPageByFilterAsync(string correlationId, FilterParams filter, PagingParams paging)
		{
			filter = filter != null ? filter : new FilterParams();
			//var key = filter.GetAsNullableString("key");

			paging = paging != null ? paging : new PagingParams();
			var skip = paging.GetSkip(0);
			var take = paging.GetTake(100);

			var result = new List<Cart>();

			lock (_lock)
			{
				foreach (var entity in _entities)
				{
					//if (key != null && !key.Equals(entity.Key))
					//	continue;

					skip--;
					if (skip >= 0) continue;

					take--;
					if (take < 0) break;

					result.Add(entity);
				}
			}

			return await Task.FromResult(new DataPage<Cart>(result, paging.Total ? _entities.Count : null));
		}

		public async Task<Cart> GetOneByIdAsync(string correlationId, long id)
        {
            await Task.Delay(0);

            lock(_lock)
            {
                foreach(var entity in _entities)
                {
                    if (entity.Id.Equals(id))
                        return entity;
                }
            }

            return null;
        }

        public async Task<Cart> CreateAsync(string correlationId, Cart entity)
        {
            await Task.Delay(0);

            lock(_lock)
            {
                _entities.Add(entity);
            }

            return entity;
        }

        public async Task<Cart> UpdateAsync(string correlationId, Cart newEntity)
        {
            await Task.Delay(0);

            lock(_lock)
            {
                for (int index = 0; index < _entities.Count; index++)
                {
                    var entity = _entities[index];
                    if (entity.Id.Equals(newEntity.Id))
                    {
                        _entities[index] = newEntity;
                        return newEntity;
                    }
                }
            }
            return null;
        }

        public async Task<Cart> DeleteByIdAsync(string correlationId, long id)
        {
            await Task.Delay(0);

            lock(_lock)
            {
                for (int index = 0; index < _entities.Count; index++)
                {
                    var entity = _entities[index];
                    if (entity.Id.Equals(id))
                    {
                        _entities.RemoveAt(index);
                        return entity;
                    }
                }
            }
            return null;
        }
    }
}
