using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssetTrackingSystem
{
    public class Computer : Asset
    {
        public Computer(string category, string brandAndModel, DateTime purchaseDate, Office office, double purchasePrice)
        {
            Category = category;
            BrandAndModel = brandAndModel;
            PurchaseDate = purchaseDate;
            Office = office;
            PurchasePrice = purchasePrice;
        }
        [Key]
        public int Id { get; set; }
    }
}
