using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreWPF.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWPF.Data
{
    public static class DbRegistrator
    {
        public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration configuration) => services
            .AddDbContext<StoreDB>(
            opt => 
            {
                var type = configuration["Type"];
                switch (type)
                {
                    case null: throw new InvalidOperationException("Не проеделен тип БД!");
                    default: throw new InvalidOperationException($"Тип подключения {type} не поддерживается!");
                    case "MSSQL": 
                        opt.UseSqlServer(configuration.GetConnectionString(type));
                        break;
                    case "SQLite": 
                        opt.UseSqlite(configuration.GetConnectionString(type));
                        break;
                    case "InMemory": 
                        opt.UseInMemoryDatabase("Store.db");
                        break;
                }
            })
        ;
    }
}
