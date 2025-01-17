using Microsoft.AspNetCore.Mvc;
using project.Domain;
using project.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataManager dataManager;

        public HomeController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ServiceEvents = await dataManager.ServiceItems.GetTop3ServiceByFatherAsync("Події");
            ViewBag.ServiceEntrant = await dataManager.ServiceItems.GetServiceByFatherWithBannerAsync("Вступнику");
            ViewBag.ServiceStudent = await dataManager.ServiceItems.GetServiceByFatherWithBannerAsync("Студенту");
            ViewBag.ServiceSpecialties = await dataManager.ServiceItems.GetServiceByFatherAsync("Спеціальності");
            ViewBag.ServiceCurrent = await dataManager.ServiceItems.GetServiceByFatherAsync("Актуальне");
            return View();
        }

        public async Task<IActionResult> About(string father)
        {
            ViewBag.ServiceAbout = await dataManager.ServiceItems.GetServiceByFatherAsync("Про колледж");
            ViewBag.ServiceCommission = await dataManager.ServiceItems.GetServiceByFatherAsync("Циклові комісії");
            ViewBag.ServiceAdmin = await dataManager.ServiceItems.GetServiceByFatherAsync("Адміністрація");
            ViewBag.ServiceTrade = await dataManager.ServiceItems.GetServiceByFatherAsync("Профспілкова організація");
            return View();
        }
        public async Task<IActionResult> Admissions(string title)
        {

            ViewBag.TextFields = await dataManager.TextFields.GetTextByTitleAsync(title);
            ViewBag.ServiceItems = await dataManager.ServiceItems.GetServiceByFatherAsync(title);
            ViewBag.Title = title;
            return View();
        }


        public async Task<IActionResult> Blog(string title)
        {
            ViewBag.ServiceItems = await dataManager.ServiceItems.GetServiceByFatherAsync(title);
            ViewBag.Title = title;
            return View();
        }

        public async Task<IActionResult> Event(string title, int page = 1)
        {
            const int pageSize = 10;
            var father = await dataManager.TextFields.GetTextByTitleAsync(title);
            var totalItems = await dataManager.ServiceItems.GetTotalServiceItemCountByFatherAsync(father.Title);
            ViewBag.ServiceItems = await dataManager.ServiceItems.GetPagedServiceItemsByFatherAsync(father.Title, page, pageSize);
            ViewBag.Title = title;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            return View();
        }



        public async Task<IActionResult> Faculty(string title)
        {
            var page = await dataManager.TextFields.GetTextByTitleAsync(title);
            ViewBag.TextFields = await dataManager.TextFields.GetTextByFatherAsync(page.Title);
            ViewBag.ServiceItems = await dataManager.ServiceItems.GetServiceByFatherAsync(page.Title);
            ViewBag.Title = title;
            return View();
        }
    }
}
