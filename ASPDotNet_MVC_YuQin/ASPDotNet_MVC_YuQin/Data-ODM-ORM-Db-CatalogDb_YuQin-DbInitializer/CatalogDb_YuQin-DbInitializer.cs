using CatalogDb_YuQin.DB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CatalogDb_YuQin.DB.Data
{
    public static class DbInitializer
    {
        public static void Initialize(CatalogDb_YuQinDbContext catalogDb_YuQinDbContext)
        {
            catalogDb_YuQinDbContext.Database.EnsureCreated();

            // Look for any students.
            if (catalogDb_YuQinDbContext.OrdersDbSet.Any())
            {
                return;   // DB has been seeded
            }

            catalogDb_YuQinDbContext.Database.ExecuteSqlRaw(
            $"INSERT INTO dbo.Orders(BuyerID,OrderDate)  VALUES('43930878@qq.com',DateTimeOffset.Now) ");
            catalogDb_YuQinDbContext.Database.ExecuteSqlRaw(
            $"INSERT INTO dbo.Orders(BuyerID,OrderDate)  VALUES('yuqin9999@dingTalk.com',DateTimeOffset.Now");

            catalogDb_YuQinDbContext.SaveChanges();
        }
    }
}
