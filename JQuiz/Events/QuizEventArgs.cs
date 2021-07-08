using JQuiz.Helper;
using JQuiz.Helper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JQuiz.Events
{
    class QuizEventArgs : EventArgs
    {
        public Dictionary<string, string> QuestionsAndAnswers { get; set; }
        public string RawContent { get; set; }
        public IQuiz CurrentQuizMode { get; set; }
    }
}
