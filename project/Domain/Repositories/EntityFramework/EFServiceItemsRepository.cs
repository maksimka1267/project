
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing;
using project.Domain.Entities;
using project.Domain.Repositories.Abstract;

namespace project.Domain.Repositories.EntityFramework
{
    public class EFServiceItemsRepository : IServiceItemsRepository
    {
        private readonly AppDbContext context;
        public EFServiceItemsRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void DeleteServiceItem(Guid id)
        {
            context.ServiceItems.Remove(new ServiceItem() { Id = id });
            context.SaveChanges();
        }
		public int GetTotalServiceItemCountByFather(string father)
		{
			return context.ServiceItems.Where(x => x.Father == father).Count();
		}
		public IQueryable<ServiceItem> GetServiceItemsForPage(int page, int pageSize)
		{
			return context.ServiceItems
						  .Skip((page - 1) * pageSize)
						  .Take(pageSize);
		}
		public List<string> GetTitleByFather(string father)
        {
            return context.ServiceItems.Where(x => x.Father == father).Select(x => x.Title).ToList();
        }
        public ServiceItem GetServiceItemById(Guid id)
        {
            return context.ServiceItems.FirstOrDefault(x => x.Id == id);
        }
        public ServiceItem GetServiceItemByTitle(string title)
        {
            return context.ServiceItems.FirstOrDefault(y => y.Title == title);
        }
        public IQueryable<ServiceItem> GetServiceItems()
        {
            return context.ServiceItems;
        }
		public IQueryable<ServiceItem> GetServiceByFather(string title)
		{
			return context.ServiceItems.Where(x => x.Father == title);
		}
        public void SaveServiceItem(ServiceItem entity)
        {
            if (entity.Id == default)
            {
                context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                context.Entry(entity).State = EntityState.Modified;
            }
            context.SaveChanges();
        }
    }
}
