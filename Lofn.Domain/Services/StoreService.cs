using Lofn.Infra.Interfaces.Repository;
using Lofn.Domain.Mappers;
using Lofn.Domain.Models;
using Lofn.Domain.Interfaces;
using Lofn.DTO.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lofn.Domain.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository<StoreModel> _storeRepository;

        public StoreService(IStoreRepository<StoreModel> storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<IList<StoreInfo>> ListAllAsync()
        {
            var items = await _storeRepository.ListAllAsync();
            return items.Select(StoreMapper.ToInfo).ToList();
        }

        public async Task<IList<StoreInfo>> ListByOwnerAsync(long ownerId)
        {
            var items = await _storeRepository.ListByOwnerAsync(ownerId);
            return items.Select(StoreMapper.ToInfo).ToList();
        }

        public async Task<StoreModel> GetByIdAsync(long storeId)
        {
            return await _storeRepository.GetByIdAsync(storeId);
        }

        public async Task<StoreModel> InsertAsync(StoreInfo store, long ownerId)
        {
            if (string.IsNullOrEmpty(store.Name))
            {
                throw new Exception("Name is empty");
            }

            var model = StoreMapper.ToModel(store, ownerId);
            return await _storeRepository.InsertAsync(model);
        }

        public async Task<StoreModel> UpdateAsync(StoreInfo store)
        {
            if (string.IsNullOrEmpty(store.Name))
            {
                throw new Exception("Name is empty");
            }

            var model = StoreMapper.ToModel(store, store.OwnerId);
            return await _storeRepository.UpdateAsync(model);
        }

        public async Task DeleteAsync(long storeId)
        {
            await _storeRepository.DeleteAsync(storeId);
        }
    }
}
