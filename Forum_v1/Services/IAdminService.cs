using System.Threading.Tasks;

namespace Forum_v1.Services
{
    public interface IAdminService
    {
        Task BanUserAsync(string id);
    }
}