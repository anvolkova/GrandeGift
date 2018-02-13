using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GrandeGift.Models;
using GrandeGift.Services;
using GrandeGift.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace GrandeGift.Controllers
{
    public class OrderController : Controller
    {
        private IDataService<Hamper> _hamperDataService;
        private IDataService<OrderLine> _orderlineDataService;
        private IDataService<Order> _orderDataService;
        private IDataService<Address> _addressDataService;
        public OrderController(IDataService<Hamper> hamperService,
            IDataService<OrderLine> orderlineService,
            IDataService<Order> orderService,
            IDataService<Address> addressService)
        {
            _hamperDataService = hamperService;
            _orderlineDataService = orderlineService;
            _orderDataService = orderService;
            _addressDataService = addressService;
        }

        private Order GetOrCreateCart()
        {
            IEnumerable<Order> orderCarts = _orderDataService.Query(o => o.Username == User.Identity.Name && o.Status == Order.OrderStatus.Cart);
            if (orderCarts.Any())
            {
                return orderCarts.First();
            }
            Order newOrderCart = new Order
            {
                Username = User.Identity.Name,
                Status = Order.OrderStatus.Cart,
                TotalPrice = 0
            };
            _orderDataService.Create(newOrderCart);
            return newOrderCart;
        }

        private double CartTotalPrice(IEnumerable<OrderLine> ol)
        {
            double total = 7.5;
            foreach (var i in ol)
            {
                total += i.UnitPrice * i.Quantity;
            }
            return total;
        }

        //Cart
        [HttpGet]
        [Authorize(Roles = "Customer, Admin")]
        public IActionResult Cart(int hamperId)
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            Order orderCart = GetOrCreateCart();

            Hamper hamper = _hamperDataService.GetSingle(h => h.HamperId == hamperId);
            if (hamper != null)
            {
                OrderLine orderLine = new OrderLine
                {
                    HamperId = hamperId,
                    OrderId = orderCart.OrderId,
                    Quantity = 1,
                    UnitPrice = hamper.Price,
                    HamperName = hamper.Name
                };
                _orderlineDataService.Create(orderLine);
                return RedirectToAction("Cart", "Order");
            }
            else
            {
                IEnumerable<OrderLine> orderLines = _orderlineDataService.Query(o => o.OrderId == orderCart.OrderId);
                OrderCartViewModel vm = new OrderCartViewModel
                {
                    OrderLines = orderLines,
                    TotalPrice = CartTotalPrice(orderLines)
                };
                return View(vm);
            }
        }
        //If we want to change items quantity
        [HttpPost]
        [Authorize(Roles = "Customer")]
        [ValidateAntiForgeryToken]
        public IActionResult Cart()
        {
            Order orderCart = GetOrCreateCart();
            IEnumerable<OrderLine> orderLines = _orderlineDataService.Query(o => o.OrderId == orderCart.OrderId);

            foreach (var i in orderLines)
            {
                int qty;
                if (int.TryParse(HttpContext.Request.Form[i.OrderLineId.ToString()].ToString(), out qty))
                {
                    if (qty != 0)
                    {
                        i.Quantity = qty;
                        _orderlineDataService.Update1(i);
                    }
                    else
                    {
                        i.Quantity = 1;
                        _orderlineDataService.Update1(i);
                    }
                }

            }
            _orderlineDataService.Saving();
            return RedirectToAction("Cart", "Order");
        }

        //Add delivery address to order
        [HttpGet]
        [Authorize(Roles = "Customer")]
        public IActionResult Addresses()
        {
            IEnumerable<Address> addressList = _addressDataService.Query(a => a.Username == User.Identity.Name && a.Removed == 0);
            OrderAddressesViewModel vm = new OrderAddressesViewModel
            {
                Addresses = addressList
            };
            return View(vm);
        }

        //Check the order details and place it
        [HttpGet]
        [Authorize(Roles = "Customer")]
        public IActionResult Checkout(int addressId)
        {
            Address address = _addressDataService.GetSingle(a => a.AddressId == addressId);
            Order orderCart = GetOrCreateCart();
            IEnumerable<OrderLine> orderLines = _orderlineDataService.Query(o => o.OrderId == orderCart.OrderId);

            OrderCheckoutViewModel vm = new OrderCheckoutViewModel
            {
                Address = address,
                AddressId = address.AddressId,
                OrderLines = orderLines,
                TotalPrice = CartTotalPrice(orderLines)
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(OrderCheckoutViewModel vm)
        {
            Order orderCart = GetOrCreateCart();
            orderCart.AddressId = vm.AddressId;
            orderCart.Status = Order.OrderStatus.Placed;
            IEnumerable<OrderLine> orderLines = _orderlineDataService.Query(o => o.OrderId == orderCart.OrderId);
            orderCart.TotalPrice = CartTotalPrice(orderLines);
            orderCart.OrderDate = DateTime.Now;

            _orderDataService.Update(orderCart);
            return RedirectToAction("Index", "Home");
        }

        //Show previous orders
        [HttpGet]
        [Authorize(Roles = "Customer")]
        public IActionResult Placed()
        {
            IEnumerable<Order> orderList = _orderDataService.Query(o => o.Username == User.Identity.Name && o.Status == Order.OrderStatus.Placed);
            
                OrderPlacedViewModel vm = new OrderPlacedViewModel
                {
                    OrderList = orderList,
                    TotalOrders = orderList.Count()                    
                };
                return View(vm);           
        }

        //Show details(orderlines) of placed order
        [HttpGet]
        [Authorize(Roles = "Customer")]
        public IActionResult PlacedDetails(int orderId)
        {

            Order order = _orderDataService.GetSingle(o => o.Username == User.Identity.Name && o.OrderId == orderId);
            if (order != null)
            {
                IEnumerable<OrderLine> orderLineList = _orderlineDataService.Query(o => o.OrderId == order.OrderId);
                Address address = _addressDataService.GetSingle(a => a.AddressId == order.AddressId);

                OrderPlacedDetailsViewModel vm = new OrderPlacedDetailsViewModel
                {
                    OrderLines = orderLineList,
                    TotalPrice = order.TotalPrice,
                    Address = address
                };
                return View(vm);
            }
            else
            {
                return NotFound();
            }
        }

        //remove order line from cart
        [HttpGet]
        [Authorize(Roles = "Customer")]
        public IActionResult RemoveLine(int lineId)
        {
            OrderLine orderLine = _orderlineDataService.GetSingle(a => a.OrderLineId == lineId);
            OrderRemoveLineViewModel vm = new OrderRemoveLineViewModel
            {
                OrderLineId = orderLine.OrderLineId,
                HamperName = orderLine.HamperName
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveLine(OrderRemoveLineViewModel vm)
        {
            OrderLine ordline = _orderlineDataService.GetSingle(a => a.OrderLineId == vm.OrderLineId);
            _orderlineDataService.Remove(ordline);
            return RedirectToAction("Cart", "Order");
        }
    }
}
