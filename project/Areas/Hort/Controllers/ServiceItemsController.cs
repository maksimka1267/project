using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project.Domain;
using project.Domain.Entities;
using project.Service;

namespace project.Areas.Hort.Controllers
{
	[Area("Hort")]
	public class ServiceItemsController : Controller
	{
		private readonly DataManager dataManager;
		private readonly IWebHostEnvironment hostingEnvironment;
		private readonly ILogger<ServiceItemsController> _logger;
		public ServiceItemsController(DataManager dataManager, IWebHostEnvironment hostingEnvironment, ILogger<ServiceItemsController> logger)
		{
			this.dataManager = dataManager;
			this.hostingEnvironment = hostingEnvironment;
			_logger = logger;
		}

		public IActionResult Edit(Guid id)
		{
			var entity = id == default ? new ServiceItem() : dataManager.ServiceItems.GetServiceItemById(id);
			var codeWordsList = dataManager.TextFields.GetTitleList();
			ViewBag.CodeWordsList = codeWordsList;
			return View(entity);
		}


		[HttpPost]
		public IActionResult Edit(ServiceItem model, IFormFile? titleImageFile)
		{
			if (ModelState.IsValid)
			{
				_logger.LogInformation($"File Name: {titleImageFile?.FileName}");

				if (titleImageFile != null)
				{
					// Новое изображение загружено, обработаем его
					model.TitleImagePath = titleImageFile.FileName;

					using (var stream = new FileStream(Path.Combine(hostingEnvironment.WebRootPath, "img/photo/", titleImageFile.FileName), FileMode.Create))
					{
						titleImageFile.CopyTo(stream);
					}
				}

				ViewBag.CodeWordsList = dataManager.TextFields.GetTitleList();
				dataManager.ServiceItems.SaveServiceItem(model);
				return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
			}

			return View(model);
		}

		[HttpPost]
		public IActionResult Delete(Guid id)
		{
			dataManager.ServiceItems.DeleteServiceItem(id);
			return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
		}
	}
}
