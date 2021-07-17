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
        protected string _currentAnswer;
        protected string _rawContent;
        protected string _userInput;
        public bool? _isAnswerCorrect = null;
        protected IDictionary<string, string> _questionsAndAnswers;
        public ICommand Check => new RelayCommand(CheckAnswer);
        public ICommand Reveal => new RelayCommand(RevealAnswer);
        public ICommand Select => new RelayCommand(SelectAnswer, CanChangeQuestion);
        public ICommand Randomize => new RelayCommand(RandomizeQuestions, CanChangeQuestion);
        public ICommand Redo => new RelayCommand(StartOver, CanChangeQuestion);

        protected QuizViewModelBase(Dictionary<string, string> questionsAndAnswers, string rawContent)
        {
            this._questionsAndAnswers = questionsAndAnswers;
            this._rawContent = rawContent;
            _currentQuestion = _questionsAndAnswers.Keys.First();
            _currentAnswer = _questionsAndAnswers[_currentQuestion];
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

        protected abstract void CheckAnswer();

        private bool CanChangeQuestion()
        {
            return _questionsAndAnswers.Count == 1 ? false : true;
        }
        protected abstract void SelectAnswer(SelectionType answerType);

        protected void RandomizeQuestions()
        {
            Dictionary<string, string> questionsAndAnswers = new Dictionary<string, string>();
            var randomizedQuestions = _rawContent.RandomizeQuestions();
            var splittedContent = randomizedQuestions.Split(new string[] { "\\", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            splittedContent.RemoveWhiteSpace();
            questionsAndAnswers.AddSplittedContent(splittedContent);
            this._questionsAndAnswers = questionsAndAnswers;
            _questionIndex = 0;
            SelectAnswer(SelectionType.CurrentIndex);
        }

        protected abstract void RevealAnswer();

        protected abstract void Reset();

        protected void StartOver()
        {
            Dictionary<string, string> questionsAndAnswers = new Dictionary<string, string>();
            var splittedContent = _rawContent.Split(new string[] { "\\", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            splittedContent.RemoveWhiteSpace();
            questionsAndAnswers.AddSplittedContent(splittedContent);
            this._questionsAndAnswers = questionsAndAnswers;
            _questionIndex = 0;
            SelectAnswer(SelectionType.CurrentIndex);
        }
    }
}
