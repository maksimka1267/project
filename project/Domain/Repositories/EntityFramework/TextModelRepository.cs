using Microsoft.EntityFrameworkCore;
using project.Domain.Entities;
using project.Domain.Repositories.Abstract;

namespace project.Domain.Repositories.EntityFramework
{
    public class TextModelRepository: ITextModelRepository
    {
        private readonly AppDbContext _context;

        public TextModelRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task DeleteTextModelAsync(Guid id)
        {
            try
            {
                var textModel = await _context.TextModels.FindAsync(id);

                if (textModel != null)
                {
                    _context.TextModels.Remove(textModel);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"TextModel с Id {id} не найден.");
                }
            }
            catch (Exception ex)
            {
                // Логируйте исключение
                throw new Exception($"Ошибка при удалении TextModel: {ex.Message}", ex);
            }
        }


        public async Task<TextModel> GetTextModelByIdAsync(Guid id)
        {
            return await _context.TextModels.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<string?> GetTextTextModelByIdAsync(Guid id)
        {
            var text= await _context.TextModels.FirstOrDefaultAsync(x => x.Id == id);
            return text?.Text;
        }

        public async Task SaveTextModelAsync(TextModel entity)
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
    }
}
