using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_json
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();

            p.testJsonArray();
            p.testJsonObject();
            Console.ReadLine();
        }
        void testJsonArray()
        {
            string path = @"..\..\resources\test.json";
            Json j = new Json();
            j.openFile(path);
            var js0 = (JsonArray)j.parse();

            foreach (var t in j.tokens)
            {
                Console.WriteLine(t.ToString());
            }
            JsonObject js = js0.get(0);
            Console.WriteLine(js.getInt("age"));
            JsonObject honghong = js0.get(1);
           JsonArray  friends= honghong.getJsonObject("info").getJsonObject("personal").getJsonArray("friends");
           JsonArray looks=    friends.get(1).getJsonArray("friendslooks");
            Console.WriteLine(friends.get(1).getInt("age"));
            Console.WriteLine(looks.get(0).getString("hair"));
   

        }
        void testJsonObject()
        {
            string path = @"..\..\resources\test2.json";
            Json j = new Json();
            j.openFile(path);
            JsonObject js = (JsonObject)j.parse();
            Console.WriteLine(js.getString("name"));
           string[] friends= js.getJsonObject("info").getJsonObject("personal").getStringList("friends");
            // js.getStringList("")
            foreach(string s in friends)
            Console.WriteLine(s);
        }
    }
 

 
}
