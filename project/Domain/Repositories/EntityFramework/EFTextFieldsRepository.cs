using Microsoft.EntityFrameworkCore;
using project.Domain.Entities;
using project.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project.Domain.Repositories.EntityFramework
{
    public class EFTextFieldsRepository : ITextFieldsRepository
    {
        private readonly AppDbContext _context;

        public EFTextFieldsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task DeleteTextFieldAsync(Guid id)
        {
            try
            {
                _context.TextFields.Remove(new TextField { Id = id });
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Логируйте исключение
                throw new Exception($"Ошибка при удалении TextField: {ex.Message}", ex);
            }
        }

        public async Task<TextField> GetTextByCodeWordAsync(string codeWord)
        {
            return await _context.TextFields.FirstOrDefaultAsync(x => x.CodeWord == codeWord);
        }

        public async Task<TextField> GetTextByTitleAsync(string title)
        {
            return await _context.TextFields.FirstOrDefaultAsync(x => x.Title == title);
        }

        public async Task<List<string>> GetDistinctCodeWordsAsync()
        {
            return await _context.TextFields
                                 .Select(x => x.CodeWord)
                                 .Distinct()
                                 .ToListAsync();
        }
        public async Task<List<TextField>> GetTextByFatherAsync(string father)
        {
            return await _context.TextFields
                                 .Where(x => x.Father == father)
                                 .ToListAsync();
        }
        public async Task<List<string>> GetDistinctTitlesAsync()
        {
            return await _context.TextFields
                                 .Select(x => x.Title)
                                 .Distinct()
                                 .ToListAsync();
        }

        public async Task<TextField> GetTextFieldByIdAsync(Guid id)
        {
            return await _context.TextFields.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveTextFieldAsync(TextField entity)
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

        public async Task<IQueryable<TextField>> GetTextFieldsAsync()
        {
            return await Task.FromResult(_context.TextFields);
        }
        /* public async Task<List<string>> GetAllPageModelForIndex()
         {
             return await _context.TextFields
                         .Where(x=>x.Index==true)
                         .Select(x=>x.Title)
                         .ToListAsync();
         }*/
        public async Task<List<TextFieldDto>> GetAllTitlesAndIdsAsync()
        {
            return await _context.TextFields
                .Select(x => new TextFieldDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Father=x.Father,
                    View=x.View
                })
                .ToListAsync();
        }
    }
}
