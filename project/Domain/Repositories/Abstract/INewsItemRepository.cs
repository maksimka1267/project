using project.Domain.Entities;

namespace project.Domain.Repositories.Abstract
{
    public interface INewsItemRepository
    {
        Task<IQueryable<NewsItem>> GetAllNewsItemsAsync();
        Task<IQueryable<NewsItem>> GetActiveNewsItemsAsync();
        Task<NewsItem> GetNewsItemByIdAsync(Guid id);
        Task SaveNewsItemAsync(NewsItem entity);
        Task<IQueryable<NewsItem>> GetNewsByFatherAsync(Guid father);
        Task<NewsItem> GetNewsItemByTitleAsync(string title);
        Task<IQueryable<NewsItem>> GetTop3NewsByFatherAsync(Guid father);
        Task<IQueryable<NewsItem>> GetTop3NewsAsync();
        Task<List<NewsItem>> GetPagedNewsItemsByFatherAsync(Guid? father, int page, int pageSize);
        Task DeleteNewsItemAsync(Guid id);
        Task<bool> ToggleActiveAsync(Guid id);
        Task<int> GetTotalNewsItemCountByFatherAsync(Guid father);
    }
}
