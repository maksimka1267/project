using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project.Domain.Entities;
using project.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project.Domain.Repositories.EntityFramework
{
    public class PageItemsRepository : IPageItemsRepository
    {
        private readonly AppDbContext _context;

        public PageItemsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task DeleteTextFieldAsync(Guid id)
        {
            try
            {
                var pageModel = await _context.PageItems.FindAsync(id);

                if (pageModel != null)
                {
                    _context.PageItems.Remove(pageModel);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"PageItem с Id {id} не найден.");
                }
            }
            catch (Exception ex)
            {
                // Логируйте исключение
                throw new Exception($"Ошибка при удалении PageItem: {ex.Message}", ex);
            }
        }
        public async Task<PageItem> GetTextByTitleAsync(string title)
        {
            return await _context.PageItems.FirstOrDefaultAsync(x => x.Title == title);
        }

        public async Task<List<PageItem>> GetTextByFatherAsync(Guid father)
        {
            return await _context.PageItems
                                 .Where(x => x.Father == father)
                                 .ToListAsync();
        }
        public async Task<List<SelectListItem>> GetDistinctTitlesWithIdsAsync()
        {
            return await _context.PageItems
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),  // Используем Id как значение
                    Text = x.Title            // Используем Title как текст
                })
                .Distinct()
                .ToListAsync();
        }


        public async Task<PageItem> GetTextFieldByIdAsync(Guid id)
        {
            return await _context.PageItems.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveTextFieldAsync(PageItem entity)
        {
            try
            {
                if (entity.Id == default)
                {
                    _context.Entry(entity).State = EntityState.Added;
                }
                else
                {
                    _context.Entry(entity).State = EntityState.Modified;
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Логируйте исключение
                throw new Exception($"Ошибка при сохранении TextField: {ex.Message}", ex);
            }
        }

        public async Task<IQueryable<PageItem>> GetTextFieldsAsync()
        {
            return await Task.FromResult(_context.PageItems);
        }

        public async Task<List<PageItem>> GetAllPageModelForIndexAsync()
        {
            return await _context.PageItems
                        .Where(x => x.Index == true)
                        .ToListAsync();
        }
    }
}
