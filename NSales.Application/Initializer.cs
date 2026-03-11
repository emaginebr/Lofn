using System;
using Core.Domain;
using Core.Domain.Repository;
using DB.Infra;
using DB.Infra.Context;
using DB.Infra.Repository;
using NSales.Domain.Impl.Core;
using NSales.Domain.Impl.Models;
using NSales.Domain.Impl.Services;
using NSales.Domain.Interfaces.Core;
using NSales.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSales.Domain;
using NAuth.ACL;
using NAuth.ACL.Interfaces;
using zTools.ACL.Interfaces;
using zTools.ACL;

namespace NSales.Application
{
    public static class Initializer
    {
        private static void injectDependency(Type serviceType, Type implementationType, IServiceCollection services, bool scoped = true)
        {
            if (scoped)
                services.AddScoped(serviceType, implementationType);
            else
                services.AddTransient(serviceType, implementationType);
        }

        public static void Configure(IServiceCollection services, string connectionString, bool scoped = true)
        {
            if (scoped)
                services.AddDbContext<NSalesContext>(x => x.UseLazyLoadingProxies().UseNpgsql(connectionString));
            else
                services.AddDbContextFactory<NSalesContext>(x => x.UseLazyLoadingProxies().UseNpgsql(connectionString));

            #region Infra
            injectDependency(typeof(NSalesContext), typeof(NSalesContext), services, scoped);
            injectDependency(typeof(IUnitOfWork), typeof(UnitOfWork), services, scoped);
            injectDependency(typeof(ILogCore), typeof(LogCore), services, scoped);
            #endregion

            #region Repository
            injectDependency(typeof(IOrderRepository<OrderModel>), typeof(OrderRepository), services, scoped);
            injectDependency(typeof(IOrderItemRepository<OrderItemModel>), typeof(OrderItemRepository), services, scoped);
            injectDependency(typeof(IProductRepository<ProductModel>), typeof(ProductRepository), services, scoped);
            #endregion

            #region Client
            services.AddHttpClient();
            injectDependency(typeof(IUserClient), typeof(UserClient), services, scoped);
            injectDependency(typeof(IChatGPTClient), typeof(ChatGPTClient), services, scoped);
            injectDependency(typeof(IMailClient), typeof(MailClient), services, scoped);
            injectDependency(typeof(IFileClient), typeof(FileClient), services, scoped);
            injectDependency(typeof(IStringClient), typeof(StringClient), services, scoped);
            injectDependency(typeof(IDocumentClient), typeof(DocumentClient), services, scoped);
            #endregion

            #region Service
            injectDependency(typeof(IProductService), typeof(ProductService), services, scoped);
            injectDependency(typeof(IOrderService), typeof(OrderService), services, scoped);
            #endregion

            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, NAuthHandler>("BasicAuthentication", null);
        }
    }
}
