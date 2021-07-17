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
        public event EventHandler OnAnswerCheck;
        public TextQuizViewModel(Dictionary<string, string> questionsAndAnswers, string rawContent) : base(questionsAndAnswers, rawContent)
        {
        }
        public string CurrentAnswer
        {
            get => _currentAnswer;
            set => SetPropertyValue(ref _currentAnswer, value);
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
        protected override void SelectAnswer(SelectionType answerType)
        {
            if (_questionIndex < _questionsAndAnswers.Count - 1)
            {
                if (answerType == SelectionType.Next) _questionIndex++;
                else if (answerType == SelectionType.Previous && _questionIndex != 0) _questionIndex--;
                var question = _questionsAndAnswers.ElementAt(_questionIndex);
                CurrentQuestion = question.Key;
                CurrentAnswer = question.Value;
                Reset();
            }
            else
            {
                _questionIndex = 0;
                SelectAnswer(SelectionType.CurrentIndex);
            }                     
        }
        protected override void RevealAnswer()
        {
            UserInput = _currentAnswer;
        }
        private void ResetFocus()
        {
            OnAnswerCheck?.Invoke(this, EventArgs.Empty);
        }
        protected override void Reset()
        {
            UserInput = null;
            IsAnswerCorrect = null;
        }
    }
}
