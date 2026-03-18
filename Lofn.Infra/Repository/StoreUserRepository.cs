using Lofn.Infra.Interfaces.Repository;
using Lofn.Infra.Context;
using Lofn.Infra.Mappers;
using Lofn.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lofn.Infra.Repository
{
    public class StoreUserRepository : IStoreUserRepository<StoreUserModel>
    {
        private readonly LofnContext _context;

        public StoreUserRepository(LofnContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StoreUserModel>> ListByStoreAsync(long storeId)
        {
            var rows = await _context.StoreUsers
                .Where(x => x.StoreId == storeId)
                .ToListAsync();
            return rows.Select(StoreUserDbMapper.ToModel);
        }

        public async Task<IEnumerable<StoreUserModel>> ListByUserAsync(long userId)
        {
            var rows = await _context.StoreUsers
                .Where(x => x.UserId == userId)
                .ToListAsync();
            return rows.Select(StoreUserDbMapper.ToModel);
        }

        public async Task<StoreUserModel> InsertAsync(StoreUserModel model)
        {
            var row = new StoreUser
            {
                StoreId = model.StoreId,
                UserId = model.UserId
            };
            _context.Add(row);
            await _context.SaveChangesAsync();
            model.StoreUserId = row.StoreUserId;
            return model;
        }

        public async Task DeleteAsync(long storeUserId)
        {
            var row = await _context.StoreUsers.FindAsync(storeUserId);
            if (row != null)
            {
                _context.StoreUsers.Remove(row);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(long storeId, long userId)
        {
            return await _context.StoreUsers
                .AnyAsync(x => x.StoreId == storeId && x.UserId == userId);
        }
    }
}
