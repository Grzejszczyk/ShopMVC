using Microsoft.AspNetCore.Mvc;
using ShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private Card card;
        public OrderController(IOrderRepository repoService, Card cartService)
        {
            repository = repoService;
            card = cartService;
        }
        public ViewResult List() => View(repository.Orders.Where(o => !o.Shipped));
        [HttpPost]
        public IActionResult MarkShipped(int orderID)
        {
            Order order = repository.Orders.FirstOrDefault(o => o.OrderID == orderID);
            if (order != null)
            {
                order.Shipped = true;
                repository.SaveOrder(order);
            }
            return RedirectToAction(nameof(List));
        }
        public ViewResult Checkout() => View(new Order());
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (card.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                order.Lines = card.Lines.ToArray();
                repository.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }
        public ViewResult Completed()
        {
            card.Clear();
            return View();
        }
    }
}
