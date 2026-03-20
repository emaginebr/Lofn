using Lofn.DTO.ShopCar;
using System.Threading.Tasks;

namespace Lofn.Domain.Interfaces
{
    public interface IShopCarService
    {
        Task<ShopCarInfo> InsertAsync(ShopCarInfo shopCar);
    }
}
