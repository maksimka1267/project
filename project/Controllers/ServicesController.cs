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
        public IActionResult Index(Guid id)
        {
            var serviceItems = dataManager.ServiceItems.GetServiceItems();
            ViewBag.ServiceItem = serviceItems;
            if (id != default)
            {
                return View("Show", dataManager.ServiceItems.GetServiceItemById(id));
            }

            //ViewBag.TextField = dataManager.TextFields.GetTextByCodeWord("PageServices");
            return View(dataManager.ServiceItems.GetServiceItems());
        }
        public IActionResult Header(string title)
        {
            var serviceItems = dataManager.ServiceItems.GetServiceItems();
            ViewBag.ServiceItem = serviceItems;
            return View("Show", dataManager.ServiceItems.GetServiceItemByTitle(title));
        }
    }
}
