using project.Domain.Entities;
using System.Threading.Tasks;

namespace project.Domain.Repositories.Abstract
{
    public interface IServiceItemsRepository
    {
        Task<IQueryable<ServiceItem>> GetServiceItemsAsync();
        Task<List<ServiceItemDto>> GetAllTitlesAndIdsAsync();
        Task<ServiceItem> GetServiceItemByIdAsync(Guid id);
        Task<ServiceItem> GetServiceItemByTitleAsync(string title);
        Task<List<string>> GetTitleByFatherAsync(string father);
        Task SaveServiceItemAsync(ServiceItem entity);
        Task<IQueryable<ServiceItem>> GetServiceByFatherAsync(string father);
        Task<IQueryable<ServiceItem>> GetTop3ServiceByFatherAsync(string father);
        Task<List<ServiceItem>> GetPagedServiceItemsByFatherAsync(string father, int page, int pageSize);
        Task<IQueryable<ServiceItem>> GetServiceByFatherWithBannerAsync(string father);
        Task DeleteServiceItemAsync(Guid id);
        Task<int> GetTotalServiceItemCountByFatherAsync(string father);
        Task<IQueryable<ServiceItem>> GetServiceItemsForPageAsync(int page, int pageSize);
    }
}
