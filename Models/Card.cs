using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Models
{
    public class Card
    {
        private List<CardLine> lineCollection = new List<CardLine>();
        public virtual void AddItem(Product product, int quantity)
        {
            CardLine line = lineCollection
            .Where(p => p.Product.ProductId == product.ProductId)
            .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CardLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public virtual void RemoveLine(Product product) => lineCollection.RemoveAll(l => l.Product.ProductId == product.ProductId);
        public virtual decimal ComputeTotalValue() => lineCollection.Sum(e => e.Product.Price * e.Quantity);
        public virtual void Clear() => lineCollection.Clear();
        public virtual IEnumerable<CardLine> Lines => lineCollection;

        public class CardLine
        {
            public int CardLineID { get; set; }
            public Product Product { get; set; }
            public int Quantity { get; set; }
        }
    }
}
