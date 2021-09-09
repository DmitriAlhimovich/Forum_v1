using Forum_v1.Services;
using Moq;
using NUnit.Framework;
using Repository.Absract;
using Repository.Entities;
using System;
using System.Threading.Tasks;

namespace Forum.UnitTests
{
    public class AdminServiceTests
    {
        private AdminService _underTest;
        private Mock<IApplicationUserManager> _userManager;
        private Mock<IGenericRepository<BanEmail>> _banRepo;

        [SetUp]
        public void Setup()
        {
            _userManager = new Mock<IApplicationUserManager>();
            _banRepo = new Mock<IGenericRepository<BanEmail>>();
            _underTest = new AdminService(_userManager.Object, _banRepo.Object);
        }

        [Test]
        public async Task BanUserAsync_ForNotFoundUser_ThrowsExceptionAsync()
        {
            //arrange
            _userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);

            //act/assert
            Assert.ThrowsAsync<Exception>(() => _underTest.BanUserAsync("1"));
        }

        [Test]
        public async Task BanUserAsync_ForNotBannedUser_CallsUserManagerAndBanRepo()
        {
            //arrange
            string testEmail = "test";
            _userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser { Email = testEmail, Ban = false });

            //act
            await _underTest.BanUserAsync("1");

            //assert
            _userManager.Verify(x => x.UpdateAsync(It.Is<ApplicationUser>(u => u.Ban == true)), Times.Once);
            _banRepo.Verify(x => x.CreateAsync(It.Is<BanEmail>(b => b.Email == testEmail)), Times.Once);
        }

        [Test]
        public async Task BanUserAsync_ForBannedUser_DoesntCallUserManagerAndBanRepo()
        {
            //arrange
            string testEmail = "test";
            _userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser { Email = testEmail, Ban = true });

            //act
            await _underTest.BanUserAsync("1");

            //assert
            _userManager.Verify(x => x.UpdateAsync(It.IsAny<ApplicationUser>()), Times.Never);
            _banRepo.Verify(x => x.CreateAsync(It.IsAny<BanEmail>()), Times.Never);
        }
    }
}
