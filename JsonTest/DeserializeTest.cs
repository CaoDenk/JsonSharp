using JsonSharp;
namespace JsonTest
{

    public class DeserializeTest
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
            var t = deserializer.Deserialize<User>(js);
            Console.WriteLine(t.age);
            Console.WriteLine(t.address.city);
            Console.WriteLine(t.address.street);

        }
    }



}
