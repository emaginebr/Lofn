using Lofn.Infra.Interfaces.Repository;
using NAuth.ACL.Interfaces;
using Lofn.Domain.Models;
using Lofn.Domain.Interfaces;
using Lofn.DTO.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lofn.Domain.Services
{
    public class StoreUserService : IStoreUserService
    {
        private readonly IUserClient _userClient;
        private readonly IStoreRepository<StoreModel> _storeRepository;
        private readonly IStoreUserRepository<StoreUserModel> _storeUserRepository;

        public StoreUserService(
            IUserClient userClient,
            IStoreRepository<StoreModel> storeRepository,
            IStoreUserRepository<StoreUserModel> storeUserRepository
        )
        {
            _userClient = userClient;
            _storeRepository = storeRepository;
            _storeUserRepository = storeUserRepository;
        }

        private async Task ValidateOwnerAsync(long storeId, long ownerId)
        {
            var store = await _storeRepository.GetByIdAsync(storeId);
            if (store == null)
                throw new Exception("Store not found");

            if (store.OwnerId != ownerId)
                throw new UnauthorizedAccessException("Access denied: user is not the owner of this store");
        }

        private async Task<StoreUserInfo> ToInfoAsync(StoreUserModel model, string token)
        {
            return new StoreUserInfo
            {
                StoreUserId = model.StoreUserId,
                StoreId = model.StoreId,
                UserId = model.UserId,
                User = await _userClient.GetByIdAsync(model.UserId, token)
            };
        }

        public async Task<IList<StoreUserInfo>> ListByStoreAsync(long storeId, long ownerId, string token)
        {
            await ValidateOwnerAsync(storeId, ownerId);

            var items = await _storeUserRepository.ListByStoreAsync(storeId);
            var result = new List<StoreUserInfo>();
            foreach (var item in items)
            {
                result.Add(await ToInfoAsync(item, token));
            }
            return result;
        }

        public async Task<StoreUserInfo> InsertAsync(long storeId, long userId, long ownerId, string token)
        {
            await ValidateOwnerAsync(storeId, ownerId);

            if (await _storeUserRepository.ExistsAsync(storeId, userId))
                throw new Exception("User already belongs to this store");

            var model = await _storeUserRepository.InsertAsync(new StoreUserModel
            {
                StoreId = storeId,
                UserId = userId
            });

            return await ToInfoAsync(model, token);
        }

        public async Task DeleteAsync(long storeId, long storeUserId, long ownerId)
        {
            await ValidateOwnerAsync(storeId, ownerId);

            var users = await _storeUserRepository.ListByStoreAsync(storeId);
            var user = users.FirstOrDefault(x => x.StoreUserId == storeUserId);

            if (user == null)
                throw new Exception("Store user not found");

            var store = await _storeRepository.GetByIdAsync(storeId);
            if (user.UserId == store.OwnerId)
                throw new Exception("Cannot remove the owner from the store");

            await _storeUserRepository.DeleteAsync(storeUserId);
        }
    }
}
