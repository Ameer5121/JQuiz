using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JQuiz.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddSplittedContent(this Dictionary<string, string> dic, string[] splittedContent)
        {
            for (int x = 0; x <= splittedContent.Length - 1; x++)
            {
                dic.Add(splittedContent[x], splittedContent[++x]);
            }
        }
    }
}
