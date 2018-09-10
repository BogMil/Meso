using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace GenericCSR
{
    public static class StringExtensions
    {
        public static JToken TryParseJToken(this string str)
        {
            try
            {
                return JToken.Parse(str);
            }
            catch
            {
                throw new Exception("Can not parse Jtoken from str: "+ str);
            }
        }
    }
}
