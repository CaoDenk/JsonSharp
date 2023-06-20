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
            p.testjson3();
            p.testPut();
            Console.ReadLine();
        }
        void testJsonArray()
        {
            string path = @"..\..\resources\test.json";
            Json j = new Json();
            j.openFile(path);
            var js0 = (JsonArray)j.parse();

            //foreach (var t in j.tokens)
            //{
            //    Console.WriteLine(t.ToString());
            //}
            JsonObject js = js0.get(0);
            Console.WriteLine(js.getInt("age"));
            JsonObject honghong = js0.get(1);
           JsonArray  friends= honghong.getJsonObject("info").getJsonObject("personal").getJsonArray("friends");
           JsonArray looks  =    friends.get(1).getJsonArray("friendslooks");
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

        void testjson2()
        {

            string path = @"..\..\resources\test2.json";
            Json j = new Json();
            j.openFile(path);
            JsonObject js = (JsonObject)j.parse();
            object[] jsonArray = (object[])js.getValue("score");

            foreach(var obj in jsonArray)
            {
                Console.WriteLine(obj);
            }
          

        }
        /// <summary>
        /// 测试汉字是否会乱码
        /// </summary>
        void testjson3()
        {
            string path = @"..\..\resources\test3.json";
            Json j = new Json();
            j.openFile(path);
            JsonObject js = (JsonObject)j.parse();
            Console.WriteLine(js.getString("filename"));
            //string[] friends = js.getJsonObject("info").getJsonObject("personal").getStringList("friends");
            //// js.getStringList("")
            //foreach (string s in friends)
            //    Console.WriteLine(s);
        }
        void testPut()
        {

            JsonObject jsonObject = new JsonObject();
            JsonObject child = new JsonObject();
            child.put("name", "小明");
            child.put("AGE", 25);
            jsonObject.put("filename", child);
            Console.Write(jsonObject.ToString());

        }

    }
 

 
}
