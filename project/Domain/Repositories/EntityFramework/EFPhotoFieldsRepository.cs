using Microsoft.EntityFrameworkCore;
using project.Domain.Entities;
using project.Domain.Repositories.Abstract;
using System.IO;

namespace project.Domain.Repositories.EntityFramework
{
	public class EFPhotoFieldsRepository : IPhotoFieldsReporitory
	{
		private readonly AppDbContext context;

		public EFPhotoFieldsRepository(AppDbContext context)
		{
			this.context = context;
		}

		public void SavePhoto(PhotoField photo)
		{
			context.photoFields.Add(photo);
			context.SaveChanges();
		}

		public IQueryable<PhotoField> GetPhoto()
		{
			return context.photoFields;
		}

		public PhotoField GetPhotoById(Guid id)
		{
			return context.photoFields.FirstOrDefault(x => x.Id == id);
		}

		public void DeletePhoto(Guid id)
		{
			var photo = context.photoFields.FirstOrDefault(x => x.Id == id);
			if (photo != null)
			{
				context.photoFields.Remove(photo);
				context.SaveChanges();
			}
		}
	}
}
