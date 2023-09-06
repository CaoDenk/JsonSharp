using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace JsonSharp
{
    public class Deserializer
    {

        /// <summary>
        /// 反序列化List<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonArray"></param>
        /// <returns></returns>
        public List<T> Deserialize<T>(JsonArray jsonArray)where T:new() 
        {

            return null;
        }

        public T Deserialize<T>(JsonObject jsobject) where T : new()
        {
            Type type = typeof(T);
            return (T)GetInstance(jsobject,type);
        }

        /// <summary>
        /// 返回List<T>
        /// </summary>
        /// <param name="jsonArray"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        object GetInstance(JsonArray jsonArray, Type type)
        {
            return null;
        }

        object GetInstance(JsonObject jsobject, Type type)
        {
            object obj = Activator.CreateInstance(type);
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (jsobject.ContainsKey(property.Name))
                {
                    var value = GetValue(jsobject,property.Name,property.PropertyType);
                    property.SetValue(obj, value);
                }
            }
            return obj;
        }

        object GetValue(JsonObject jsobject,string key,Type type)
        {
            if (jsobject == null)
                return null;
            if (jsobject.GetValue(key).GetType() == typeof(JsonObject))
            {
                return GetInstance(jsobject.GetValue<JsonObject>(key), type);
            }
            //json中支持 "key":[]
            //得判别[]中是[object,...] 还是[JsonObject...]
            //if(jsobject.GetValue(key).GetType().GetGenericTypeDefinition()==typeof(List<>))
            //{
            //    List<object> arr=jsobject.GetValue<List<object>>(key);
            //    if (arr.Count == 0)
            //        return null;
            //    else if (arr[0].GetType()==typeof(JsonObject))//说明是
            //    {
            //        List<JsonObject> jsonArr = new List<JsonObject>();
            //        for(int i=0; i<arr.Count;++i)
            //        {
            //            //jsonArr.Add()
            //        }

                    
            //        ///
            //    }

            //    //return GetInstance(jsobject.GetValue<List<object>>(key), type);
            //}

            switch (type)
            {

                case Type when type == typeof(int):
                    return jsobject.GetValue<int>(key);
                case Type when type == typeof(float):
                    return jsobject.GetValue<float>(key);
                case Type when type == typeof(double):
                    return jsobject.GetValue<double>(key);
                case Type when type == typeof(string):
                    return jsobject.GetValue<string>(key);
                case Type when type == typeof(bool):
                    return jsobject.GetValue<bool>(key);
                //case Type when type==typeof(JsonObject):
                //    return GetInstance(jsonNode.AsObject(), type);
                default:
                    //Console.WriteLine(jsonNode.GetType());
                    throw new Exception("类型不匹配");
                    //return null;
            }

        }
    }
}
