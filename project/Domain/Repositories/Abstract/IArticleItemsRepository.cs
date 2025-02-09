using Microsoft.AspNetCore.Mvc.Rendering;
using project.Domain.Entities;
using System.Threading.Tasks;

namespace project.Domain.Repositories.Abstract
{
    public interface IArticleItemsRepository
    {
        Task<IQueryable<ArticleItem>> GetAllServiceItemsAsync();
        Task<ArticleItem> GetServiceItemByIdAsync(Guid id);
        Task<ArticleItem> GetServiceItemByTitleAsync(string title);
        Task SaveServiceItemAsync(ArticleItem entity);
        Task<IQueryable<ArticleItem>> GetServiceByFatherAsync(Guid father);
        Task<IQueryable<ArticleItem>> GetServiceByFatherWithBannerAsync(Guid father);
        Task<List<SelectListItem>> GetDistinctTitlesWithIdsAsync();
        Task<bool> ToggleActiveAsync(Guid id);
        Task DeleteServiceItemAsync(Guid id);
    }
}
