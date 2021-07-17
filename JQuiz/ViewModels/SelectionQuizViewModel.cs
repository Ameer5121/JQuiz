using JQuiz.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JQuiz.ViewModels
{
    class SelectionQuizViewModel : QuizViewModelBase
    {

        public SelectionQuizViewModel(Dictionary<string, string> questionsAndAnswers, string rawContent) : base(questionsAndAnswers, rawContent)
        {
        }

        protected override void CheckAnswer()
        {
            throw new NotImplementedException();
        }

        protected override void Reset()
        {
            throw new NotImplementedException();
        }

        protected override void RevealAnswer()
        {
            throw new NotImplementedException();
        }

        protected override void SelectAnswer(SelectionType answerType)
        {
            throw new NotImplementedException();
        }
    }
}
