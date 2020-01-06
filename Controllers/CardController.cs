using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ShopMVC.Models;
using ShopMVC.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopMVC.Models.ViewModel;

namespace ShopMVC.Controllers
{
    public class CardController : Controller
    {
        private IProductRepository repository;
        private Card card;
        public CardController(IProductRepository repo, Card cardService)
        {
            repository = repo;
            card = cardService;
        }
        public ViewResult Index(string returnUrl)
        {
            return View(new CardIndexViewModel
            {
                //Card = GetCard(),
                Card = card,
                ReturnUrl = returnUrl
            }); ;
        }
        public RedirectToActionResult AddToCard(int productId, string returnUrl)
        {
            Product product = repository.Products
            .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                //Card card = GetCard();
                card.AddItem(product, 1);
                //SaveCard(card);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToActionResult RemoveFromCard(int productId, string returnUrl)
        {
            Product product = repository.Products
            .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                //Card card = GetCard();
                card.RemoveLine(product);
                //SaveCard(card);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        //private Card GetCard()
        //{
        //    Card card = HttpContext.Session.GetJson<Card>("Card") ?? new Card();
        //    return card;
        //}
        //private void SaveCard(Card card)
        //{
        //    HttpContext.Session.SetJson("Card", card);
        //}
    }
}
