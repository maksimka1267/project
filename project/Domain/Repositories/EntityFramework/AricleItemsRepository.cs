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
    public class AricleItemsRepository : IArticleItemsRepository
    {
        private readonly AppDbContext _context;

        public AricleItemsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task DeleteServiceItemAsync(Guid id)
        {
            try
            {
                var articleModel = await _context.ArticleItems.FindAsync(id);

                if (articleModel != null)
                {
                    _context.ArticleItems.Remove(articleModel);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"ArticleItem с Id {id} не найден.");
                }
            }
            catch (Exception ex)
            {
                // Логируйте исключение
                throw new Exception($"Ошибка при удалении ArticleItem: {ex.Message}", ex);
            }
        }
        public async Task<ArticleItem> GetServiceItemByIdAsync(Guid id)
        {
            return await _context.ArticleItems.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<bool> ToggleActiveAsync(Guid id)
        {
            var article = await _context.ArticleItems.FindAsync(id);
            if (article == null)
                return false;

            article.Active = !article.Active;
            await _context.SaveChangesAsync();
            return article.Active; // Возвращаем новый статус
        }

        public async Task<ArticleItem> GetServiceItemByTitleAsync(string title)
        {
            return await _context.ArticleItems.FirstOrDefaultAsync(x => x.Title == title);
        }

        public async Task<IQueryable<ArticleItem>> GetAllServiceItemsAsync()
        {
            return await Task.FromResult(_context.ArticleItems);
        }
        public async Task<List<SelectListItem>> GetDistinctTitlesWithIdsAsync()
        {
            return await _context.ArticleItems
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),  // Используем Id как значение
                    Text = x.Title            // Используем Title как текст
                })
                .Distinct()
                .ToListAsync();
        }
        public async Task<IQueryable<ArticleItem>> GetServiceByFatherAsync(Guid father)
        {
            return await Task.FromResult(
                _context.ArticleItems.
                Where(x => x.Father == father).
                Where(x=> x.Active == true).
                OrderByDescending(x => x.DateAdded)
            );
        }
        public async Task<IQueryable<ArticleItem>> GetServiceByFatherWithBannerAsync(Guid father)
        {
            return await Task.FromResult(
                _context.ArticleItems.
                Where(x => x.Father == father).
                Where(x=>x.ShowBanners==true).
                Where(x=>x.Active==true).
                OrderByDescending(item => item.DateAdded)
            );
        }
        public async Task SaveServiceItemAsync(ArticleItem entity)
        {
            try
            {
                var existingEntity = await _context.ArticleItems.FindAsync(entity.Id);

                if (existingEntity == null)
                {
                    // Если объект новый, добавляем его
                    _context.ArticleItems.Add(entity);
                }
                else
                {
                    // Если объект уже отслеживается, применяем обновление
                    _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при сохранении записи: {ex.Message}", ex);
            }
        }

    }
}
