using Microsoft.AspNetCore.Mvc;
using project.Domain;

namespace project.Controllers
{
    public class ServicesController : Controller
    {
        private readonly DataManager dataManager;
        public ServicesController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        public async Task<IActionResult> Index(Guid id)
        {
            ViewBag.News = await dataManager.ServiceItems.GetTop3ServiceByFatherAsync("Події");
            return View("Show",await dataManager.ServiceItems.GetServiceItemByIdAsync(id));
        }
        public async Task<IActionResult> Header(string title)
        {
            var serviceItems =await dataManager.ServiceItems.GetServiceItemsAsync();
            ViewBag.ServiceItem = serviceItems;
            return View("Show",await dataManager.ServiceItems.GetServiceItemByTitleAsync(title));
        }
    }
}