using Lofn.Domain.Interfaces;
using Lofn.DTO.ShopCar;
using System.Threading.Tasks;

namespace Lofn.Domain.Services
{
    public class ShopCarService : IShopCarService
    {
        public async Task<ShopCarInfo> InsertAsync(ShopCarInfo shopCar)
        {
            return await Task.FromResult(shopCar);
        }
    }
}
