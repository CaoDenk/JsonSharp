using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonSharp
{
    class Test
    {
        static void Main(string[] args)
        {
            Test p = new Test();

            p.TestJsonArray();
            p.TestJsonObject();
            p.Testjson3();
            p.TestPut();
            Console.ReadLine();
        }
        void TestJsonArray()
        {
            string path = @"resources\test.json";
            Json j = new Json();
            j.OpenFile(path);
            var js0 = (JsonArray)j.Parse();

            //foreach (var t in j.tokens)
            //{
            //    Console.WriteLine(t.ToString());
            //}
            JsonObject js = js0.Get(0);
            Console.WriteLine(js.getInt("age"));
            JsonObject honghong = js0.Get(1);
            JsonArray  friends= honghong.getJsonObject("info").getJsonObject("personal").getJsonArray("friends");
            JsonArray looks  =    friends.Get(1).getJsonArray("friendslooks");
            Console.WriteLine(friends.Get(1).getInt("age"));
            Console.WriteLine(looks.Get(0).getString("hair"));
   

        }
        void TestJsonObject()
        {
            string path = @"resources\test2.json";
            Json j = new Json();
            j.OpenFile(path);
            JsonObject js = (JsonObject)j.Parse();
            Console.WriteLine(js.getString("name"));
            string[] friends= js.getJsonObject("info").getJsonObject("personal").getStringList("friends");
            // js.getStringList("")
            foreach(string s in friends)
            Console.WriteLine(s);
        }

        void Testjson2()
        {

            string path = @"resources\test2.json";
            Json j = new Json();
            j.OpenFile(path);
            JsonObject js = (JsonObject)j.Parse();
            object[] jsonArray = (object[])js.getValue("score");

            foreach(var obj in jsonArray)
            {
                Console.WriteLine(obj);
            }
          

        }
        /// <summary>
        /// 测试汉字是否会乱码
        /// </summary>
        void Testjson3()
        {
            string path = @"resources\test3.json";
            Json j = new Json();
            j.OpenFile(path);
            JsonObject js = (JsonObject)j.Parse();
            Console.WriteLine(js.getString("filename"));
            //string[] friends = js.getJsonObject("info").getJsonObject("personal").getStringList("friends");
            //// js.getStringList("")
            //foreach (string s in friends)
            //    Console.WriteLine(s);
        }
        void TestPut()
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
