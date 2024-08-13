using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.FileSystemGlobbing;
using project.Areas.Hort.Controllers;
using project.Domain;
using project.Domain.Entities;
using System.Diagnostics;

namespace project.Controllers
{
	public class HomeController : Controller
	{
        private readonly DataManager dataManager;

		public HomeController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
		public IActionResult Index()
		{
			var textFields = dataManager.TextFields.GetTextFields();
			var serviceItem = dataManager.ServiceItems.GetServiceItems();
            ViewBag.TextFields = textFields;
			ViewBag.ServiceItems = serviceItem;
            return View();
		}
		public IActionResult About()
		{
			var textFields = dataManager.TextFields.GetTextFields();
			var serviceItem = dataManager.ServiceItems.GetServiceItems();
			ViewBag.TextFields = textFields;
			ViewBag.ServiceItems = serviceItem;
			return View();
		}
		public IActionResult Admissions(string title)
		{
			var textFields = dataManager.TextFields.GetTextFields();
			var serviceItem = dataManager.ServiceItems.GetServiceByFather(title);
			ViewBag.TextFields = textFields;
			ViewBag.ServiceItems = serviceItem;
			ViewBag.Title = title;
			return View();
		}
		public IActionResult Blog(string title)
		{
			var textFields = dataManager.TextFields.GetTextFields();
			var serviceItem = dataManager.ServiceItems.GetServiceByFather(title);
			ViewBag.TextFields = textFields;
			ViewBag.ServiceItems = serviceItem;
			ViewBag.Title = title;
			return View();
		}
		public IActionResult Event(string title, int page = 1)
		{
			const int pageSize = 10; // Количество элементов на странице
			var textFields = dataManager.TextFields.GetTextFields();
			var serviceItems = dataManager.ServiceItems.GetServiceByFather(title)
								.Skip((page - 1) * pageSize)
								.Take(pageSize)
								.ToList();

			ViewBag.TextFields = textFields;
			ViewBag.ServiceItems = serviceItems;
			ViewBag.Title = title;

			// Добавляем информацию о текущей странице и общем количестве страниц в ViewBag
			ViewBag.CurrentPage = page;
			ViewBag.TotalPages = (int)Math.Ceiling(dataManager.ServiceItems.GetTotalServiceItemCountByFather(title) / (double)pageSize);

			return View();
		}
		public IActionResult Faculty(string title)
		{
			var textFields = dataManager.TextFields.GetTextFields();
			var serviceItem = dataManager.ServiceItems.GetTitleByFather(title);
			ViewBag.TextFields = textFields;
			ViewBag.ServiceItems = serviceItem;
			ViewBag.Title = title;
			return View();
		}
        public IActionResult Gallery(string title)
        {
			IQueryable<PhotoField> allPhotos = dataManager.PhotoFields.GetPhoto(); // Получаем все фотографии из базы данных
			return View(allPhotos);
		}

    }
}
