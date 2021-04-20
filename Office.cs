using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AssetTrackingSystem
{
    public class Office
    {
       [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public Office(string name)
        {
            Name = name;
        }
        public Office()
        {
 
        }
        //List<Mobile> Phones { get; set; }
        //List<Computer> Computers { get; set; }
    }
}
