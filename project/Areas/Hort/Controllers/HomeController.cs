using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project.Domain;

namespace project.Areas.Hort.Controllers
{
	[Area("Hort")]
    [Authorize]
    public class HomeController : Controller
	{
		private readonly DataManager dataManager;


        public HomeController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.TextFields = await dataManager.TextFields.GetTextFieldsAsync();
            ViewBag.ServiceItems = await dataManager.ServiceItems.GetAllServiceItemsAsync();
            ViewBag.NewsItems = await dataManager.NewsItems.GetAllNewsItemsAsync();
            ViewBag.ArticleName = await dataManager.ServiceItems.GetDistinctTitlesWithIdsAsync();
            ViewBag.PageName = await dataManager.TextFields.GetDistinctTitlesWithIdsAsync();
            return View();
        }

    }
}
