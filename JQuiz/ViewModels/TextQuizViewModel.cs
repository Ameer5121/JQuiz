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
        private string _currentAnswer;
        private string _userInput;
        public event EventHandler OnAnswerCheck;
        public TextQuizViewModel(Dictionary<string, string> questionsAndAnswers, string rawContent)
        {
            this._questionsAndAnswers = questionsAndAnswers;
            this._rawContent = rawContent;
            _currentQuestion = _questionsAndAnswers.Keys.First();
            _currentAnswer = _questionsAndAnswers[_currentQuestion];
        }
        public string CurrentAnswer
        {
            get => _currentAnswer;
            set => SetPropertyValue(ref _currentAnswer, value);
        }
        public string UserInput
        {
            get => _userInput;
            set => SetPropertyValue(ref _userInput, value);
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
