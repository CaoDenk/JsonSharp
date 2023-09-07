using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonTest
{
    class Address
    {
        public string street { get; set; }
        public string city { get; set; }
    }

    internal class User
    {
        public string name { get; set; }
        public int age { get; set; }
        public Address address { get; set; }
    }
}
