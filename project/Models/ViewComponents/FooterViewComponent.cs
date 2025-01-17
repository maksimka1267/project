using Microsoft.AspNetCore.Mvc;
using project.Domain;

namespace project.Models.ViewComponents
{
	public class HeaderViewComponent : ViewComponent
	{
		private readonly DataManager dataManager;
		public HeaderViewComponent(DataManager dataManager)
		{
			this.dataManager = dataManager;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
            var textFields = await dataManager.TextFields.GetTextFieldsAsync();  // Используем await
            return View("Default", textFields);  // Передаем данные в представление
        }
	}
}
