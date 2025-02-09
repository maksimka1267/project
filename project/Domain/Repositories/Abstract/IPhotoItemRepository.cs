using project.Domain.Entities;

namespace project.Domain.Repositories.Abstract
{
    public interface IPhotoItemRepository
    {
        Task<PhotoModel> GetPhotoModelByIdAsync(Guid id);
        Task<byte[]?> GetImageDataByIdAsync(Guid id);
        Task SavePhotoModelAsync(PhotoModel entity);
        Task DeletePhotoModelAsync(Guid id);
    }
}
