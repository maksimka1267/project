using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using project.Domain;

namespace project.Models.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly DataManager dataManager;

        public SidebarViewComponent(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var serviceItems = await dataManager.ServiceItems.GetAllServiceItemsAsync();
            return View("Default", serviceItems);
        }
    }
}
