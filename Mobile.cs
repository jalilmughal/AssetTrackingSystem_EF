using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssetTrackingSystem
{
    public class Mobile : Asset
    {
        public Mobile(string category, DateTime purchaseDate, string modelName, double purchasePrice)
        {
            //Office= office;
            Category = category;
            PurchaseDate = purchaseDate;
            ModelName = modelName;
            PurchasePrice = purchasePrice;
        }         
        //public List<Mobile> Mobiles { get; set; }
    }
}
