using Microsoft.AspNetCore.Mvc;
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
            PageItemDto model;

            if (id == default)
            {
                model = new PageItemDto();
            }
            else
            {
                var entity = await dataManager.TextFields.GetTextFieldByIdAsync(id);
                var textEntity = await dataManager.TextModels.GetTextModelByIdAsync(entity.Text);

                model = new PageItemDto
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Child = entity.Child,
                    DateAdded = DateTime.Now,
                    Father = entity.Father,
                    Number = entity.Number,
                    View = entity.View,
                    Index= entity.Index,
                    Text = textEntity?.Text // Если текст есть — загружаем, иначе оставляем null
                };
            }

            ViewBag.CodeWordsList = await dataManager.TextFields.GetDistinctTitlesWithIdsAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PageItemDto model)
        {
            if (ModelState.IsValid)
            {
                var codeWordsList = await dataManager.TextFields.GetDistinctTitlesWithIdsAsync();
                ViewBag.CodeWordsList = codeWordsList;

                var page = model.Id == Guid.Empty
                    ? new PageItem() {} // Новый объект
                    : await dataManager.TextFields.GetTextFieldByIdAsync(model.Id); // Существующий объект

                page.Title = model.Title;
                page.Child = model.Child;
                page.Father = model.Father;
                page.Number = model.Number;
                page.Index = model.Index;
                page.View = model.View;

                if (!string.IsNullOrEmpty(model.Text))
                {
                    if (page.Text == Guid.Empty) // Если нет привязанного текста, создаем новый TextModel
                    {
                        var textModel = new TextModel {Text = model.Text };
                        await dataManager.TextModels.SaveTextModelAsync(textModel);
                        page.Text = textModel.Id;
                    }
                    else // Если есть TextModel, обновляем текст
                    {
                        var existingText = await dataManager.TextModels.GetTextModelByIdAsync(page.Text);
                        if (existingText != null)
                        {
                            existingText.Text = model.Text;
                            await dataManager.TextModels.SaveTextModelAsync(existingText);
                        }
                    }
                }

                await dataManager.TextFields.SaveTextFieldAsync(page);
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var page = await dataManager.TextFields.GetTextFieldByIdAsync(id);
            if (page != null)
            {
                if (page.Text != Guid.Empty)
                {
                    await dataManager.TextModels.DeleteTextModelAsync(page.Text); // Удаляем текст, если он есть
                }
                await dataManager.TextFields.DeleteTextFieldAsync(id);
            }
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
        }
    }
}
