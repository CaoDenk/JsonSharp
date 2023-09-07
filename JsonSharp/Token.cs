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

        COMMA,// ,逗号
        COLON,//冒号
        VALUE_ARRAY,//[



        INT,
        //LONG,
        DOUBLE,
        NUMBER,//应该定义一个
        STRING,
        BOOL,



        KEY_STRING,//
        VALUE_STRING,
       
        END,
       
    }
    enum Number
    {
        INT,
        LONG,
        FLOAT,
        DOUBLE,
    }

    enum ValueType
    {
        Number,
        Bool,
        String
    }


}
