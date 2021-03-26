using System;
using System.Collections.Generic;
using System.Text;

namespace AssetTrackingSystem
{
    class Function
    {
        static AssetContext _db = new AssetContext();

        /*public static void ClearDatabase()
        {
            _db.RemoveRange(_db.Computers);
            _db.RemoveRange(_db.Mobiles);

            _db.SaveChanges();
        } */

        /*public static void AddSomeAssets()
        {

            var computer = new AssetContext { Category = "Laptop", PurchaseDate = Convert.ToDateTime(22 / 02 / 2019), ModelName = "Asus K55VM", PurchasePrice = Convert.ToDouble(8999) };
            var phone = new AssetContext { Category = "Mobile", PurchaseDate = Convert.ToDateTime(01 / 03 / 2021), ModelName = "Samsung Galaxy S21", PurchasePrice = Convert.ToDouble(10599) };

            using (var asset = new AssetContext())
            {
                asset.AddRange(computer, phone);
                asset.SaveChanges();
            }
        }  */
    }
}
