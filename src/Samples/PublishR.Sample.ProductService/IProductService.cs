using System.ServiceModel;
using PublishR.Sample.ProductService.Request;
using PublishR.Sample.ProductService.Response;
using PublishR.Wcf;

namespace PublishR.Sample.ProductService
{
    [ServiceContract]
    public interface IProductService : IPublishrService
    {
        [OperationContract]
        ProductCreatedResponse CreateProduct(CreateProductRequest request);

        [OperationContract]
        ProductDeletedResponse DeleteProduct(DeleteProductRequest request);

        [OperationContract]
        ProductsResponse GetProducts(GetProductsRequest request);
    }
}
