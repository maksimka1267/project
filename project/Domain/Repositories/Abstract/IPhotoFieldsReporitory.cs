using project.Domain.Entities;

namespace project.Domain.Repositories.Abstract
{
	public interface IPhotoFieldsReporitory
	{
		void SavePhoto(PhotoField photo);
		void DeletePhoto(Guid id);
		IQueryable<PhotoField> GetPhoto();
		PhotoField GetPhotoById(Guid id);
	}
}
