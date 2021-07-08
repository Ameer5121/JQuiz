using JQuiz.Helper.Interfaces;
using JQuiz.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JQuiz.Helper
{
    class TextQuizMode : IQuiz
    {
        public void StartQuiz(Dictionary<string, string> questionsAndAnswers, string rawContent)
        {
            TextQuizWindow quizWindow = new TextQuizWindow(questionsAndAnswers, rawContent);
            quizWindow.Show();
        }
    }
}
