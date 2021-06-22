using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JQuiz.Extensions
{
    public static class TextExtensions
    {
        public static string RandomizeQuestions(this string rawContent)
        {
            var splittedContent = rawContent.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            string newContent = "";
            Random random = new Random();
            for (int x = 0; x < splittedContent.Length; x++)
            {
                ref var currentElement = ref splittedContent[x];
                ref var randomElement = ref splittedContent[random.Next(splittedContent.Length)];
                var tempElement = currentElement;
                currentElement = randomElement;
                randomElement = tempElement;
            }
            foreach (string s in splittedContent)
            {
                newContent += s;
                newContent += Environment.NewLine;
            }
            return newContent;
        }
    }
}
