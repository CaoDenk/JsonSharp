using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonSharp
{
    public class JsonObject
    {
        Dictionary<string, object> dict = new Dictionary<string, object> { };
        public T GetValue<T>(string key) //只要这一个支持泛型就行
        {
            return (T)dict[key];
        }

        public object GetValue(string key)
        {
            dict.TryGetValue(key, out object obj);
            return obj;
        }
     
        public void Put(string key, object value)
        {
            dict.Add(key, value);
        }

        public override string ToString()
        {
            StringBuilder res = new StringBuilder('{');
            int i = 0;
            foreach (string s in dict.Keys)
            {
                ++i;
                res.Append('"').Append(s).Append('"');
                if (dict[s] is JsonObject jso)
                {
                    res.Append(jso.ToString());

                    if (dict.Keys.Count > i)
                    {
                        res.Append(',');
                        continue;
                    }
                    else break;
                }

                if (dict[s] is string str)
                {
                    res.Append($"\"{str}\"");
                    if (dict.Keys.Count > i)
                    {
                        res.Append(',');
                        continue;
                    }
                    else
                        break;
                }
                res.Append(dict[s]);

                if (dict.Keys.Count > i)
                {
                    res.Append(',');
                }
                else
                    break;

            }

            return res.Append('}').ToString();
        }


        public bool ContainsKey(string key)=> dict.ContainsKey(key);


    }
}
