using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project.Domain;
using project.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.IO;
using project.Service;

namespace project.Areas.Hort.Controllers
{
	[Area("Hort")]
	[Authorize]
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
        public async Task<IActionResult> Edit(ServiceItem model, IFormFile? titleImageFile)
        {
            if (ModelState.IsValid)
            {
                // Если новое изображение загружено, сохраняем его
                if (titleImageFile != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await titleImageFile.CopyToAsync(memoryStream);
                        model.TitleImage = memoryStream.ToArray();
                    }
                }
                else if (model.TitleImage == null || model.TitleImage.Length == 0)
                {
                    // Если новое изображение не загружено и модель не имеет изображения, используем стандартное
                    var defaultImagePath = Path.Combine(hostingEnvironment.WebRootPath, "img/else.jpg");
                    if (System.IO.File.Exists(defaultImagePath))
                    {
                        model.TitleImage = await System.IO.File.ReadAllBytesAsync(defaultImagePath);
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
