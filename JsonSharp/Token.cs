using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonSharp
{
    public enum Token
    {
        ARRAY_BEGIN,//[
        ARRAY_END,//]
        OBJECT_BEGIN,//{
        OBJECT_END,//}
        COMMA,//
        COLON,
        INT,
        //LONG,
        DOUBLE,
        KEY_STRING,
        VALUE_STRING,
        VALUE_ARRAY,//[]
        END,
       
    }
}
