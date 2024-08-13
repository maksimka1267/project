using Microsoft.AspNetCore.Mvc;
using project.Domain;

namespace project.Controllers
{
    public class PhotoController : Controller
    {
        private readonly DataManager dataManager;
        public PhotoController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        [HttpGet]
        public IActionResult GetImage(Guid id)
        {
            var photo = dataManager.PhotoFields.GetPhotoById(id);

            if (photo != null && photo.PhotoData != null)
            {
                return File(photo.PhotoData, "image/jpeg"); // Используйте правильный MIME-тип
            }

            // Если изображение не найдено или его данные отсутствуют, вернуть пустой результат
            return NoContent();
        }
    }
}
