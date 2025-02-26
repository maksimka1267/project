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

        public async Task<IActionResult> Edit(Guid id)
        {
            ArticleItemDto model;

            if (id == default)
            {
                model = new ArticleItemDto();
            }
            else
            {
                var entity = await dataManager.ServiceItems.GetServiceItemByIdAsync(id);
                var textEntity = await dataManager.TextModels.GetTextModelByIdAsync(entity.Text);
                var photoEntity = await dataManager.PhotoItems.GetPhotoModelByIdAsync(entity.TitleImage);
                model = new ArticleItemDto
                {
                    Id = entity.Id,
                    Title=entity.Title,
                    TitleImage=photoEntity.ImageData,
                    Father=entity.Father,
                    MakePage=entity.MakePage,
                    ShowBanners=entity.ShowBanners,
                    Subtitle=entity.Subtitle,
                    DateAdded= DateTime.Now,
                    Text = textEntity?.Text // Если текст есть — загружаем, иначе оставляем null
                };
            }

            ViewBag.CodeWordsList = await dataManager.TextFields.GetDistinctTitlesWithIdsAsync();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ArticleItemDto model, IFormFile? titleImageFile)
        {
            if (ModelState.IsValid)
            {
                var test = await dataManager.ServiceItems.GetServiceItemByIdAsync(model.Id);
                ArticleItem article = new ArticleItem();
                if (test == null)
                {
                    article.Title = model.Title;
                    article.ShowBanners = model.ShowBanners;
                    article.Subtitle = model.Subtitle;
                    article.Id = model.Id;
                    article.Father = model.Father;
                    article.MakePage = model.MakePage;
                }
                else
                {
                    article.Id = test.Id;
                    article.Title = model.Title;
                    article.ShowBanners = model.ShowBanners;
                    article.Subtitle = model.Subtitle;
                    article.Father = model.Father;
                    article.TitleImage = test.TitleImage;
                    article.Text = test.Text;
                    article.MakePage = model.MakePage;
                    article.DateAdded = DateTime.Now;
                }
                // Если новое изображение загружено, сохраняем его
                if (titleImageFile != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        PhotoModel photo = new PhotoModel();
                        await titleImageFile.CopyToAsync(memoryStream);
                        photo.ImageData = memoryStream.ToArray();
                        await dataManager.PhotoItems.SavePhotoModelAsync(photo);
                        article.TitleImage = photo.Id;
                    }
                }
                else if (model.TitleImage == null || model.TitleImage.Length == 0)
                {
                    // Если новое изображение не загружено и модель не имеет изображения, используем стандартное
                    var defaultImagePath = Path.Combine(hostingEnvironment.WebRootPath, "img/else.jpg");
                    if (System.IO.File.Exists(defaultImagePath))
                    {
                        PhotoModel photo = new PhotoModel();
                        photo.ImageData = await System.IO.File.ReadAllBytesAsync(defaultImagePath);
                        await dataManager.PhotoItems.SavePhotoModelAsync(photo);
                        article.TitleImage = photo.Id;
                    }
                }
                var codeWordsList = await dataManager.TextFields.GetDistinctTitlesWithIdsAsync();
                ViewBag.CodeWordsList = codeWordsList;
                var text = await dataManager.TextModels.GetTextModelByIdAsync(article.Text);
                if (text == null)
                {
                    text = new TextModel { Text = model.Text };
                    await dataManager.TextModels.SaveTextModelAsync(text);
                }
                else
                {
                    text.Text = model.Text;
                    await dataManager.TextModels.UpdateTextModelAsync(text);
                }
                article.Text = text.Id;
                await dataManager.ServiceItems.SaveServiceItemAsync(article);
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var page = await dataManager.ServiceItems.GetServiceItemByIdAsync(id);
            if (page.Text != Guid.Empty)
            {
                await dataManager.TextModels.DeleteTextModelAsync(page.Text);
            }
            if(page.TitleImage!= Guid.Empty)
            {
                await dataManager.PhotoItems.DeletePhotoModelAsync(page.TitleImage);
            }
            await dataManager.ServiceItems.DeleteServiceItemAsync(id);
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
        }
        public async Task<IActionResult> ToggleActive(Guid id)
        {
            await dataManager.ServiceItems.ToggleActiveAsync(id); // Вызываем метод репозитория
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController()); // Перенаправляем обратно на список статей
        }

    }
}