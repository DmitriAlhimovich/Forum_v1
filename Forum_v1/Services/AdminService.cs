using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Repository.Absract;
using Repository.Entities;

namespace Forum_v1.Services
{
    public class AdminService : IAdminService
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IGenericRepository<BanEmail> _banRepo;

        public AdminService(IApplicationUserManager userManager, IGenericRepository<BanEmail> banRepo)
        {
            _userManager = userManager;
            _banRepo = banRepo;
        }

        public async Task BanUserAsync(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                throw new Exception("User Not found");
            }

            if (user.Ban)
            {
                return;
            }

            user.Ban = true;
            await _userManager.UpdateAsync(user);

            BanEmail banEmail = new BanEmail { Email = user.Email };

            await _banRepo.CreateAsync(banEmail);
        }
    }

    public interface IApplicationUserManager
    {
        Task<ApplicationUser> FindByIdAsync(string id);
        Task UpdateAsync(ApplicationUser user);
    }

    public class ApplicationUserManager : IApplicationUserManager
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserManager(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser> FindByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task UpdateAsync(ApplicationUser user)
        {
            await _userManager.UpdateAsync(user);
        }
    }
}