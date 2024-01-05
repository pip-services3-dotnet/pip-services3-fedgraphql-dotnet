using System;
using System.Threading.Tasks;
using PipServices3.Commons.Config;
using PipServices3.Commons.Refer;
using PipServices3.Components.Log;
using PipServices3.GraphQL.Federation.Services;
using PipServices3.Rpc.Services;

namespace PipServices3.GraphQL.Federation
{
	class Program
    {
		static void Main(string[] args)
		{
			DummyProcessAsync().Wait();
		}

		private static async Task DummyProcessAsync()
		{
			var httpEnpoint = new HttpEndpoint();

			var cartController = new CartController();
			var productController = new ProductController();

			var cartService = new CartGraphQLService();
			var productService = new ProductGraphQLService();
			var facadeService = new FacadeGraphQLService();

			var logger = new ConsoleLogger();

			var httpConfig = ConfigParams.FromTuples(
				"connection.protocol", "http",
				"connection.host", "localhost",
				"connection.port", 3000
			);

			httpEnpoint.Configure(httpConfig);

			cartService.Configure(ConfigParams.FromTuples(
				"base_route", "/cart/graphql",
				"enable_tool", "true"
			));

			productService.Configure(ConfigParams.FromTuples(
				"base_route", "/product/graphql",
				"enable_tool", "true"
			));

			var subgraphs = new ConfigParams();
			subgraphs.AddSection("carts", ConfigParams.FromTuples(
				"uri", "http://localhost:3000/cart/graphql"
			));

			subgraphs.AddSection("products", ConfigParams.FromTuples(
				"uri", "http://localhost:3000/product/graphql"
			));

			var facadeConfig = ConfigParams.FromTuples(
				"allow_introspection", "true",
				"max_execution_depth", "10",
				"enable_mutation", "false",
				"base_route", "/graphql",
				"enable_tool", "true"
			);
			facadeConfig.AddSection("subgraphs", subgraphs);
			facadeService.Configure(facadeConfig);

			var references = References.FromTuples(
				new Descriptor("pip-services3", "endpoint", "http", "default", "1.0"), httpEnpoint,
				new Descriptor("pip-services3-commons", "logger", "console", "default", "1.0"), logger,

				new Descriptor("pip-services3-product", "controller", "default", "default", "1.0"), productController,
				new Descriptor("pip-services3-cart", "controller", "default", "default", "1.0"), cartController,
				
				new Descriptor("pip-services3-product", "service", "graphql", "default", "1.0"), productService,
				new Descriptor("pip-services3-cart", "service", "graphql", "default", "1.0"), cartService,
				new Descriptor("pip-services3-federation", "service", "fedgraphql", "default", "1.0"), facadeService
			);

			cartService.SetReferences(references);
			productService.SetReferences(references);
			facadeService.SetReferences(references);

			CreateProductsAsync(productController).Wait();
			CreateCartsAsync(cartController).Wait();

			await httpEnpoint.OpenAsync(null);

//#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
//			Task.Run(() => ClientSideAsync(httpConfig, logger));
//#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

			Console.WriteLine("Press ENTER to exit...");
			Console.ReadLine();
		}

		//private static async Task ClientSideAsync(ConfigParams config, Components.Log.ILogger logger)
		//{
		//	try
		//	{
		//		var clientConfig = ConfigParams.FromTuples(
		//			"base_route", "/graphql"
		//		);

		//		clientConfig.Append(config);
		//		var client = new DummyGraphQLClient();
		//		client.Configure(clientConfig);

		//		await client.OpenAsync(null);

		//		Dummy dummy;

		//		dummy = await client.CreateAsync(IdGenerator.NextLong(), dummy1);
		//		dummy = await client.CreateAsync(IdGenerator.NextLong(), dummy2);
		//		dummy = await client.CreateAsync(IdGenerator.NextLong(), dummy3);
		//	}
		//	catch (Exception ex)
		//	{
		//		logger.Error(null, ex);
		//	}
		//}

		private static async Task CreateCartsAsync(ICartController controller)
		{
			await controller.CreateAsync(null, new Cart
			{
				Id = 1,
				Items =
				{
					new CartItem { ProductId = 1, Quantity = 10, Total = 1 },
					new CartItem { ProductId = 2, Quantity = 1, Total = 2 },
					new CartItem { ProductId = 1, Quantity = 3, Total = 1 },
				},
				SubTotal = 4,
			});

			await controller.CreateAsync(null, new Cart
			{
				Id = 2,
				Items =
				{
					new CartItem { ProductId = 1, Quantity = 3, Total = 1 },
				},
				SubTotal = 1,
			});

			await controller.CreateAsync(null, new Cart
			{
				Id = 3,
				Items =
				{
					new CartItem { ProductId = 1, Quantity = 5, Total = 1 },
					new CartItem { ProductId = 2, Quantity = 1, Total = 2 },
					new CartItem { ProductId = 3, Quantity = 4, Total = 5 },
				},
				SubTotal = 8,
			});
		}

		private static async Task CreateProductsAsync(IProductController controller)
		{
			await controller.CreateAsync(null, new Product
			{
				Id = 1,
				Title = "test1",
				Description = "test1 description",
				Images = { "http://domain.com/image1.jpg", "http://domain.com/image2.jpg" },
				Price = 120,
				Sku = "A98723423"
			});

			await controller.CreateAsync(null, new Product
			{
				Id = 2,
				Title = "test2",
				Description = "test2 description",
				Images = { "http://domain.com/image3.jpg", "http://domain.com/image4.jpg", "http://domain.com/image5.jpg" },
				Price = 120,
				Sku = "A98723414"
			});

			await controller.CreateAsync(null, new Product
			{
				Id = 3,
				Title = "test3",
				Description = "test1 desc",
				Images = { "http://domain.com/image6.jpg", "http://domain.com/image7.jpg" },
				Price = 120,
				Sku = "A98723499"
			});
		}

	}
}
