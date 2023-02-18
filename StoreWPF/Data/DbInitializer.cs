using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoreWPF.DAL.Context;
using StoreWPF.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWPF.Data
{
    class DbInitializer
    {
        private readonly StoreDB _db;
        private readonly ILogger<DbInitializer> _logger;

        public DbInitializer(StoreDB db, ILogger<DbInitializer> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task InitializeAsync() 
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация БД.");

            _logger.LogInformation("Удаление БД.");
            await _db.Database.EnsureDeletedAsync().ConfigureAwait(false);
            _logger.LogInformation($"Удаление БД выполнено за {timer.ElapsedMilliseconds} мс");

            //_db.Database.EnsureCreated();
            
            await _db.Database.MigrateAsync();

            if (await _db.Products.AnyAsync())
                return;

            await InitializeCategory();
            await InitializeUOM();
            await InitializeOperationType();
            await InitializeProvider();
            await InitializeOperation();
            await InitializeProduct();
            await InitializeOrder();
            await InitializeOrderProduct();

            _logger.LogInformation("Завершение инициализация БД за {0}", timer.Elapsed.TotalSeconds);
        }

        private const int __CategoriesCount = 10;
        private Category[] _Categories;
        private async Task InitializeCategory() 
        {
            _Categories = new Category[__CategoriesCount];
            for (int i = 0; i < __CategoriesCount; i++)
            {
                _Categories[i] = new Category
                {
                    Name = $"Категория {i + 1}"
                };
            }
            await _db.Categories.AddRangeAsync(_Categories);
            await _db.SaveChangesAsync();
        }


        private UOM[] _UOMs;
        private async Task InitializeUOM()
        {
            _UOMs = new UOM[4];
            _UOMs[0] = new UOM
            {
                Name = "Литр",
                ShortName = "лт"
            };
            _UOMs[1] = new UOM
            {
                Name = "Киллограмм",
                ShortName = "кг"
            };
            _UOMs[2] = new UOM
            {
                Name = "Штук",
                ShortName = "шт"
            };
            _UOMs[3] = new UOM
            {
                Name = "Метр",
                ShortName = "м"
            };
            await _db.UOMs.AddRangeAsync(_UOMs);
            await _db.SaveChangesAsync();
        }

        private OperationType[] _OperationTypes;
        private async Task InitializeOperationType()
        {
            _OperationTypes = new OperationType[2];
            _OperationTypes[0] = new OperationType { Name = "Приход"};
            _OperationTypes[1] = new OperationType { Name = "Расход"};
            await _db.OperationTypes.AddRangeAsync(_OperationTypes);
            await _db.SaveChangesAsync();
        }


        private const int __ProvidersCount = 5;
        private Provider[] _Providers;
        private async Task InitializeProvider()
        {
            _Providers = new Provider[__ProvidersCount];
            for (int i = 0; i < __ProvidersCount; i++)
            {
                _Providers[i] = new Provider
                {
                    Name = $"Поставщик {i}",
                    INN = $"{i * 123456789}",
                    Phone = $"92 {i*1231234}",
                    Address = $"Город {i}, улица {i}"
                };
            }
            await _db.Providers.AddRangeAsync(_Providers);
            await _db.SaveChangesAsync();
        }

        private const int __ProductsCount = 25;
        private Product[] _Products;
        private async Task InitializeProduct()
        {
            var rnd = new Random();
            _Products = Enumerable.Range(1, __ProductsCount)
                .Select(i => new Product
                {
                    Name = $"Продукт {i}",
                    Category = rnd.NextItem(_Categories),
                    Barcode = $"{rnd.Next(111111111, 1111111111)}",
                    UOM = rnd.NextItem(_UOMs),
                    SellingPrice =(decimal) rnd.NextDouble() * 125 + 1
                }).ToArray();
            await _db.Products.AddRangeAsync(_Products);
            await _db.SaveChangesAsync();
        }


        private const int __OperationsCount = 9;
        private Operation[] _Operations;
        private async Task InitializeOperation()
        {
            var rnd = new Random();
            _Operations = Enumerable.Range(1, __OperationsCount)
                .Select(i => new Operation
                {
                    Number = $"12345{i}",
                     DocumentNumber = rnd.Next(1,1234).ToString(),
                     OperationType = rnd.NextItem(_OperationTypes),
                     Provider =  rnd.NextItem(_Providers)
                }).ToArray();
            await _db.Operations.AddRangeAsync(_Operations);
            await _db.SaveChangesAsync();
        }

        private const int __OrdersCount = __OperationsCount;
        private Order[] _Orders;
        private async Task InitializeOrder()
        {
            _Orders = new Order[__OrdersCount];
            for (int i = 0; i < __OperationsCount; i++)
            {
                _Orders[i] = new Order
                {
                    Operation = _Operations[i],
                    OperationDate = DateTime.Today
                };
            }
            await _db.Orders.AddRangeAsync(_Orders);
            await _db.SaveChangesAsync();
        }

        private const int __OrderProductsCount = 199;
        private OrderProduct[] _OrderProducts;
        private async Task InitializeOrderProduct()
        {
            _OrderProducts = new OrderProduct[__OrderProductsCount];
            var rnd = new Random();

            Order order;
            Product product;
            for (int i = 0; i < __OrderProductsCount; i++)
            {
                order = rnd.NextItem(_Orders);
                product = rnd.NextItem(_Products);
                _OrderProducts[i] = new OrderProduct
                {
                    Order = order,
                    OrderId = order.Id,
                    Product = product,
                    ProductId = product.Id,
                    Quantity = rnd.Next(1, 9),
                    Price = product.SellingPrice * (product.SellingPrice / 100 * 15)
                    //Price = product.SellingPrice * 2
                };
            }
            /*var rnd = new Random();
            _OrderProducts = Enumerable.Range(1, __OrderProductsCount)
                .Select(i => new OrderProduct
                {
                    OrderId = rnd.NextItem(_Orders).Id,
                    ProductId = rnd.NextItem(_Products).Id,
                    Quantity = rnd.Next(1, 9)
                }).ToArray();*/

            await _db.OrderProducts.AddRangeAsync(_OrderProducts);
            await _db.SaveChangesAsync();
        }

    }
}
