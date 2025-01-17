using Microsoft.EntityFrameworkCore;
using project.Domain.Entities;
using project.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project.Domain.Repositories.EntityFramework
{
    public class EFServiceItemsRepository : IServiceItemsRepository
    {
        private readonly AppDbContext _context;

        public EFServiceItemsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task DeleteServiceItemAsync(Guid id)
        {
            try
            {
                _context.ServiceItems.Remove(new ServiceItem { Id = id });
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Добавьте логирование исключения
                throw new Exception($"Ошибка при удалении записи: {ex.Message}", ex);
            }
        }

        public async Task<int> GetTotalServiceItemCountByFatherAsync(string father)
        {
            return await _context.ServiceItems.CountAsync(x => x.Father == father);
        }

        public async Task<IQueryable<ServiceItem>> GetServiceItemsForPageAsync(int page, int pageSize)
        {
            return await Task.FromResult(
                _context.ServiceItems.Skip((page - 1) * pageSize).Take(pageSize)
            );
        }

        public async Task<List<string>> GetTitleByFatherAsync(string father)
        {
            return await _context.ServiceItems
                                 .Where(x => x.Father == father)
                                 .Select(x => x.Title)
                                 .ToListAsync();
        }

        public async Task<ServiceItem> GetServiceItemByIdAsync(Guid id)
        {
            return await _context.ServiceItems.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ServiceItem> GetServiceItemByTitleAsync(string title)
        {
            return await _context.ServiceItems.FirstOrDefaultAsync(x => x.Title == title);
        }

        public async Task<IQueryable<ServiceItem>> GetServiceItemsAsync()
        {
            return await Task.FromResult(_context.ServiceItems);
        }

        public async Task<IQueryable<ServiceItem>> GetServiceByFatherAsync(string father)
        {
            return await Task.FromResult(
                _context.ServiceItems.
                Where(x => x.Father == father).
                OrderByDescending(x => x.DateAdded)
            );
        }
        public async Task<IQueryable<ServiceItem>> GetTop3ServiceByFatherAsync(string father)
        {
            return await Task.FromResult(
                _context.ServiceItems.
                Where(x => x.Father == father).
                OrderByDescending(x => x.DateAdded).
                Take(3)
                );
        }
        public async Task<List<ServiceItem>> GetPagedServiceItemsByFatherAsync(string father, int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;
            return await _context.ServiceItems
                .Where(x => x.Father == father)
                .OrderByDescending(x => x.DateAdded)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IQueryable<ServiceItem>> GetServiceByFatherWithBannerAsync(string father)
        {
            return await Task.FromResult(
                _context.ServiceItems.
                Where(x => x.Father == father).
                Where(x=>x.ShowBanners==true).
                OrderByDescending(item => item.DateAdded)
            );
        }

        public async Task SaveServiceItemAsync(ServiceItem entity)
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
        public async Task<List<ServiceItemDto>> GetAllTitlesAndIdsAsync()
        {
            return await _context.ServiceItems
                .Select(x => new ServiceItemDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Father = x.Father,
                })
                .ToListAsync();
        }

    }
}
