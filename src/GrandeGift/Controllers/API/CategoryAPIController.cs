using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GrandeGift.Services;
using GrandeGift.Models;
using System.Net;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeGift.Controllers.API
{
    public class CategoryAPIController : Controller
    {
        private IDataService<Category> _categoryDataService;
        private IDataService<Hamper> _hamperDataService;

        public CategoryAPIController(IDataService<Category> categoryService, IDataService<Hamper> hamperService)
        {
            _categoryDataService = categoryService;
            _hamperDataService = hamperService;
        }

        // web method - get all categories
        [HttpGet("api/categories")]
        public JsonResult GetCategories()
        {
            try
            {
                IEnumerable<Category> categories = _categoryDataService.GetAll();
                var cats = new List<string>();
                foreach (var item in categories)
                {
                    cats.Add(item.Name);
                }                
                return Json(cats);
            }
            catch(Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = ex.Message });
            }
        }

        // web method - get all hampers
        [HttpGet("api/hampers")]
        public JsonResult GetAllHampers()
        {
            try
            {
                IEnumerable<Hamper> hamperList = _hamperDataService.GetAll();
                return Json(hamperList);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = ex.Message });
            }
        }

        // web method - get hampers by category name
        [HttpGet("api/products")]
        public JsonResult GetHampersByCategory(string catName)
        {
            try
            {
                Category category = _categoryDataService.GetSingle(c => c.Name == catName);
                if(category != null)
                {
                    IEnumerable<Hamper> hampers = _hamperDataService.Query(h => h.CategoryId == category.CategoryId);                  
                    return Json(hampers);
                }
                else
                {
                    return Json(new { message = "cannot find this category" });
                }
            }
            catch(Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = ex.Message });
            }
        }
    }
}
