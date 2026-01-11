using Microsoft.AspNetCore.Mvc.Rendering;
using project.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project.Domain.Repositories.Abstract
{
    public interface IPageItemsRepository
    {
        Task<IQueryable<PageItem>> GetTextFieldsAsync();
        Task<List<PageItem>> GetAllPageModelForIndexAsync();
        Task<PageItem> GetTextFieldByIdAsync(Guid id);
        Task<PageItem> GetTextByTitleAsync(string title);
        Task<List<PageItem>> GetTextByFatherAsync(Guid father);
        Task<List<SelectListItem>> GetDistinctTitlesWithIdsAsync();
        Task SaveTextFieldAsync(PageItem entity);
        Task DeleteTextFieldAsync(Guid id);
    }
}