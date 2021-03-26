using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssetTrackingSystem
{
    public class AssetContext:DbContext
    {
        //public DbSet<Computer> Computers { get; set; }
        //public DbSet<Mobile> Mobiles { get; set; }
        public DbSet<Asset> Assets { get; set; }

       // public string Office { get; set; }
        public string Category { get;  set; }
        public DateTime PurchaseDate { get;  set; }
        public string ModelName { get;  set; }
        public double PurchasePrice { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AssetTSDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        } 
    }
}
