using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Godel.AutoPartsStore.BusinessLogicLayer.Interfaces;
using Godel.AutoPartsStore.DAL.Models;
using Microsoft.AspNetCore.Authorization;

namespace Godel.AutoPartsStore.PartsStore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ICategoryService _categoryService;
        private IPartService _partService;
        public HomeController(ILogger<HomeController> logger, ICategoryService categoryService, IPartService partService)
        {
            _logger = (logger ?? throw new ArgumentNullException(nameof(logger)));
            _categoryService = (categoryService ?? throw new ArgumentNullException(nameof(categoryService)));
            _partService = (partService ?? throw new ArgumentNullException(nameof(partService)));
        }
        
        public IActionResult Index()
        {
            return View(_categoryService.GetAll());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Category()
        {
            return View(_categoryService.GetAll());
        }

        public async Task<IActionResult> Parts()
        {   
            return View(await _partService.GetFilteredList(null, string.Empty, string.Empty, 1, 5));
        }

        public async Task<IActionResult> FilteredParts(int id)
        {   
            return View("Parts", await _partService.GetFilteredList(id, string.Empty, string.Empty, 1, 5));
        }

        public async Task <IActionResult> Details(int id)
        {
            return View(await _partService.Get(id));
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
          await _partService.Delete(id);
          return RedirectToAction("Parts");
        }

        [Authorize(Roles = "admin")] 
        public IActionResult Create()
        {
            ViewData["categoryList"] = GetCategories();
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Create(Part part)
        {
            await _partService.Create(part);
            return RedirectToAction("Parts");
        }

        [Authorize(Roles = "admin")]    
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["categoryList"] = GetCategories();
            return View(await _partService.Get(id));
        }
               
        [HttpPost]
        public async Task<IActionResult> Edit(Part part)
        {
            await _partService.Update(part);
            return RedirectToAction("Parts");
        }

        public async Task<IActionResult> GetFilteredParts(string sortOrder, string search, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["PriceSortParm"] = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";
            ViewData["NameSortParm"] = sortOrder == "Name" ? "name_desc" : "Name";
            ViewData["CategorySortParm"] = sortOrder == "Category" ? "category_desc" : "Category";
            ViewData["ParmFilter"] = search;

            if (search != null)
            {
                pageNumber = 1;
            }
            else
            {
                search = currentFilter;
            }

            ViewData["CurrentFilter"] = search;
            int pageSize = 5;
            return View("Parts", await _partService.GetFilteredList(null, search, sortOrder, pageNumber ?? 1, pageSize));
        }

        private List<SelectListItem> GetCategories()
        {
            var result = new List<SelectListItem>();
            var categories = _categoryService.GetAll();
            foreach (var category in categories)
            {
                var item = new SelectListItem(category.Name, category.Id.ToString());
                result.Add(item);
            }
            return result;
        }
    }
}
