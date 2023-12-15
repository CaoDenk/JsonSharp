using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonSharp
{
    public class OneToken
    {
        public Token token;
        public object value;
        public OneToken(Token token, Object value)
        {
            this.token = token;
            this.value = value;
        }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder($"{token} [");

            if (token == Token.VALUE_ARRAY)
            {
                foreach (var i in (object[])value)
                {
                    if (i is string s)
                    {
                        builder.Append($"\"{s} ,");
                    }
                    else builder.Append($"{i.ToString()},");
                }
                builder.Append("\b]");
                return builder.ToString();
            }
            return $"{token}\t {value}";
        }
    }
}
