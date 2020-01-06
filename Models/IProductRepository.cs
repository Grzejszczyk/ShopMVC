using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Models
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        void SaveOrder(Order order);
    }
}