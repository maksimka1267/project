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

        public async Task<IActionResult> Edit(Guid id)
        {
            var entity = id == default ? new TextField() : await dataManager.TextFields.GetTextFieldByIdAsync(id);
            var codeWordsList = await dataManager.TextFields.GetDistinctTitlesAsync();
            ViewBag.CodeWordsList = codeWordsList;
            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TextField model)
        {
            if (ModelState.IsValid)
            {
                var codeWordsList = await dataManager.TextFields.GetDistinctTitlesAsync();
                ViewBag.CodeWordsList = codeWordsList;
                await dataManager.TextFields.SaveTextFieldAsync(model);
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
            }

            // Если модель невалидна, обновляем список слов для повторного отображения в выпадающем списке
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await dataManager.TextFields.DeleteTextFieldAsync(id);
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
        }
    }
}