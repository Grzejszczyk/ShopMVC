using Microsoft.AspNetCore.Mvc;
using ShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Components
{
    public class CardSummaryViewComponent : ViewComponent
    {
        private Card card;
        public CardSummaryViewComponent(Card cardService)
        {
            card = cardService;
        }
        public IViewComponentResult Invoke()
        {
            return View(card);
        }
    }
}
