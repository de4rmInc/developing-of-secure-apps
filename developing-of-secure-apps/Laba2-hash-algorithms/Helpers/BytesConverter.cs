using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2_hash_algorithms.Helpers
{
    public static class BytesConverter
    {
        public static string ConvertToHexString(this byte[] bytes)
        {
            if (bytes == null)
            {
                return string.Empty;
            }
            var hex = BitConverter.ToString(bytes).Replace("-", string.Empty);

            return hex;
        }

        public static string ConvertToBinString(this byte[] bytes)
        {
            if (bytes == null)
            {
                return string.Empty;
            }

            var bins = new StringBuilder();
            var bin = string.Empty;

            foreach (var b in bytes)
            {
                bin = Convert.ToString(b, 2);
                bins.Append(bin);
                bins.Append(" ");
            }

            return bins.ToString().TrimEnd();
        }
    }
}
