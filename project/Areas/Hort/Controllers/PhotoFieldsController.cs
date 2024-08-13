using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http; // Добавлено для IFormFile
using project.Domain;
using project.Domain.Entities;
using project.Domain.Repositories.Abstract;
using project.Service;

namespace project.Areas.Hort.Controllers
{
	[Area("Hort")]
	public class PhotoFieldsController : Controller
	{
		private readonly DataManager dataManager;
		private readonly ILogger<PhotoFieldsController> _logger;

		public PhotoFieldsController(DataManager dataManager, ILogger<PhotoFieldsController> logger)
		{
			this.dataManager = dataManager;
			this._logger = logger;
		}

		public IActionResult Edit(Guid id)
		{
			var entity = id == default ? new PhotoField() : dataManager.PhotoFields.GetPhotoById(id);
			return View(entity);
		}

		[HttpPost]
		public IActionResult Edit(PhotoField model, IFormFile photoFile)
		{
			if (ModelState.IsValid)
			{
				_logger.LogInformation($"File Name: {photoFile?.FileName}");

				// Конвертируем содержимое фотографии в массив байтов и сохраняем в свойство PhotoData
				using (var memoryStream = new MemoryStream())
				{
					photoFile.CopyTo(memoryStream);
					model.PhotoData = memoryStream.ToArray();
				}

				// Остальной код вашего метода
				model.TitleImagePath = photoFile.FileName;

				// Обработка загрузки фотографии и сохранение в базу данных
				dataManager.PhotoFields.SavePhoto(model);

				// Редирект или другая логика в случае успешной загрузки
				return RedirectToAction("Index", "Home");
			}

			// Если ModelState не валидна, возвращаем представление с ошибками
			return View(model);
		}



		[HttpPost]
		public IActionResult Delete(Guid id)
		{
			dataManager.PhotoFields.DeletePhoto(id);
			return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
		}
	}
}
