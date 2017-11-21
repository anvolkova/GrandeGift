using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeGift.Services;
using GrandeGift.Models;
using GrandeGift.ViewModels;
using Microsoft.AspNetCore.Authorization;
// these namespaces for uploading picture and creating subfolder (dynamically)
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeGift.Controllers
{
    public class CategoryController : Controller
    {
        private IDataService<Category> _categoryDataService;
        private IDataService<Hamper> _hamperDataService;
        private IHostingEnvironment _environment;
        public CategoryController(IDataService<Category> categoryService, IDataService<Hamper> hamperService,
            IHostingEnvironment envService)
        {
            _categoryDataService = categoryService;
            _hamperDataService = hamperService;
            _environment = envService;
        }

        //(Admin) Create category
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CategoryCreateViewModel vm, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                Category c = new Category
                {
                    Name = vm.Name,
                    Details = vm.Details
                };
                //if file is to be uploaded
                if (file != null)
                {
                    //upload server-side path
                    var uploadPath = Path.Combine(_environment.WebRootPath, "uploads");
                    //create folder with username (dynamically). If folder already exists - no code executed
                    Directory.CreateDirectory(Path.Combine(uploadPath, User.Identity.Name));
                    string fileName = FileNameHelper.GetNameFormat(file.FileName);
                    //copy file from client side to server
                    using (var fileStream = new FileStream(Path.Combine(uploadPath, User.Identity.Name, fileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    //add path to the database (category object)
                    c.Picture = User.Identity.Name + "/" + fileName;
                }// end file is to be uploaded

                _categoryDataService.Create(c);
                return RedirectToAction("Index", "Home");
            }
            return View(vm);
        }

        //Details page     
        [HttpGet]
        public IActionResult Details(int id)
        {
            //add the CategoryId to the session so we can use it in Hamper/Create controller
            TempData["catId"] = id.ToString(); //TempData takes only strings
            double minPrice, maxPrice;
            bool hasMin = Double.TryParse(HttpContext.Request.Query["MinPrice"].ToString(), out minPrice);
            bool hasMax = Double.TryParse(HttpContext.Request.Query["MaxPrice"].ToString(), out maxPrice);

            //get the single category from DB
            Category category = _categoryDataService.GetSingle(c => c.CategoryId == id);

            //get the list of products for this category
            bool isAdmin = User.IsInRole("Admin");
            IEnumerable<Hamper> hamperList = _hamperDataService.Query(p =>
                    p.CategoryId == id &&
                    (isAdmin || p.Hidden == 0)
                    && (!hasMin || p.Price >= minPrice)
                    && (!hasMax || p.Price <= maxPrice));

            //create vm
            CategoryDetailsViewModel vm = new CategoryDetailsViewModel
            {
                Name = category.Name,
                Details = category.Details,
                Hampers = hamperList,
                Total = hamperList.Count(),
                MinPrice = hasMin ? minPrice.ToString() : "",
                MaxPrice = hasMax ? maxPrice.ToString() : "",
            };

            return View(vm);
        }
        
        //(admin) Update category
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
        {
            Category category = _categoryDataService.GetSingle(c => c.CategoryId == id);
            CategoryUpdateViewModel vm = new CategoryUpdateViewModel
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Details = category.Details,
                Picture = category.Picture
            };
            return View(vm);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(CategoryUpdateViewModel vm, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                Category c = new Category
                {
                    CategoryId = vm.CategoryId,
                    Name = vm.Name,
                    Details = vm.Details,
                    Picture = vm.Picture
                };
                //if file is to be uploaded
                if (file != null)
                {
                    //upload server-side path
                    var uploadPath = Path.Combine(_environment.WebRootPath, "uploads");
                    //create folder with username (dynamically). If folder already exists - no code executed
                    Directory.CreateDirectory(Path.Combine(uploadPath, User.Identity.Name));
                    string fileName = FileNameHelper.GetNameFormat(file.FileName);
                    //copy file from client side to server
                    using (var fileStream = new FileStream(Path.Combine(uploadPath, User.Identity.Name, fileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    //add path to the database (category object)
                    c.Picture = User.Identity.Name + "/" + fileName;
                }// end file is to be uploaded

                _categoryDataService.Update(c);
                return RedirectToAction("Details", "Category", new { id = c.CategoryId });
            }
            return View(vm);
        }
    }
}
