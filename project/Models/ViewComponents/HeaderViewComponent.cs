using Microsoft.AspNetCore.Mvc;
using project.Domain;

namespace project.Models.ViewComponents
{
	public class FooterViewComponent : ViewComponent
	{
		private readonly DataManager dataManager;
		public FooterViewComponent(DataManager dataManager)
		{
			this.dataManager = dataManager;
		}
		public Task<IViewComponentResult> InvokeAsync()
		{
			return Task.FromResult((IViewComponentResult)View("Default", dataManager.TextFields.GetTextFields()));
		}
	}
}
