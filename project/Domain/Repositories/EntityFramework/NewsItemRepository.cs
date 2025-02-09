using Microsoft.EntityFrameworkCore;
using project.Domain.Entities;
using project.Domain.Repositories.Abstract;

namespace project.Domain.Repositories.EntityFramework
{
    public class NewsItemRepository : INewsItemRepository
    {
        private readonly AppDbContext _context;

        public NewsItemRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task DeleteNewsItemAsync(Guid id)
        {
            try
            {
                var newsModel = await _context.News.FindAsync(id);

                if (newsModel != null)
                {
                    _context.News.Remove(newsModel);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"New с Id {id} не найден.");
                }
            }
            catch (Exception ex)
            {
                // Логируйте исключение
                throw new Exception($"Ошибка при удалении New: {ex.Message}", ex);
            }
        }
        public async Task<bool> ToggleActiveAsync(Guid id)
        {
            var article = await _context.News.FindAsync(id);
            if (article == null)
                return false;

            article.Active = !article.Active;
            await _context.SaveChangesAsync();
            return article.Active; // Возвращаем новый статус
        }
        public async Task<IQueryable<NewsItem>> GetNewsByFatherAsync(Guid father)
        {
            return await Task.FromResult(
                _context.News.
                Where(x => x.Father == father).
                OrderByDescending(x => x.DateAdded)
            );
        }

        public async Task<NewsItem> GetNewsItemByIdAsync(Guid id)
        {
            return await _context.News.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IQueryable<NewsItem>> GetAllNewsItemsAsync()
        {
            return await Task.FromResult(_context.News);
        }
        public async Task<IQueryable<NewsItem>> GetActiveNewsItemsAsync()
        {
            return await Task.FromResult(_context.News.Where(x => x.Active == true));
        }
        public async Task<List<NewsItem>> GetPagedNewsItemsByFatherAsync(Guid? father, int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;

            IQueryable<NewsItem> query = _context.News
                .Where(x => x.Active == true)
                .OrderByDescending(x => x.DateAdded);

            // Если father передан (не null), фильтруем по нему
            if (father.HasValue)
            {
                query = query.Where(x => x.Father == father.Value);
            }

            return await query.Skip(skip).Take(pageSize).ToListAsync();
        }



        public async Task<IQueryable<NewsItem>> GetTop3NewsByFatherAsync(Guid father)
        {
            return await Task.FromResult(
                _context.News.
                Where(x => x.Father == father).
                Where(x => x.Active == true).
                OrderByDescending(x => x.DateAdded).
                Take(3)
                );
        }
        public async Task<IQueryable<NewsItem>> GetTop3NewsAsync()
        {
            return await Task.FromResult(
                _context.News.
                OrderByDescending(x => x.DateAdded).
                Where(x => x.Active == true).
                Take(3)
                );
        }
        public async Task<int> GetTotalNewsItemCountByFatherAsync(Guid father)
        {
            return await _context.News.Where(x=>x.Active==true).CountAsync(x => x.Father == father);
        }

        public async Task SaveNewsItemAsync(NewsItem entity)
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
                // Добавьте логирование исключения
                throw new Exception($"Ошибка при сохранении записи: {ex.Message}", ex);
            }
        }

        public async Task<NewsItem> GetNewsItemByTitleAsync(string title)
        {
            return await _context.News.FirstOrDefaultAsync(x => x.Title == title);
        }
    }
}
