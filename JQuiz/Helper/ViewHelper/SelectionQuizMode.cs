using JQuiz.Helper.Interfaces;
using JQuiz.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JQuiz.Helper
{
    class SelectionQuizMode : IQuiz
    {
        public void StartQuiz(Dictionary<string, string> questionsAndAnswers, string rawContent)
        {
            SelectionQuizWindow quizWindow = new SelectionQuizWindow(questionsAndAnswers, rawContent);
            quizWindow.Show();
        }
    }
}
