PublishR
========

The project was created for a purpose that brings the real time web to SOA based applications with SignalR. 
It provides backend library for web clients that use same service can be informed about each other's actions in real time. 
Also provides javascript API for quickly, easily and securely adding scalable realtime functionality to web and mobile apps.

_[Sample video available on vimeo]( https://vimeo.com/63431591 "Simple PublishR demo")_

**_Sample applications and documentation will be presented soon._**

##Basic Setup##

###Asp.NET MVC###

**bootstrap**
```csharp
 Publishr.Instance.Configure(ctx =>
  {
    ctx.RegisterModules(typeof(HomeController).Assembly)
    .WithClient<ProductServiceClient>()
    .WithDomain("http://acommerce.com/");
    
    //Default Name
    ctx.Subscriptions.Add(new Subscription(typeof(ProductOperationsModule), 
      Defaults.PUBLISHR_HUB_NAME, "logMessage"));
      
    //Type
    ctx.Subscriptions.Add(new Subscription(typeof(OrderOperationsModule), 
      typeof(PublishrHub).GetHubName(), "logMessage"));
  });
```

**handle events**
```csharp
 public class ProductOperationsModule : PublishrModule,
       IHandle<ProductCreatedMessage>,
       IHandle<ProductUpdatedMessage>,
       IHandle<ProductDeletedMessage>
    {
        public void Handle(ProductCreatedMessage message)
        {
            //Get hub by name
            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext(message.HubName);
            hubContext.Clients.All.productCreated(new { Message = "Product Create.", message.ProductId });
        }

        public void Handle(ProductUpdatedMessage message)
        {
            //Use current hub
            CurrentHubContext.Clients.All.productUpdated(new { Message = "Product Update.", message.ProductId });
        }

        public void Handle(ProductDeletedMessage message)
        {
            //Get hub by type
            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<PublishrHub>();
            hubContext.Clients.All.productDeleted(new { Message = "Product Delete.", message.ProductId });
        }
    }
```

**publish events from wcf**
```csharp
  public ProductCreatedResponse CreateProduct(CreateProductRequest request)
        {
           //operations...
           //............
           
            Publishr.Publish(new ProductCreatedMessage { 
                  Message = "Product Created", 
                  ProductId = product.Id, 
                  ProductName = product.Name });

            return new ProductCreatedResponse
            {
                CreatedProductId = product.Id
            };
        }
```

###NANCYFX###

**bootstrap**
```csharp
  Publishr.Instance.Configure(ctx =>
  {
    ctx.WithClient<ProductServiceClient>();
    ctx.WithDomain("http://ccommerce.com/");
    ctx.Subscriptions.Add(new Subscription(typeof(ProductOperationsHandlerModule)));
  });
```

**handle events**
```csharp
  public class ProductOperationsHandlerModule : 
      PublishrModule,
      IHandle<ProductCreatedMessage>,
      IHandle<ProductDeletedMessage>
    {
        public void Handle(ProductCreatedMessage message)
        {
            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext(message.HubName);
            hubContext.Clients.All.productCreated(new { msg = string.Format("Product created id:{0}", message.ProductId) });
        }

        public void Handle(ProductDeletedMessage message)
        {
            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext(message.HubName);
            hubContext.Clients.All.productDeleted(new { msg = string.Format("Product Deleted id:{0}", message.ProductId) });
        }
    }
```
