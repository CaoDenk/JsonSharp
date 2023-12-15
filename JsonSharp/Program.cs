namespace JsonSharp
{
    internal class Program
    {
        static void TestJsonArray()
        {
            string path = @"resources\test.json";
            Json j = new Json();
            j.OpenFile(path);
            var js0 = (JsonArray)j.Parse();
            Console.WriteLine(js0.ToString());
            //foreach (var t in j.tokens)
            //{
            //    Console.WriteLine(t.ToString());


        }


        static void Testjson2()
        {

            string path = @"resources\test2.json";
            Json j = new Json();
            j.OpenFile(path);
            JsonObject js = (JsonObject)j.Parse();
            object[] jsonArray = (object[])js.GetValue("score");

            foreach (var obj in jsonArray)
            {
                Console.WriteLine(obj);
            }


        }
        /// <summary>
        /// 测试汉字是否会乱码
        /// </summary>
        static void Testjson3()
        {
            string path = @"resources\test3.json";
            Json j = new Json();
            j.OpenFile(path);
            JsonObject js = (JsonObject)j.Parse();

            //string[] friends = js.getJsonObject("info").getJsonObject("personal").getStringList("friends");
            //// js.getStringList("")
            //foreach (string s in friends)
            //    Console.WriteLine(s);
        }
        static void TestPut()
        {

            JsonObject jsonObject = new JsonObject();
            JsonObject child = new JsonObject();
            child.Put("name", "小明");
            child.Put("AGE", 25);
            jsonObject.Put("filename", child);
            Console.Write(jsonObject.ToString());

        }

        static void Main(string[] args)
        {
            Testjson2();

        }
        //static void Main(string[] args)
        //{

        //    string path = @"resources\deserialize.json";
        //    Json j = new Json();
        //    j.OpenFile(path);
        //    JsonObject js = (JsonObject)j.Parse();
        //    Console.WriteLine(js.ToString());
        //}
    }
}