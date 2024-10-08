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

		public IActionResult Index()
		{
			var serviceItems = dataManager.ServiceItems.GetServiceItems();
			var textFields = dataManager.TextFields.GetTextFields();
			var photoFields = dataManager.PhotoFields.GetPhoto();
			var title = dataManager.TextFields.GetTitleList();
			// Помещаем данные в ViewBag
			ViewBag.ServiceItems = serviceItems;
			ViewBag.TextFields = textFields;
			ViewBag.Name = title;
			ViewBag.PhotoFields = photoFields;
			return View();
		}
	}
}
