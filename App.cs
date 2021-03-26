using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssetTrackingSystem
{
    public class App
    {
        static AssetContext _db = new AssetContext();

        Asset assets = new Asset();
        List<Asset> assetsList;
        internal void Run()
        {
            PageMainMenu();
        }
        private void PageMainMenu()
        {
            ShowAllAssets();
            WriterExtensions.WriteInGreen("Main Menu");
            WriterExtensions.WriteInGreen("a) Go to Main Menu");
            WriterExtensions.WriteInGreen("b) Create an asset");
            WriterExtensions.WriteInGreen("c) Update an asset");
            WriterExtensions.WriteInGreen("d) Delete an asset");
        
            ConsoleKey command = Console.ReadKey(true).Key;

            if (command == ConsoleKey.A)
                PageMainMenu();

            if (command == ConsoleKey.B)
                CreateAssets();

            if (command == ConsoleKey.C)
                UpdateAssets();

            if (command == ConsoleKey.D)
                DeleteAssets();


        }

        private void DeleteAssets()
        {
            WriterExtensions.WriteInGreen("Delete Assets");

            ShowAllAssets();

            Console.Write("Which asset (Id) you want to delete: ");

            int assetId = int.Parse(Console.ReadLine());
            Asset asset = _db.Assets.Find(assetId);
            _db.Assets.Remove(asset);

            //Save update data
            _db.SaveChanges();

            WriterExtensions.WriteInGreen("Asset Updated!");
            Console.ReadKey();

            PageMainMenu();
        } 

        private void UpdateAssets()
        {
            //Read all assets
            WriterExtensions.WriteInGreen("Update Assets");

            //Show all assests           
            ShowAllAssets();
            //Ask for ID of which asset to update

            Console.Write("Which asset (Id) do you want to update: ");
            int assetId = int.Parse(Console.ReadLine());
            Asset asset = _db.Assets.Find(assetId);

            //Show assets of given assetId
            WriterExtensions.WriteInYellow("Current Category is: " + asset.Category);
            Console.Write("Write the new Category: ");
            asset.Category = Console.ReadLine();

            WriterExtensions.WriteInYellow("Current Purchase Date is: " + asset.PurchaseDate);
            Console.Write("Write the new Purchase Date: ");
            asset.PurchaseDate = Convert.ToDateTime(Console.ReadLine());

            WriterExtensions.WriteInYellow("Current Model Name is: " + asset.ModelName);
            Console.Write("Write the new Model Name: ");
            asset.ModelName = Console.ReadLine();

            WriterExtensions.WriteInYellow("Current Purchase Price is: " + asset.PurchasePrice);
            Console.Write("Write the new Purchase Price: ");
            asset.PurchasePrice = Convert.ToDouble(Console.ReadLine());
            

            //Save update data
            _db.SaveChanges();  

            WriterExtensions.WriteInGreen("Asset Updated!");
            Console.ReadKey();

            PageMainMenu();

            //show all assets
        }

        private void ShowAllAssets()
        {
            Console.Clear();
            PrintHeaderWithId();
            assetsList = _db.Assets.OrderBy(a => a.Id).ToList();
            foreach (Asset assetItem in assetsList)
            {
                Console.WriteLine(Tab(assetItem.Id.ToString()) + Tab(assetItem.Category) + Tab(Convert.ToDateTime(assetItem.PurchaseDate).ToShortDateString()) + Tab(assetItem.ModelName) + Tab(assetItem.PurchasePrice.ToString()));
            }
        }

        private void ReadAssets()
        {
            WriterExtensions.WriteInGreen("Read Assets");
            PrintHeaderWithId();
            //Reading all my data in the DB
            assetsList = _db.Assets.OrderBy(Category => Category).ToList();

            //Data is now IN Memory
            assetsList = (List<Asset>)assetsList.OrderBy(a => a.Category).ThenBy(b => b.PurchaseDate).ToList();  //DB Sorting by Category Thenby PurchaseDate


            //If purchaseDate older then 33 months from today's DateTime, WriteLine in RED else WriteLine normal.
            foreach (Asset assetItem in assetsList)
            {
                if ((DateTime.Now > assetItem.PurchaseDate.AddMonths(33)))
                {
                    WriterExtensions.WriteInRed(Tab(assetItem.Id.ToString())+Tab(assetItem.Category) + Tab(Convert.ToDateTime(assetItem.PurchaseDate).ToShortDateString()) + Tab(assetItem.ModelName) + Tab(assetItem.PurchasePrice.ToString()));
                }
                else
                {
                    Console.WriteLine(Tab(assetItem.Id.ToString()) + Tab(assetItem.Category) + Tab(Convert.ToDateTime(assetItem.PurchaseDate).ToShortDateString()) + Tab(assetItem.ModelName) + Tab(assetItem.PurchasePrice.ToString()));
                }
            }
        }

        public void CreateAssets()
        {
            WriterExtensions.WriteInGreen("Create Assets");

            string _Category = _db.Category;
            DateTime _PurchaseDate = _db.PurchaseDate;
            string _ModelName = _db.ModelName;
            double _PurchasePrice = _db.PurchasePrice;

            WriterExtensions.WriteInYellow("Type your product details bellow, exit by typing 'exit'.");
            do
            {
                //Input Category of the asset
                WriterExtensions.WriteInYellow("Type 'Laptop' for Laptop Computers and 'Mobile' for Mobile Phones!");
                Console.Write("Category: ");

                _Category = Console.ReadLine();
                if (_Category.ToLower().Trim() == "exit")
                {
                    break;
                }

                //Input PurchaseDate of the asset
                WriterExtensions.WriteInYellow("Please enter date in format DD/MM/YYYY or YYYY/MM/DD");
                Console.Write("Purchase Date: ");
                _PurchaseDate = Convert.ToDateTime(Console.ReadLine());

                //Input ModelName of the asset
                Console.Write("Model Name: ");
                _ModelName = Console.ReadLine();
                if (_ModelName.ToLower().Trim() == "exit")
                {
                    break;
                }

                //Input price of the asset
                Console.WriteLine("Type Price as 123 and not decimal!");
                Console.Write("Price: ");
                _PurchasePrice = Convert.ToDouble(Console.ReadLine());
                if ((_PurchasePrice).ToString().ToLower().Trim() == "exit")
                {
                    break;
                }

                //if-loop, if the _Category=laptop then add asset in computerAssets-List else add in mobileAssets-List.
                if ((_Category.ToLower().Trim() == "laptop") && (!String.IsNullOrWhiteSpace(_Category)))
                {
                    _db.Assets.Add(new Computer(_Category, _PurchaseDate, _ModelName, (Convert.ToDouble(_PurchasePrice))));
                }
                else
                {
                    _db.Assets.Add(new Mobile(_Category, _PurchaseDate, _ModelName, (Convert.ToDouble(_PurchasePrice))));
                }
            } while (true);
                                 
            //Save create Database
            _db.SaveChanges();

            WriterExtensions.WriteInGreen("Asset Created!");
            Console.ReadKey();
            PageMainMenu();
        }

        public static void PrintHeaderWithId()
        {
            WriterExtensions.WriteInYellow(Tab("Id") + Tab("Category") + Tab("Purchase Date") + Tab("Model") + Tab("Price"));
            WriterExtensions.WriteInYellow(Tab("--") + Tab("-----") + Tab("-------------") + Tab("-----") + Tab("-----"));
        }
        public static void PrintHeader()
        {
            WriterExtensions.WriteInYellow(Tab("Category") + Tab("Purchase Date") + Tab("Model") + Tab("Price"));
            WriterExtensions.WriteInYellow(Tab("-----") + Tab("-------------") + Tab("-----") + Tab("-----"));
        }                                                      
        public static string Tab(string input)
        {
            return input.PadRight(15);
        }
    }
}
