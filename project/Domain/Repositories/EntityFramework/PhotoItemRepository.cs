using Microsoft.EntityFrameworkCore;
using project.Domain.Entities;
using project.Domain.Repositories.Abstract;

namespace project.Domain.Repositories.EntityFramework
{
    public class PhotoItemRepository : IPhotoItemRepository
    {
        private readonly AppDbContext _context;

        public PhotoItemRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task DeletePhotoModelAsync(Guid id)
        {
            try
            {
                var photoModel = await _context.PhotoModels.FindAsync(id);

                if (photoModel != null)
                {
                    _context.PhotoModels.Remove(photoModel);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"PhotoModel с Id {id} не найден.");
                }
            }
            catch (Exception ex)
            {
                // Логируйте исключение
                throw new Exception($"Ошибка при удалении PhotoModel: {ex.Message}", ex);
            }
        }
        public async Task<byte[]?> GetImageDataByIdAsync(Guid id)
        {
            var photo = await _context.PhotoModels.FirstOrDefaultAsync(x => x.Id == id);
            return photo?.ImageData;  // Возвращаем null, если фото не найдено
        }


        public async Task<PhotoModel> GetPhotoModelByIdAsync(Guid id)
        {
            return await _context.PhotoModels.FirstOrDefaultAsync(x=>x.Id == id);

        }

        public async Task SavePhotoModelAsync(PhotoModel entity)
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
