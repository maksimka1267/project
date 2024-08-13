using project.Domain.Entities;

namespace project.Domain.Repositories.Abstract
{
    public interface IServiceItemsRepository
    {
        IQueryable<ServiceItem> GetServiceItems();
        ServiceItem GetServiceItemById(Guid id);
        ServiceItem GetServiceItemByTitle(string title);
        List<string> GetTitleByFather(string father);
        void SaveServiceItem(ServiceItem entity);
		IQueryable<ServiceItem> GetServiceByFather(string father);
        void DeleteServiceItem(Guid id);
        int GetTotalServiceItemCountByFather(string father);
        IQueryable<ServiceItem> GetServiceItemsForPage(int page, int pageSize);

	}
}
