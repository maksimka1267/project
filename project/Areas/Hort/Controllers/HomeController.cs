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
            ViewBag.TextFields = await dataManager.TextFields.GetAllTitlesAndIdsAsync();
            ViewBag.ServiceItems = await dataManager.ServiceItems.GetAllTitlesAndIdsAsync();
            //ViewBag.PhotoFields = dataManager.PhotoFields.GetPhoto();
            ViewBag.Name = await dataManager.TextFields.GetDistinctTitlesAsync();
            return View();
        }

    }
}
