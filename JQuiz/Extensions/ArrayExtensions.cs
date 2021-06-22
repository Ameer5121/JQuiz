using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JQuiz.Extensions
{
    public static class ArrayExtensions
    {
        public static void RemoveWhiteSpace(this string[] splittedContent)
        {
            for (int x = 0; x < splittedContent.Length; x++)
            {
                var trimmedString = splittedContent[x].Trim();
                splittedContent[x] = trimmedString;
            }
        }
    }
}
