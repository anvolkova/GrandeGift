using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeGift.Services;
using GrandeGift.Models;
using GrandeGift.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeGift.Controllers
{
    public class HomeController : Controller
    {
        private IDataService<Category> _categoryService;
        public HomeController(IDataService<Category> categoryService)
        {
            _categoryService = categoryService;
        }      
        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _categoryService.GetAll();
            HomeIndexViewModel vm = new HomeIndexViewModel
            {
                Categories = categoryList
            };
            return View(vm);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
