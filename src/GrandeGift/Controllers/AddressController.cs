using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GrandeGift.Models;
using GrandeGift.Services;
using GrandeGift.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace GrandeGift.Controllers
{
    public class AddressController : Controller
    {
        private IDataService<Address> _addressDataService;

        public AddressController(IDataService<Address> addressService)
        {
            _addressDataService = addressService;
        }

        //Create address
        [HttpGet]
        [Authorize(Roles ="Customer")]
        public IActionResult Create()
        {            
            return View();
        }
        [HttpPost]
        [Authorize(Roles ="Customer")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AddressCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Address address = new Address
                {
                    Name = vm.Name,
                    Street = vm.Street,                    
                    City = vm.City,
                    Region = vm.Region,
                    PostCode = vm.PostCode,
                    Country = vm.Country,
                    Username = User.Identity.Name                    
                };
                _addressDataService.Create(address);
                return RedirectToAction("List", "Address");
            }
            return View(vm);
        }

        //Update address
        [HttpGet]
        [Authorize(Roles ="Customer")]
        public IActionResult Update(int id)
        {
            Address address = _addressDataService.GetSingle(a => a.AddressId == id);
            AddressUpdateViewModel vm = new AddressUpdateViewModel
            {
                AddressId = address.AddressId,
                Name = address.Name,
                Street = address.Street,
                City = address.City,
                Region = address.Region,
                PostCode = address.PostCode,
                Country = address.Country
            };
            return View(vm);
        }
        [HttpPost]
        [Authorize(Roles ="Customer")]
        [ValidateAntiForgeryToken]
        public IActionResult Update(AddressUpdateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Address a = new Address
                {
                    AddressId = vm.AddressId,
                    Name = vm.Name,
                    Street = vm.Street,
                    City = vm.City,
                    Region = vm.Region,
                    PostCode = vm.PostCode,
                    Country = vm.Country,
                    Username = User.Identity.Name
                };
                _addressDataService.Update(a);                
                return RedirectToAction("List", "Address");
            }
            return View(vm);
        }

        //List addresses
        [Authorize(Roles = "Customer")]
        public IActionResult List()
        {
            IEnumerable<Address> addressList = _addressDataService.Query(a => a.Username == User.Identity.Name && a.Removed == 0);
            AddressListViewModel vm = new AddressListViewModel
            {
                Addresses = addressList
            };
            return View(vm);
        }

        //remove address
        [HttpGet]
        [Authorize(Roles ="Customer")]
        public IActionResult Remove(int id)
        {
            Address address = _addressDataService.GetSingle(a => a.AddressId == id);
            AddressRemoveViewModel vm = new AddressRemoveViewModel
            {
                AddressId = address.AddressId,
                Street = address.Street
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles ="Customer")]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(AddressRemoveViewModel vm)
        {
            Address adrs = _addressDataService.GetSingle(a => a.AddressId == vm.AddressId);
            adrs.Removed = 1;                       
            _addressDataService.Update(adrs);
            return RedirectToAction("List", "Address");
        }
    }
}
