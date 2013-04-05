using System.ServiceModel;
using PublishR.Wcf;

namespace PublishR.Sample.ProductService
{
    [ServiceContract]
    public interface IProductService : IPublishrService
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        void SetData(int value);
        
    }
}
