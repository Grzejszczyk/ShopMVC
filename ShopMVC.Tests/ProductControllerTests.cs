using System;
using Xunit;
using Moq;
using ShopMVC.Models;
using ShopMVC.Controllers;
using System.Linq;
using System.Collections.Generic;
using ShopMVC.Models.ViewModel;

namespace ShopMVC.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Paginate()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductId = 1, Category="a", Description="d",Price=10, Name = "P1"},
                new Product {ProductId = 2, Category="b", Description="d",Price=10, Name = "P2"},
                new Product {ProductId = 3, Category="b", Description="d",Price=10, Name = "P3"},
                new Product {ProductId = 4, Category="b", Description="d",Price=10, Name = "P4"},
                new Product {ProductId = 5, Category="a", Description="d",Price=10, Name = "P5"}
                    }).AsQueryable<Product>());

            ProductController controller = new ProductController(mock.Object);
            controller.pageSize = 3;

            // Act
            IEnumerable<Product> result = (controller.List("a", 1).ViewData.Model as ProductsListViewModel).Products.ToArray();
            // Assert
            Product[] prodArray = result.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P1", prodArray[0].Name);
            Assert.Equal("P5", prodArray[1].Name);
        }
        [Fact]
        public void Can_Filter_Products()
        {
            // Arrange
            // - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductId = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductId = 2, Name = "P2", Category = "Cat2"},
                new Product {ProductId = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductId = 4, Name = "P4", Category = "Cat2"},
                new Product {ProductId = 5, Name = "P5", Category = "Cat3"}
                }).AsQueryable<Product>());

            // Arrange - create a controller and make the page size 3 items
            ProductController controller = new ProductController(mock.Object);
            controller.pageSize = 3;

            // Action
            Product[] result =
            (controller.List("Cat2", 1).ViewData.Model as ProductsListViewModel)
            .Products.ToArray();

            // Assert
            Assert.Equal(2, result.Length);
            Assert.True(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.True(result[1].Name == "P4" && result[1].Category == "Cat2");
        }
    }
}