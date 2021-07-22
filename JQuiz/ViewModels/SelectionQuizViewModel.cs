using JQuiz.Commands;
using JQuiz.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JQuiz.ViewModels
{
    class SelectionQuizViewModel : QuizViewModelBase
    {
        private string[] _answers;
        private string _selectedAnswer;
        public event EventHandler ResetSelection;
        public event EventHandler Reveal;
        public string[] Answers
        {
            get => _answers;
            set => SetPropertyValue(ref _answers, value);
        }
        public string SelectedAnswer
        {
            get => _selectedAnswer;
            set => _selectedAnswer = value;
        }
        public ICommand SelectAnswerCommand => new RelayCommand(SelectAnswer);
        public SelectionQuizViewModel(Dictionary<string, string> questionsAndAnswers, string rawContent) : base(questionsAndAnswers, rawContent)
        {
            _answers = InitializeRandomAnswers();
        }


        protected override void CheckAnswer()
        {
            if (_selectedAnswer == _currentCorrectAnswer)
            {
                IsAnswerCorrect = true;
            }
            else
            {
                IsAnswerCorrect = false;
            }
        }
        protected override void SelectQuestion(SelectionType answerType)
        {
            base.SelectQuestion(answerType);
            Answers = InitializeRandomAnswers();
        }
        private void SelectAnswer(string answer)
        {
            _selectedAnswer = answer;
        }
        protected override void RevealAnswer()
        {
            TryResetInput();
            Reveal?.Invoke(this, EventArgs.Empty);
        }

        protected override bool TryResetInput()
        {
            if (_selectedAnswer != null)
            {
                _selectedAnswer = null;
                ResetSelection?.Invoke(this, EventArgs.Empty);
                return true;
            }
            return false;
        }

        private string[] InitializeRandomAnswers()
        {
            var random = new Random();
            var answers = new string[4];
            var answersSelected = new List<string>();
            answers[random.Next(0, answers.Count())] = _currentCorrectAnswer;
            var filteredCollection = _questionsAndAnswers.Where(x => x.Value != _currentCorrectAnswer);
            for (int i = 0; i < answers.Count(); i++)
            {
                if (answers[i] == null)
                {
                    answers[i] = filteredCollection.ElementAt(random.Next(0, filteredCollection.Count() - 1)).Value;
                    answersSelected.Add(answers[i]);
                    filteredCollection = filteredCollection.Where(x =>
                    {
                        foreach (string answerSelected in answersSelected)
                        {
                            if (x.Value == answerSelected) return false;
                        }
                        return true;
                    });

                }
            }
            return answers;
        }
    }
}
