using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoreWPF.DAL.Context;
using System;
using System.Collections.Generic;
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
            await _db.Database.EnsureDeletedAsync().ConfigureAwait(false);

            //_db.Database.EnsureCreated();
            
            await _db.Database.MigrateAsync();

            if (await _db.Products.AnyAsync())
                return;

        }
    }
}
