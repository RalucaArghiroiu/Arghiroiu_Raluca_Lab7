using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arghiroiu_Raluca_Lab7.Models
{
    public class Shop
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Details
        {
            get
            {
                return Name + " " + Address;
            }
        }

        [OneToMany]
        public List<ShopList> ShopLists { get; set;}
    }
}
