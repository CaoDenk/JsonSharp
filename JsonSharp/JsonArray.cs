using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonSharp
{
    public class JsonArray
    {
        private List<JsonObject> Jsons = new List<JsonObject> ();
        public void Put(JsonObject json)=> Jsons.Add(json);

        public JsonObject ElementAt(int index)=> Jsons.ElementAt(index);
        public JsonObject this[int index]=> Jsons.ElementAt(index);
      


    }
}
