using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace AssetTrackingSystem
{
    public class App
    {
        static AssetContext _db = new AssetContext();
        Asset _Assets = new Asset();
        List<Asset> assetsList;

        internal void Run()
        {
            MainMenu();
        }
        private void MainMenu()
        {
            ReadAllAssetsFromDB();

            Console.WriteLine("");
            WriterExtensions.WriteInGreen("Main Menu".ToUpper());
            WriterExtensions.WriteInYellow("C) Create an asset");
            WriterExtensions.WriteInYellow("R) Read DB & Go to Main Menu");
            WriterExtensions.WriteInYellow("U) Update an asset");
            WriterExtensions.WriteInYellow("D) Delete an asset");
            Console.WriteLine("");
            WriterExtensions.WriteInRed("X) Exit Application");
            Console.WriteLine("");

            ConsoleKey command = Console.ReadKey(true).Key;

            if (command == ConsoleKey.C)
                CreateAssets();

            if (command == ConsoleKey.U)
                UpdateAssets();

            if (command == ConsoleKey.R)
                MainMenu();

            if (command == ConsoleKey.D)
                DeleteAssets();

            if (command == ConsoleKey.X)
                ExitApplication();
        }
        static DateTime GetDate(string date)
        {
            //validation tak
            /* string strDate = date.ToString();

             DateTime _date = DateTime.Parse(strDate);
            if((_date.Month != System.DateTime.Now.Month) || (_date.Day < 1 && _date.Day > 31) || _date.Year != System.DateTime.Now.Year)
             {
                 WriterExtensions.WriteInRed("Invalid date!");

             } */
            return DateTime.ParseExact(date, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);

        }
        public void CreateAssets()
        {
            string _Category;
            string _BrandAndModel;
            DateTime _PurchaseDate;
            double _PurchasePrice;
            WriterExtensions.WriteInGreen("Create Assets".ToUpper());
            Console.WriteLine("");

            WriterExtensions.WriteInYellow("Type your product details bellow, Go to Main Menu by typing 'save'.");
            do
            {
                //office - object must be placed inside the loop, so it can save the Office-data to the db.
                Office _Office = new Office();

                //Input Category of the asset
                WriterExtensions.WriteInYellow("Type 'Laptop' for Computers and 'Mobile' for Phones!");
                Console.Write("Category: ");

                _Category = Console.ReadLine();
                if (_Category.ToLower().Trim() == "save")
                {
                    break;
                }
                //Input BrandName of the asset
                Console.Write("Brand & Model: ");
                _BrandAndModel = Console.ReadLine();
                if (_BrandAndModel.ToLower().Trim() == "save")
                {
                    break;
                }
                //Input PurchaseDate of the asset
                WriterExtensions.WriteInYellow("Please enter date YYYY/MM/DD");
                Console.Write("Purchase Date: ");
                _PurchaseDate = GetDate(Console.ReadLine());
                if (_PurchaseDate.ToString().ToLower().Trim() == "save")
                {
                    break;
                }

                WriterExtensions.WriteInYellow("Type Office name!");
                Console.Write("Office: ");
                _Office.Name = Console.ReadLine();
                if (_Office.Name.ToLower().Trim() == "save")
                {
                    break;
                }

                //Input price of the asset
                WriterExtensions.WriteInYellow("Type Price as 123 or in decimals!");
                Console.Write("Price: ");
                _PurchasePrice = Convert.ToDouble(Console.ReadLine());
                if ((_PurchasePrice).ToString().ToLower().Trim() == "save")
                {
                    break;
                }

                //if-loop, if the _Category=laptop then add asset in computerAssets-List else add in mobileAssets-List.
                if ((_Category.ToLower().Trim() == "laptop") && (!String.IsNullOrWhiteSpace(_Category)))
                {

                    _db.Assets.Add(new Computer(_Category, _BrandAndModel, _PurchaseDate, _Office, (Convert.ToDouble(_PurchasePrice))));
                    WriterExtensions.WriteInGreen($"Asset '{_BrandAndModel}' created!");
                }
                else
                {
                    _db.Assets.Add(new Mobile(_Category, _BrandAndModel, _PurchaseDate, _Office, (Convert.ToDouble(_PurchasePrice))));
                    WriterExtensions.WriteInGreen($"Asset '{_BrandAndModel}' created!");
                }
            } while (true);

            //Save changes to Database
            _db.SaveChanges();
            //Go to MainMenu
            MainMenu();
        }
        private void ReadAllAssetsFromDB()
        {
            if (_db.Assets.Count() <= 0)
            {
                WriterExtensions.WriteInGreen("Welcome to ATS, your new Asset Tracking System".ToUpper());
                WriterExtensions.WriteInYellow("Press 'C' to Create and Add an asset to your ATS!");
            }
            else
            {
                Console.Clear();
                PrintHeaderWithId();
                // MotorCompany comp = _db.Companies.Where(comp => comp.Name.Contains("Lenny")).Include(comp => comp.Cars).FirstOrDefault();
                assetsList = _db.Assets.Where(asset => asset.Id != null).Include(asset => asset.Office).ToList();
                //Office off = new Office(assetsList[0].Office);
                //

                //Data is now IN Memory
                //DB Sorting by Category Thenby PurchaseDate

                //Asset assTest = _db.Assets.Find(1);
                assetsList = (List<Asset>)assetsList.OrderBy(a => a.Category).ThenByDescending(b => b.PurchaseDate).ToList();
                foreach (Asset assetItem in assetsList)
                {
                    //If purchaseDate older then 33 months from today's DateTime, WriteLine in RED else WriteLine normal.
                    if ((DateTime.Now > assetItem.PurchaseDate.AddMonths(33)))
                    {
                        WriterExtensions.WriteInRed(Tab(assetItem.Id.ToString()) + Tab(assetItem.Category) + Tab(assetItem.BrandAndModel) + Tab(Convert.ToDateTime(assetItem.PurchaseDate).ToShortDateString()) + Tab((assetItem.Office.Name).ToString()) + Tab(assetItem.PurchasePrice.ToString()));
                    }
                    else
                    {
                        Console.WriteLine(Tab(assetItem.Id.ToString()) + Tab(assetItem.Category) + Tab(assetItem.BrandAndModel) + Tab(Convert.ToDateTime(assetItem.PurchaseDate).ToShortDateString()) + Tab((assetItem.Office.Name).ToString()) + Tab(assetItem.PurchasePrice.ToString()));
                    }
                }
            }
        }
        private void UpdateAssets()
        {   //checking if the database has any
            if (_db.Assets.Count() <= 0)
            {
                WriterExtensions.WriteInGreen("You must have atleast 1 asset to update!".ToUpper());
            }
            else
            {
                //Read all assests
                ReadAllAssetsFromDB();

                //Read all _Assets
                Console.WriteLine("");
                WriterExtensions.WriteInGreen("Update Assets".ToUpper());

                //Ask for ID of which asset to update
                Console.Write("Asset (Id) you want to update: ");
                int assetId = int.Parse(Console.ReadLine());
                Asset asset = _db.Assets.Find(assetId);

                //Show _Assets of given assetId
                WriterExtensions.WriteInYellow("Current Category is: " + asset.Category);
                Console.Write("New Category: ");
                asset.Category = Console.ReadLine();

                WriterExtensions.WriteInYellow("Current Brand and Model is: " + asset.BrandAndModel);
                Console.Write("Write the new Brand and Model name: ");
                asset.BrandAndModel = Console.ReadLine();

                WriterExtensions.WriteInYellow("Current Office is: " + asset.Office.Name);
                Console.Write("Write the new Office name: ");
                asset.Office.Name = Console.ReadLine();

                WriterExtensions.WriteInYellow("Current Purchase Date is: " + asset.PurchaseDate);
                Console.Write("New Purchase Date: ");
                asset.PurchaseDate = GetDate(Console.ReadLine());



                WriterExtensions.WriteInYellow("Current Purchase Price is: " + asset.PurchasePrice);
                Console.Write("New Purchase Price: ");
                asset.PurchasePrice = Convert.ToDouble(Console.ReadLine());


                //Save update data
                _db.SaveChanges();

                WriterExtensions.WriteInGreen($"Asset Id: {assetId} has been updated!");
                Console.ReadKey();
            }
            MainMenu();
        }
        private void DeleteAssets()
        {
            if (_db.Assets.Count() <= 0)
            {
                WriterExtensions.WriteInGreen("You must have atleast 1 asset to delete!".ToUpper());
            }
            else
            {
                ReadAllAssetsFromDB();
                Console.WriteLine("");
                WriterExtensions.WriteInGreen("Delete Assets".ToUpper());

                Console.Write("Asset (Id) you want to delete: ");

                int assetId = int.Parse(Console.ReadLine());
                Asset asset = _db.Assets.Find(assetId);
                _db.Assets.Remove(asset);

                //Save update data
                _db.SaveChanges();

                WriterExtensions.WriteInGreen($"Asset {assetId}, has been deleted !");
                Console.ReadKey();
            }
            MainMenu();
        }
        private void ExitApplication()
        {
            _db.SaveChanges();
            WriterExtensions.WriteInGreen("Have an amazing day, see you later :)");
            return;
        }
        public static void PrintHeaderWithId()
        {
            WriterExtensions.WriteInYellow(Tab("Id") + Tab("Category") + Tab("Brand & Model") + Tab("Purchase Date") + Tab("Office") + Tab("Price"));
            WriterExtensions.WriteInYellow(Tab("--") + Tab("--------") + Tab("-------------") + Tab("-------------") + Tab("------") + Tab("-----"));
        }
        public static void PrintHeaderWithoutId()
        {
            WriterExtensions.WriteInYellow(Tab("Category") + Tab("Brand & Model") + Tab("Purchase Date") + Tab("Office") + Tab("Price"));
            WriterExtensions.WriteInYellow(Tab("--------") + Tab("-------------") + Tab("-------------") + Tab("------") + Tab("-----"));
        }
        public static string Tab(string input)
        {
            return input.PadRight(15);
        }
    }
}
