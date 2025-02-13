using Microsoft.AspNetCore.Mvc;
using project.Domain;

namespace project.Controllers
{
    public class ServicesController : Controller
    {
        private readonly DataManager dataManager;
        private readonly ILogger<ServicesController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ServicesController(IWebHostEnvironment webHostEnvironment, DataManager dataManager, ILogger<ServicesController> logger)
        {
            this.dataManager = dataManager;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;   
        }
        public async Task<IActionResult> Index(Guid id)
        {
            var article = await dataManager.ServiceItems.GetServiceItemByIdAsync(id);
            var photo = await dataManager.PhotoItems.GetPhotoModelByIdAsync(article.TitleImage);
            var text = await dataManager.TextModels.GetTextModelByIdAsync(article.Text);
            ViewBag.Photo = photo.ImageData;
            ViewBag.Text = text.Text;
            var news = await dataManager.NewsItems.GetTop3NewsByFatherAsync(article.Id);
            if (news == null || !news.Any())  // Проверка на null или пустой список
            {
                news = await dataManager.NewsItems.GetTop3NewsAsync();
            }
            ViewBag.News = news;
            return View("Show",article);
        }
        public async Task<IActionResult> Header(string title)
        {
            var serviceItems =await dataManager.ServiceItems.GetServiceItemByTitleAsync(title);
            ViewBag.ServiceItem = serviceItems;
            var news = await dataManager.NewsItems.GetTop3NewsByFatherAsync(serviceItems.Id);
            if (news == null || !news.Any())  // Проверка на null или пустой список
            {
                news = await dataManager.NewsItems.GetTop3NewsAsync();
            }
            ViewBag.News = news;
            return View("Show", serviceItems);
        }
        [HttpGet]
        public async Task<IActionResult> GetImage(Guid id)
        {
            var imageData = await dataManager.PhotoItems.GetImageDataByIdAsync(id);

            if (imageData == null || imageData.Length == 0)
            {
                return NotFound(); // Если изображения нет, возвращаем 404
            }

            return File(imageData, "image/jpeg"); // Отправляем изображение клиенту
        }
        [HttpGet]
        public async Task<IActionResult> GetText(Guid id)
        {
            var text = await dataManager.TextModels.GetTextTextModelByIdAsync(id);

            if (string.IsNullOrEmpty(text))
            {
                return NotFound(); // Если текста нет, возвращаем 404
            }

            return Content(text, "text/html"); // Возвращаем текст в формате HTML
        }

    }
}