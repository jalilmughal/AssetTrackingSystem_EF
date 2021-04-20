using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssetTrackingSystem
{
    public class Asset
    {
        [Key]
        public int Id { get; set; }
        public string Category { get; set; }
        public string BrandAndModel { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Office Office { get; set; }
        public double PurchasePrice { get; set; }
    }
}
