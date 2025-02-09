using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly AppDbContext _context;
        public HomeController(DataManager dataManager, AppDbContext context)
        {
            this.dataManager = dataManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ServiceEvents = await dataManager.NewsItems.GetTop3NewsAsync();
            var page = await dataManager.TextFields.GetAllPageModelForIndexAsync();

            // Если в коллекции page есть именованные элементы, замените индексы на их имена.
            var entrantPage = page.FirstOrDefault(p => p.Title == "Вступнику");
            var specialtyPage = page.FirstOrDefault(p => p.Title == "Спеціальності");
            var studentPage = page.FirstOrDefault(p => p.Title == "Студенту");
            var currentPage = page.FirstOrDefault(p => p.Title == "Актуальне");

            if (entrantPage != null)
            {
                ViewBag.ServiceEntrant = await dataManager.ServiceItems.GetServiceByFatherWithBannerAsync(entrantPage.Id);
            }

            if (specialtyPage != null)
            {
                ViewBag.ServiceSpecialties = await dataManager.ServiceItems.GetServiceByFatherAsync(specialtyPage.Id);
            }

            if (studentPage != null)
            {
                ViewBag.ServiceStudent = await dataManager.ServiceItems.GetServiceByFatherWithBannerAsync(studentPage.Id);
            }

            if (currentPage != null)
            {
                var article = await dataManager.ServiceItems.GetServiceByFatherAsync(currentPage.Id);

                // Получаем список всех Id
                var articleIds = article.Select(a => a.Text).ToList();

                // Запрашиваем все данные одним запросом асинхронно
                var texts = await _context.TextModels
                    .Where(t => articleIds.Contains(t.Id))
                    .Select(t => t.Text)
                    .ToListAsync();  // используем ToListAsync для асинхронного выполнения запроса

                ViewBag.ServiceCurrent = texts;

                var t = await dataManager.TextFields.GetTextFieldByIdAsync(currentPage.Id);
                Console.WriteLine($"Такой айди передаем страницы {t.Title}:{currentPage}");
            }



            return View();
        }


        public async Task<IActionResult> About(string title)
        {
            var page = await dataManager.TextFields.GetTextByTitleAsync(title);
            var pages = await dataManager.TextFields.GetTextByFatherAsync(page.Id);
            ViewBag.ServiceAbout = await dataManager.ServiceItems.GetServiceByFatherAsync(page.Id);
            ViewBag.ServiceCommission = await dataManager.ServiceItems.GetServiceByFatherAsync(pages[0].Id);
            ViewBag.ServiceAdmin = await dataManager.ServiceItems.GetServiceByFatherAsync(pages[1].Id);
            ViewBag.ServiceTrade = await dataManager.ServiceItems.GetServiceByFatherAsync(pages[2].Id);
            return View();
        }
        public async Task<IActionResult> Admissions(string title)
        {

            var page = await dataManager.TextFields.GetTextByTitleAsync(title);
            ViewBag.TextFields = page;
            ViewBag.ServiceItems = await dataManager.ServiceItems.GetServiceByFatherAsync(page.Id);
            ViewBag.Title = title;
            return View();
        }


        public async Task<IActionResult> Blog(string title)
        {
            var page = await dataManager.TextFields.GetTextByTitleAsync(title);
            ViewBag.ServiceItems = await dataManager.ServiceItems.GetServiceByFatherAsync(page.Id);
            ViewBag.Title = title;
            return View();
        }

        public async Task<IActionResult> Event(string title, int page = 1)
        {
            const int pageSize = 10;
            var father = await dataManager.TextFields.GetTextByTitleAsync(title);
            var totalItems = await dataManager.NewsItems.GetTotalNewsItemCountByFatherAsync(father.Id);
            ViewBag.ServiceItems = await dataManager.NewsItems.GetPagedNewsItemsByFatherAsync(null, page, pageSize);
            ViewBag.Title = title;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            return View();
        }
        public async Task<IActionResult> Faculty(string title)
        {
            var page = await dataManager.TextFields.GetTextByTitleAsync(title);
            ViewBag.TextFields = await dataManager.TextFields.GetTextByFatherAsync(page.Id);
            ViewBag.ServiceItems = await dataManager.ServiceItems.GetServiceByFatherAsync(page.Id);
            ViewBag.Title = title;
            return View();
        }
    }
}
