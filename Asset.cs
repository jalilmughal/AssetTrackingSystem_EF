using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssetTrackingSystem
{
    public class Asset
    {
        [Key] public int Id { get; set; }

        public string Category { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string BrandName { get; set; }
        public double PurchasePrice { get; set; }

        //public List<Computer> Computers { get; set; }
        //public List<Mobile> Mobiles { get; set; }
    }
}
