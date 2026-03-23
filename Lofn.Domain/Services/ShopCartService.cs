using FluentValidation;
using Lofn.Domain.Interfaces;
using Lofn.DTO.ShopCart;
using Lofn.Infra.Interfaces.AppService;
using System.Threading.Tasks;

namespace Lofn.Domain.Services
{
    public class ShopCartService : IShopCartService
    {
        private readonly IRabbitMQAppService _rabbitMQAppService;
        private readonly IValidator<ShopCartInfo> _validator;

        public ShopCartService(
            IRabbitMQAppService rabbitMQAppService,
            IValidator<ShopCartInfo> validator)
        {
            _rabbitMQAppService = rabbitMQAppService;
            _validator = validator;
        }

        public async Task<ShopCartInfo> InsertAsync(ShopCartInfo shopCart)
        {
            _validator.ValidateAndThrow(shopCart);
            await _rabbitMQAppService.PublishAsync(shopCart);
            return shopCart;
        }
    }
}
