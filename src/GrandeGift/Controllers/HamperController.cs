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



namespace GrandeGift.Controllers
{
    public class HamperController : Controller
    {
        private IDataService<Category> _categoryDataService;
        private IDataService<Hamper> _hamperDataService;
        private IHostingEnvironment _environment;

        public HamperController(IDataService<Category> categoryService, IDataService<Hamper> hamperService,
            IHostingEnvironment envService)
        {
            _categoryDataService = categoryService;
            _hamperDataService = hamperService;
            _environment = envService;
        }

        //(Admin) Create hamper
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HamperCreateViewModel vm, IFormFile file)
        {
            int theCategoryId = int.Parse(TempData["catId"].ToString());
            if (ModelState.IsValid)
            {
                Hamper hamper = new Hamper
                {
                    Name = vm.Name,
                    Details = vm.Details,
                    Price = vm.Price,
                    CategoryId = theCategoryId
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
                    hamper.Picture = User.Identity.Name + "/" + fileName;
                }// end file is to be uploaded

                _hamperDataService.Create(hamper);
                return RedirectToAction("Details", "Category", new { id = theCategoryId });
            }
            return View(vm);
        }

        //Details page
        public IActionResult Details(int id)
        {
            //get the single category from DB
            Hamper hamper = _hamperDataService.GetSingle(h => h.HamperId == id);

            HamperDetailsViewModel vm = new HamperDetailsViewModel
            {
                HamperId = hamper.HamperId,
                Name = hamper.Name,
                Details = hamper.Details,
                Price = hamper.Price,
                Picture = hamper.Picture
            };

            return View(vm);
        }

        //(admin) Update hamper
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
        {
            Hamper hamper = _hamperDataService.GetSingle(h => h.HamperId == id);
            HamperUpdateViewModel vm = new HamperUpdateViewModel
            {
                HamperId = hamper.HamperId,
                Name = hamper.Name,
                Details = hamper.Details,
                Price = hamper.Price,
                Hidden = hamper.Hidden!=0,
                Picture = hamper.Picture
            };
            return View(vm);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(HamperUpdateViewModel vm, IFormFile file)
        {
            int theCategoryId = int.Parse(TempData["catId"].ToString());
            if (ModelState.IsValid)
            {
                Hamper hamp = new Hamper
                {
                    HamperId = vm.HamperId,
                    Name = vm.Name,
                    Details = vm.Details,
                    Price = vm.Price,
                    Hidden = vm.Hidden ? 1:0,
                    Picture = vm.Picture,
                    CategoryId = theCategoryId
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
                    hamp.Picture = User.Identity.Name + "/" + fileName;
                }// end file is to be uploaded

                _hamperDataService.Update(hamp);
                return RedirectToAction("Details", "Category", new { id = theCategoryId });
            }
            return View(vm);
        }
    }
}
