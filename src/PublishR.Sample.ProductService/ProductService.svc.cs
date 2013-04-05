using System;
using System.Reflection;
using System.ServiceModel;
using PublishR.Sample.MessageLibrary;
using PublishR.Wcf;

namespace PublishR.Sample.ProductService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class ProductService : PublishrService, IProductService
    {
        private readonly Random _random;

        public ProductService()
        {
            _random = new Random();
        }
        public string GetData(int value)
        {
            Publishr.Publish(new ProductUpdatedMessage { ProductId = _random.Next(10, 1000) }, MethodBase.GetCurrentMethod().Name);
            return string.Format("You entered: {0}", value);
        }

        public void SetData(int value)
        {
            Publishr.Publish(new ProductCreatedMessage { Message = "Product Created", ProductId = _random.Next(10, 1000) }, MethodBase.GetCurrentMethod().Name);
            Publishr.Publish(new ProductUpdatedMessage { ProductId = _random.Next(10, 1000) }, MethodBase.GetCurrentMethod().Name);
        }
    }
}
