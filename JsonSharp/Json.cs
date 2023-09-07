using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonSharp
{
    public class Json
    {
        char[] buf;
        int line = 0;
        //const int bufferMaxSize = 1024 * 1024 * 4;
        int len = 0;
        /// <summary>
        /// 从硬盘中读取json文件到json对象中
        /// </summary>
        /// <param name="path"></param>
        /// <exception cref="Exception"></exception>
        public void OpenFile(string path)
        {

            if (File.Exists(path))
            {         
         
                byte[] bytes= File.ReadAllBytes(path);

                if (bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF) //判断包含BOM头
                {
                    buf = Encoding.UTF8.GetChars(bytes, 3, bytes.Length - 3);
                }
                else
                    buf = Encoding.UTF8.GetChars(bytes);

                len = buf.Length;
            }
            else
                throw new Exception($"{path} can't be found!");
        }

        public List<OneToken> tokens;
        public void Tokenize()
        {
            int i = 0;
            tokens = new List<OneToken>();
            for (; ; )
            {
                SkipWhite(ref i);
                if (i < len)
                {
                    if (buf[i] == '[')
                    {
                        i++;
                        SkipWhite(ref i);
                        if (buf[i] != '{')
                        {
                            tokens.Add(new OneToken(Token.VALUE_ARRAY, ReadValueObjectArray(ref i)));
                            i++;
                        }
                        else
                        {
                            tokens.Add(new OneToken(Token.ARRAY_BEGIN, "["));
                            tokens.Add(new OneToken(Token.OBJECT_BEGIN, "{"));
                            i++;
                        }

                        continue;
                    }
                    else if (buf[i] == ']')
                    {
                        i++;
                        tokens.Add(new OneToken(Token.ARRAY_END, "]"));
                        continue;
                    }
                    else if (buf[i] == '{')
                    {
                        tokens.Add(new OneToken(Token.OBJECT_BEGIN, "{"));
                        i++;
                        continue;
                    }
                    else if (buf[i] == '}')
                    {
                        tokens.Add(new OneToken(Token.OBJECT_END, "}"));
                        i++;
                        continue;
                    }
                    else if (buf[i] >= '0' && buf[i] <= '9')
                    {
                        string res = ReadNumber(ref i, buf[i], out bool intflag);
                        if (intflag)
                        {

                            tokens.Add(new OneToken(Token.INT, int.Parse(res)));
                        }
                        else
                        {
                            tokens.Add(new OneToken(Token.DOUBLE, double.Parse(res)));
                        }

                        continue;
                    }
                    else if (buf[i] == '"')
                    {
                        tokens.Add(new OneToken(Token.VALUE_STRING, ReadString(ref i)));
                        i++;
                        continue;
                    }
                    else if (buf[i] == ':')
                    {
                        if (tokens.Last().token == Token.VALUE_STRING)
                            tokens.Last().token = Token.KEY_STRING;

                        tokens.Add(new OneToken(Token.COLON, ":"));
                        i++;
                        continue;
                    }
                    else if (buf[i] == ',')
                    {
                        tokens.Add(new OneToken(Token.COMMA, ","));
                        i++;
                        continue;
                    }
                }
                else
                {
                    tokens.Add(new OneToken(Token.END, '0'));
                    return;
                }

            }

        }
        /// <summary>
        /// 跳过空字符
        /// </summary>
        /// <param name="i"></param>
        void SkipWhite(ref int i)
        {
            while (i < len &&  (buf[i] == ' '
                || buf[i] == '\t'
                || buf[i] == '\b'
                || buf[i] == '\n'
                || buf[i] == '\r'))
            {
                if (buf[i] == '\n')
                    ++line;
                i++;
            }

        }
        string ReadNumber(ref int i, char c, out bool intFlag)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(c);
            i++;
            while ((i < len && buf[i] >= '0' && buf[i] <= '9'))
            {
                builder.Append(buf[i]);
                i++;
            }
            if (buf[i] == '.')
            {
                intFlag = false;
                builder.Append(buf[i]);
                i++;
                while ((i < len && buf[i] >= '0' && buf[i] <= '9'))
                {
                    builder.Append(buf[i]);
                    i++;
                }
            }
            else
                intFlag = true;

            return builder.ToString();

        }
        string ReadString(ref int i)
        {
            i++;
            StringBuilder builder = new StringBuilder(buf[i]);

            while (i < len && buf[i] != '"')
            {
                builder.Append(buf[i]);
                i++;
            }
            if (i >= len)
            {
                throw new Exception("lack '\"' ");
            }
            return builder.ToString();
        }
        object ReadValueObjectArray(ref int i)
        {
            SkipWhite(ref i);

            if (buf[i] >= '0' && buf[i] <= '9')
            {
                List<object> vs = new List<object> { };
                do
                {
                    string res = ReadNumber(ref i, buf[i], out bool intflag);
                    
                    if (intflag)
                    {
                        vs.Add(int.Parse(res));
                    }
                    else
                    {
                        vs.Add(double.Parse(res));
                    }

                } while (NextIsOk(',', ref i) && buf[i] >= '0' && buf[i] <= '9');
                if (i >= len)
                {
                    throw new Exception("lack  ']'  ");
                }
                if (buf[i] == ']')
                    return vs.ToArray();

            }
            else if (buf[i] == '"')
            {
                List<string> vs = new List<string> ();
                do
                {
                    vs.Add(ReadString(ref i));
                    i++;
                } while (NextIsOk(',', ref i) && buf[i] == '"');
                if (buf[i] == ']')
                    return vs.ToArray();
            }
            throw new Exception("unexpect ->'" + buf[i] + "'  ");
        }
        bool NextIsOk(char expext, ref int i)
        {
            SkipWhite(ref i);
            if (i < len && buf[i] == expext)
            {
                i++;
                SkipWhite(ref i);
                return true;
            }
            return false;
        }


        public object Parse()
        {
            Tokenize();
            int j = 0;
            switch (tokens.ElementAt(j).token)
            {
                case Token.ARRAY_BEGIN:
                    j++;
                    JsonArray jsonArray = ParseJsonArray(ref j);
                    if (tokens.ElementAt(j).token == Token.END)
                        return jsonArray;
                    else break;
                case Token.OBJECT_BEGIN:
                    {
                        j++;
                        JsonObject jsonObject = ParseJsonObject(ref j);
                        if (tokens.ElementAt(j).token == Token.END)
                            return jsonObject;
                        else break;
                    }

                default:
                    throw new Exception($"unexpect ->'{tokens.ElementAt(j).value}',line at {line}");

            }
            throw new Exception($"unexpect ->'{tokens.ElementAt(j).value}',line at {line}");
        }
        JsonArray ParseJsonArray(ref int j)
        {
            JsonArray jsonObjects = new JsonArray();
            for (; ; )
            {
                if (tokens.ElementAt(j).token == Token.OBJECT_BEGIN)
                {
                    j++;
                    jsonObjects.Put(ParseJsonObject(ref j));
                }
                if (tokens.ElementAt(j).token != Token.COMMA)
                {
                    break;
                }
                else
                    j++;

            }
            if (tokens.ElementAt(j).token == Token.ARRAY_END)
            {
                j++;
                return jsonObjects;
            }
            throw new Exception($"unexpect ->'{tokens.ElementAt(j).value}',line at {line}");

        }

        JsonObject ParseJsonObject(ref int j)
        {
            JsonObject jsonObject = new JsonObject();
            string key = null;

            while (j < tokens.Count)
            {
                OneToken currentToken = tokens.ElementAt(j);
                switch (currentToken.token)
                {
                    case Token.KEY_STRING:

                        NextTokenIsOk(Token.KEY_STRING, tokens.ElementAt(++j ));
                        key = (string)currentToken.value;

                        break;
                    case Token.INT:
                        NextTokenIsOk(Token.INT, tokens.ElementAt(++j ));
                        jsonObject.Put(key, currentToken.value);
                        break;
                    case Token.DOUBLE:
                        NextTokenIsOk(Token.DOUBLE, tokens.ElementAt(++j));
                        jsonObject.Put(key, currentToken.value);
                        break;
                    case Token.VALUE_ARRAY:
                        NextTokenIsOk(Token.VALUE_ARRAY, tokens.ElementAt(++j ));

                        jsonObject.Put(key, currentToken.value);
                        break;
                    case Token.COLON:
                        NextTokenIsOk(Token.COLON, tokens.ElementAt(++j));               
                        break;

                    case Token.VALUE_STRING:
                        if (tokens.ElementAt(j + 1).token == Token.COLON)
                        {
                            tokens.ElementAt(j).token = Token.KEY_STRING;
                        }else
                        {
                            NextTokenIsOk(Token.VALUE_STRING, tokens.ElementAt(++j));
                            jsonObject.Put(key, currentToken.value);
                        }
                        break;

                    case Token.COMMA:
                        NextTokenIsOk(Token.COMMA, tokens.ElementAt(++j));
                        break;
                    case Token.OBJECT_BEGIN:
                        j++;
                        jsonObject.Put(key, ParseJsonObject(ref j));
                        break;
                    case Token.OBJECT_END:
                        j++;
                        return jsonObject;
                    case Token.ARRAY_BEGIN:
                        NextTokenIsOk(Token.ARRAY_BEGIN, tokens.ElementAt(++j));
                        jsonObject.Put(key, ParseJsonArray(ref j));
                        break;
                    default:
                        throw new Exception($"unexpect ->'{tokens.ElementAt(j).value}',line at {line}");

                }

            }
            if (tokens.ElementAt(j).token == Token.END)
                return jsonObject;
            throw new Exception("lack }");
        }

        void NextTokenIsOk(Token currentToken, OneToken nextToken)
        {
            bool flag = false;
            if (currentToken == Token.KEY_STRING)
                flag = nextToken.token == Token.COLON;
            else if (currentToken == Token.COLON)//:
            {
                flag = (nextToken.token == Token.VALUE_ARRAY
                    || nextToken.token == Token.INT
                    || nextToken.token == Token.DOUBLE
                    || nextToken.token == Token.VALUE_STRING
                    || nextToken.token == Token.OBJECT_BEGIN
                    || nextToken.token == Token.ARRAY_BEGIN);

            }
            else if (currentToken == Token.OBJECT_END
                 || currentToken == Token.VALUE_STRING
                 || currentToken == Token.VALUE_ARRAY
                 || currentToken == Token.INT
                 || currentToken == Token.DOUBLE)
                flag = nextToken.token == Token.COMMA || nextToken.token == Token.OBJECT_END || nextToken.token == Token.ARRAY_END;

            else if (currentToken == Token.COMMA)
                flag = nextToken.token == Token.KEY_STRING
                   || nextToken.token == Token.OBJECT_END;
            else if (currentToken == Token.KEY_STRING)
                flag = nextToken.token == Token.COLON;
            else if (currentToken == Token.ARRAY_BEGIN)
                flag = nextToken.token == Token.OBJECT_BEGIN;
            if (!flag)
                throw new Exception($"unexpect token->'{nextToken.value}'");

        }

    }

   

   

  
}
