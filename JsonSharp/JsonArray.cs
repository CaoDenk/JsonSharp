using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonSharp
{
    public class JsonArray
    {
        private List<JsonObject> jsons = new List<JsonObject> ();
        public void Put(JsonObject json)
        {
            jsons.Add(json);
        }
        public JsonObject ElementAt(int index)=> jsons.ElementAt(index);
        public JsonObject this[int index]=>jsons.ElementAt(index);
      


    }
}
