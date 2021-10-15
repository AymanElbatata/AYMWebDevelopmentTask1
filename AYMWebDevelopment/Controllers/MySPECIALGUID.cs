using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AYMWebDevelopment.Controllers
{
    public class MySPECIALGUID
    {
        public static string GetUniqueKey(int length)
        {
            string guidResult = string.Empty;

            while (guidResult.Length < length)
            {
                // Get the GUID.
                guidResult += Guid.NewGuid().ToString().GetHashCode().ToString("x");
            }

            // Make sure length is valid.
            if (length <= 0 || length > guidResult.Length)
                throw new ArgumentException("Length must be between 1 and " + guidResult.Length);

            // Return the first length bytes.
            return guidResult.Substring(0, length);
        }


        public static string GenerateGUID(int first, int second)
        {
            string get1, get2, allready;
            get1 = GetUniqueKey(first);
            get2 = GetUniqueKey(second);
            allready = get1 + "_" + get2;
            return allready;
        }


        public static string GenerateMyCode(string first, string usercreation, string last)
        {
            string get1, get2, allready;
            get1 = GetUniqueKey(5);
            get2 = "" + DateTime.Now.Millisecond + DateTime.Now.Hour;
            allready = first + get1 + usercreation + get2 + last;
            return allready;
        }


    }
}