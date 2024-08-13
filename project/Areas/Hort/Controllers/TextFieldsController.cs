using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using project.Domain;
using project.Domain.Entities;
using project.Service;
namespace project.Areas.Hort.Controllers
{
    [Area("Hort")]
    public class TextFieldsController : Controller
    {
        private readonly DataManager dataManager;
        public TextFieldsController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public IActionResult Edit(string codeWord)
        {
            var entity = string.IsNullOrEmpty(codeWord)
                ? new TextField()
                : dataManager.TextFields.GetTextByCodeWord(codeWord);
            var codeWordsList = dataManager.TextFields.GetTitleList();
            ViewBag.CodeWordsList = codeWordsList;
            return View(entity);
        }

        [HttpPost]
        public IActionResult Edit(TextField model)
        {
            if (ModelState.IsValid)
            {
                var codeWordsList = dataManager.TextFields.GetTitleList();
                ViewBag.CodeWordsList = codeWordsList;
                dataManager.TextFields.SaveTextField(model);
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
            }

            // Если модель невалидна, обновляем список слов для повторного отображения в выпадающем списке
            return View(model);
        }


        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            dataManager.TextFields.DeleteTextField(id);
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
        }
    }
}
