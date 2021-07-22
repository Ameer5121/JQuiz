using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using JQuiz.Commands;
using JQuiz.Extensions;
using JQuiz.Helper;

namespace JQuiz.ViewModels
{
    class TextQuizViewModel : QuizViewModelBase
    {
        public event EventHandler ResetWindowFocus;
        public TextQuizViewModel(Dictionary<string, string> questionsAndAnswers, string rawContent) : base(questionsAndAnswers, rawContent)
        {
        }
        protected override void CheckAnswer()
        {
            if (_userInput == _questionsAndAnswers[_currentQuestion])
            {
                IsAnswerCorrect = true;
                UserInput = null;
                ResetFocus();
            }
            else
            {
                IsAnswerCorrect = false;
                UserInput = null;
            }
        }                                                
        protected override void RevealAnswer()
        {
            UserInput = _currentCorrectAnswer;
        }
        private void ResetFocus()
        {
            ResetWindowFocus?.Invoke(this, EventArgs.Empty);
        }
        protected override bool TryResetInput()
        {
            if(UserInput != null)
            {
                UserInput = null;
                return true;
            }
            return false;
        }
    }
}
