using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssetTrackingSystem
{
    public class Computer : Asset
    {
        public Computer(string category, DateTime purchaseDate, string brandName, double purchasePrice)
        {
            Category = category;
            PurchaseDate = purchaseDate;
            BrandName = brandName;
            PurchasePrice = purchasePrice;
        }
       //public int Id { get; set; }
       //public  List<Computer> Computers { get; set; }
    }
}
