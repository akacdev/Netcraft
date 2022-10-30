using Netcraft.Entities;
using System;
using System.Linq;
using System.Net;
using System.Text;

namespace Netcraft
{
    public static class Extensions
    {
        public static string UrlEncode(this string value) => WebUtility.UrlEncode(value);

        public static DateTime ToDate(this long value)
            => DateTime.UnixEpoch.AddSeconds(value);

        public static long ToUnixSeconds(this DateTime value)
            => (long)(value - DateTime.UnixEpoch).TotalSeconds;

        public static string ToSnakeCase(this string str)
            => string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x)? "_" + x.ToString() : x.ToString())).ToLower();

        public static string ToApi(this Direction direction) => direction switch
        {
            Direction.Ascending => "ASC",
            Direction.Descending => "DESC",
            _ => throw new NotImplementedException()
        };

        public static string ToApi(this State state) => ConvertStateToApi(state.ToString());

        public static string ConvertStateToApi(string name)
        {
            bool first = true;
            StringBuilder sb = new();
            
            foreach (char x in name)
            {
                char c = char.ToLower(x);

                if (first)
                {
                    first = false;
                    sb.Append(c);
                    continue;
                }

                bool upper = char.IsUpper(x);

                if (!upper) sb.Append(c);
                else sb.Append($" {c}");
            }

            return sb.ToString();
        }

        public static string ToApi(this FileSort sort) => ConvertFileSortToApi(sort.ToString());

        public static string ConvertFileSortToApi(string name)
        {
            return name switch
            {
                "FileName" => "filename",
                "FileState" => "file_state",
                _ => throw new NotImplementedException()
            };
        }
    }
}