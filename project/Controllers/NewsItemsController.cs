using Microsoft.AspNetCore.Mvc;
using project.Domain;

namespace project.Controllers
{
    public class NewsItemsController:Controller
    {
        private readonly DataManager dataManager;
        public NewsItemsController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        public async Task<IActionResult> Index(Guid id)
        {
            var article = await dataManager.NewsItems.GetNewsItemByIdAsync(id);
            var photo = await dataManager.PhotoItems.GetPhotoModelByIdAsync(article.TitleImage);
            var text = await dataManager.TextModels.GetTextModelByIdAsync(article.Text);
            ViewBag.Photo = photo.ImageData;
            ViewBag.Text = text.Text;
            ViewBag.News = await dataManager.NewsItems.GetTop3NewsAsync();
            return View("Show", article);
        }
    }
}
