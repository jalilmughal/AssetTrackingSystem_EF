using System;
using System.Collections.Generic;
using System.Linq;

namespace AssetTrackingSystem
{
    public class Program
    {
        static AssetContext _db = new AssetContext();

        public static void Main(string[] args)
        {
            var app = new App();
            app.Run();
        }
    } 
}
