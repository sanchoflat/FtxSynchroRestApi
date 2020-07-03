using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FtxRestSynchro.Rest.Parsers
{
    public static class ParserHelpers
    {
        public static JToken Parse(string data)
        {
            if (string.IsNullOrEmpty(data)) return null;
            var token = JsonConvert.DeserializeObject<JToken>(data);
            return token;
        }

        public static decimal ParseDecimal(JToken token)
        {
            var s = token.ToString();
            if (string.IsNullOrEmpty(s)) return 0;
            return decimal.Parse(s, NumberStyles.Float, CultureInfo.CurrentCulture);
        }

        public static decimal ParseDecimal(JToken token, bool canBeNull)
        {
            if (token == null) return 0;
            var s = token.ToString();
            if (string.IsNullOrEmpty(s)) return 0;
            return decimal.Parse(s, NumberStyles.Float, CultureInfo.CurrentCulture);
        }

        public static bool ParseBool(JToken token)
        {
            return bool.Parse(token.ToString());
        }

        public static DateTime ParseDateTime(JToken token)
        {
            return ((DateTime)token).ToUniversalTime();
        }

        public static string ParseString(JToken jToken)
        {
            if (jToken == null) return "";
            return jToken.ToString();
        }
    }
}