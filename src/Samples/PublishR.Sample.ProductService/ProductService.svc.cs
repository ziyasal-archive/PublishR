using System;
using System.Collections.Generic;
using System.Reflection;
using System.ServiceModel;
using PublishR.Sample.MessageLibrary;
using PublishR.Sample.ProductService.Request;
using PublishR.Sample.ProductService.Response;
using PublishR.Wcf;

namespace PublishR.Sample.ProductService {
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class ProductService : PublishrService, IProductService {
        private readonly Random _random;
        readonly List<Product> _products;

        public ProductService() {
            _products = InitSampleProducts();
            _random = new Random();
        }

        private List<Product> InitSampleProducts() {
            return new List<Product>() {
                new Product {Id = 1,Name = "Adidas Shoes"},
                new Product {Id = 2,Name = "Nike T-Shirt"},
                new Product {Id = 3,Name = "Converse Wallet"}
            };
        }


        public ProductCreatedResponse CreateProduct(CreateProductRequest request) {
            var product = new Product {
                Name = request.Name,
                Id = _random.Next(10, 1000)
            };
            _products.Add(product);
            Publishr.Publish(new ProductCreatedMessage { Message = "Product Created", ProductId = product.Id, ProductName = product.Name });

            return new ProductCreatedResponse {
                CreatedProductId = product.Id
            };
        }

        public ProductDeletedResponse DeleteProduct(DeleteProductRequest request) {
            _products.RemoveAll(product => product.Id == request.ProductTodeleteId);
            Publishr.Publish(new ProductCreatedMessage { Message = "Product Deleted", ProductId = request.ProductTodeleteId });
            return new ProductDeletedResponse {
                Success = true
            };
        }

        public ProductsResponse GetProducts(GetProductsRequest request) {
            return new ProductsResponse {
                Products = _products
            };
        }
    }
}
