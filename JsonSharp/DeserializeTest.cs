using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonSharp
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
    internal class DeserializeTest
    {
        static void Main(string[] args)
        {
            string path = @"resources\deserialize.json";
            Json j = new Json();
            j.OpenFile(path);
            JsonObject js = (JsonObject)j.Parse();

            //Console.WriteLine(js.GetType());
            //Console.WriteLine(js.GetValue("address").GetType());
            Deserializer deserializer = new Deserializer();
            var t= deserializer.Deserialize<User>(js);
            Console.WriteLine(t.age);
            Console.WriteLine(t.address.city);
            Console.WriteLine(t.address.street);

        }


    }
}
