using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JQuiz.Helper.Interfaces
{
    interface IQuiz
    {
        public void StartQuiz(Dictionary<string, string> questionsAndAnswers, string rawContent);
    }
}
