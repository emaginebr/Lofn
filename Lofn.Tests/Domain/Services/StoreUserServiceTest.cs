using Xunit;
using Moq;
using Lofn.Domain.Services;
using Lofn.Domain.Models;
using Lofn.Infra.Interfaces.Repository;
using NAuth.ACL.Interfaces;
using NAuth.DTO.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lofn.Tests.Domain.Services
{
    public class StoreUserServiceTest
    {
        private readonly Mock<IUserClient> _userClientMock;
        private readonly Mock<IStoreRepository<StoreModel>> _storeRepositoryMock;
        private readonly Mock<IStoreUserRepository<StoreUserModel>> _storeUserRepositoryMock;
        private readonly StoreUserService _sut;

        private readonly StoreModel _validStore = new StoreModel { StoreId = 1, OwnerId = 1, Name = "Loja", Slug = "loja" };

        public StoreUserServiceTest()
        {
            _userClientMock = new Mock<IUserClient>();
            _storeRepositoryMock = new Mock<IStoreRepository<StoreModel>>();
            _storeUserRepositoryMock = new Mock<IStoreUserRepository<StoreUserModel>>();
            _sut = new StoreUserService(
                _userClientMock.Object,
                _storeRepositoryMock.Object,
                _storeUserRepositoryMock.Object
            );
        }

        [Fact]
        public async Task InsertAsync_ShouldThrowException_WhenStoreNotFound()
        {
            _storeRepositoryMock.Setup(x => x.GetByIdAsync(99)).ReturnsAsync((StoreModel)null);

            var ex = await Assert.ThrowsAsync<Exception>(() => _sut.InsertAsync(99, 2, 1, "token"));
            Assert.Equal("Store not found", ex.Message);
        }

        [Fact]
        public async Task InsertAsync_ShouldThrowUnauthorized_WhenUserIsNotOwner()
        {
            _storeRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(_validStore);

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _sut.InsertAsync(1, 2, 999, "token"));
        }

        [Fact]
        public async Task InsertAsync_ShouldThrowException_WhenUserAlreadyExists()
        {
            _storeRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(_validStore);
            _storeUserRepositoryMock.Setup(x => x.ExistsAsync(1, 2)).ReturnsAsync(true);

            var ex = await Assert.ThrowsAsync<Exception>(() => _sut.InsertAsync(1, 2, 1, "token"));
            Assert.Equal("User already belongs to this store", ex.Message);
        }

        [Fact]
        public async Task InsertAsync_ShouldAddUser_WhenValid()
        {
            _storeRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(_validStore);
            _storeUserRepositoryMock.Setup(x => x.ExistsAsync(1, 2)).ReturnsAsync(false);
            _storeUserRepositoryMock.Setup(x => x.InsertAsync(It.IsAny<StoreUserModel>()))
                .ReturnsAsync(new StoreUserModel { StoreUserId = 10, StoreId = 1, UserId = 2 });
            _userClientMock.Setup(x => x.GetByIdAsync(2, "token")).ReturnsAsync(new UserInfo { UserId = 2 });

            var result = await _sut.InsertAsync(1, 2, 1, "token");

            Assert.Equal(10, result.StoreUserId);
            Assert.Equal(2, result.UserId);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenStoreUserNotFound()
        {
            _storeRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(_validStore);
            _storeUserRepositoryMock.Setup(x => x.ListByStoreAsync(1)).ReturnsAsync(new List<StoreUserModel>());

            var ex = await Assert.ThrowsAsync<Exception>(() => _sut.DeleteAsync(1, 99, 1));
            Assert.Equal("Store user not found", ex.Message);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenTryingToRemoveOwner()
        {
            _storeRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(_validStore);
            _storeUserRepositoryMock.Setup(x => x.ListByStoreAsync(1))
                .ReturnsAsync(new List<StoreUserModel> { new StoreUserModel { StoreUserId = 5, StoreId = 1, UserId = 1 } });

            var ex = await Assert.ThrowsAsync<Exception>(() => _sut.DeleteAsync(1, 5, 1));
            Assert.Equal("Cannot remove the owner from the store", ex.Message);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDelete_WhenValid()
        {
            _storeRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(_validStore);
            _storeUserRepositoryMock.Setup(x => x.ListByStoreAsync(1))
                .ReturnsAsync(new List<StoreUserModel> { new StoreUserModel { StoreUserId = 5, StoreId = 1, UserId = 2 } });

            await _sut.DeleteAsync(1, 5, 1);

            _storeUserRepositoryMock.Verify(x => x.DeleteAsync(5), Times.Once);
        }
    }
}
