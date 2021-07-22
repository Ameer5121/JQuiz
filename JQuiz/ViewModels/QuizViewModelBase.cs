using JQuiz.Commands;
using JQuiz.Extensions;
using JQuiz.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JQuiz.ViewModels
{
    abstract class QuizViewModelBase : ViewModelBase
    {
        protected int _questionIndex = 0;
        protected string _currentQuestion;
        protected string _currentCorrectAnswer;
        protected string _rawContent;
        protected string _userInput;
        public bool? _isAnswerCorrect = null;
        protected IDictionary<string, string> _questionsAndAnswers;
        public ICommand Check => new RelayCommand(CheckAnswer);
        public ICommand Reveal => new RelayCommand(RevealAnswer);
        public ICommand SelectNextQuestion => new RelayCommand(SelectQuestion, CanChangeQuestion);
        public ICommand SelectPreviousQuestion => new RelayCommand(SelectQuestion, CanSelectPreviousQuestion);
        public ICommand Randomize => new RelayCommand(RandomizeQuestions, CanChangeQuestion);
        public ICommand Redo => new RelayCommand(StartOver, CanChangeQuestion);
        protected QuizViewModelBase(Dictionary<string, string> questionsAndAnswers, string rawContent)
        {
            this._questionsAndAnswers = questionsAndAnswers;
            this._rawContent = rawContent;
            _currentQuestion = _questionsAndAnswers.Keys.First();
            _currentCorrectAnswer = _questionsAndAnswers[_currentQuestion];
        }
        public string CurrentQuestion
        {
            get => _currentQuestion;
            set => SetPropertyValue(ref _currentQuestion, value);
        }
        public bool? IsAnswerCorrect
        {
            get => _isAnswerCorrect;
            set => SetPropertyValue(ref _isAnswerCorrect, value);
        }
        public string UserInput
        {
            get => _userInput;
            set => SetPropertyValue(ref _userInput, value);
        }
        public string CurrentCorrectAnswer => _currentCorrectAnswer;
        protected abstract void CheckAnswer();

        private bool CanSelectPreviousQuestion()
        {
            return _questionIndex == 0 ? false : true;
        }
        private bool CanChangeQuestion()
        {
            return _questionsAndAnswers.Count == 1 ? false : true;
        }

        protected virtual void SelectQuestion(SelectionType answerType)
        {
            if (_questionIndex < _questionsAndAnswers.Count - 1)
            {
                if (answerType == SelectionType.Next) _questionIndex++;
                else if (answerType == SelectionType.Previous && _questionIndex != 0) _questionIndex--;
                var question = _questionsAndAnswers.ElementAt(_questionIndex);
                CurrentQuestion = question.Key;
                _currentCorrectAnswer = question.Value;
                TryResetInput();
                TryResetStatus();
            }
            else
            {
                _questionIndex = 0;
                SelectQuestion(SelectionType.CurrentIndex);              
            }
        }

        protected void RandomizeQuestions()
        {
            Dictionary<string, string> questionsAndAnswers = new Dictionary<string, string>();
            var randomizedQuestions = _rawContent.RandomizeQuestions();
            var splittedContent = randomizedQuestions.Split(new string[] { "\\", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            splittedContent.RemoveWhiteSpace();
            questionsAndAnswers.AddSplittedContent(splittedContent);
            this._questionsAndAnswers = questionsAndAnswers;
            _questionIndex = 0;
            SelectQuestion(SelectionType.CurrentIndex);
        }

        protected abstract void RevealAnswer();

        protected abstract bool TryResetInput();

        private bool TryResetStatus()
        {
            if (_isAnswerCorrect != null)
            {
                IsAnswerCorrect = null;
                return true;
            }
            return false;
        }

        protected void StartOver()
        {
            Dictionary<string, string> questionsAndAnswers = new Dictionary<string, string>();
            var splittedContent = _rawContent.Split(new string[] { "\\", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            splittedContent.RemoveWhiteSpace();
            questionsAndAnswers.AddSplittedContent(splittedContent);
            this._questionsAndAnswers = questionsAndAnswers;
            _questionIndex = 0;
            SelectQuestion(SelectionType.CurrentIndex);
        }
    }
}
