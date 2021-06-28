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
    class QuizViewModel : ViewModelBase
    {
        private string _currentAnswer;
        private string _currentQuestion;
        private string _userInput;
        private string _rawContent;
        private bool? _isAnswerCorrect = null;
        private int _questionIndex = 0;
        private IDictionary<string, string> _questionsAndAnswers;
        public event EventHandler OnExit;
        public event EventHandler OnAnswerCheck;

        public QuizViewModel(Dictionary<string, string> questionsAndAnswers, string rawContent)
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
        public ICommand Check => new RelayCommand(CheckAnswer);
        public ICommand Reveal => new RelayCommand(RevealAnswer);
        public ICommand Select => new RelayCommand(SelectAnswer);
        public ICommand Randomize => new RelayCommand(RandomizeQuestions);
        public ICommand Redo => new RelayCommand(StartOver);
        public ICommand Exit => new RelayCommand(ExitQuiz);

        private void CheckAnswer()
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
        private void SelectAnswer(SelectionType answerType)
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

 
        private void RandomizeQuestions()
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


        private void RevealAnswer()
        {
            UserInput = _currentAnswer;
        }

        private void Reset()
        {
            UserInput = null;
            IsAnswerCorrect = null;
        }
        private void ResetFocus()
        {
            OnAnswerCheck?.Invoke(this, EventArgs.Empty);
        }
        private void StartOver()
        {
            Dictionary<string, string> questionsAndAnswers = new Dictionary<string, string>();
            var splittedContent = _rawContent.Split(new string[] { "\\", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            splittedContent.RemoveWhiteSpace();
            questionsAndAnswers.AddSplittedContent(splittedContent);
            this._questionsAndAnswers = questionsAndAnswers;
            _questionIndex = 0;
            SelectAnswer(SelectionType.CurrentIndex);
        }
        private void ExitQuiz()
        {
            OnExit?.Invoke(this, EventArgs.Empty);
        }
    }
}
