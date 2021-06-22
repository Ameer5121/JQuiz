using JQuiz.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;
using System.IO;
using JQuiz.Events;
using JQuiz.Extensions;

namespace JQuiz.ViewModels
{
    class HomeViewModel : ViewModelBase
    {
        private string _fileName;
        private string _rawContent;
        private string _filePath;
        private bool _fileSelected;
        private Dictionary<string, string> _questionsAndAnswers;
        public event EventHandler<QuizEventArgs> OnQuizStart;
        public string FileName
        {
            get => _fileName;
            set => SetPropertyValue(ref _fileName, value);
        }
        public ICommand Choose => new RelayCommand(ChooseFile);
        public ICommand Start => new RelayCommand(StartQuiz, CanStartQuiz);

        private void ChooseFile()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = "txt";
            fileDialog.Filter = "Text Files (.txt) | *.txt;";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                FileName = Path.GetFileName(fileDialog.FileName);
                _filePath = fileDialog.FileName;
                _fileSelected = true;
            }
        }

        private bool CanStartQuiz()
        {
            return _fileSelected == false ? false : true;
        }
        private void StartQuiz()
        {
            _questionsAndAnswers = GetContent();
            OnQuizStart?.Invoke(this, new QuizEventArgs { QuestionsAndAnswers = this._questionsAndAnswers, RawContent = this._rawContent});
        }

        private Dictionary<string, string> GetContent()
        {
            Dictionary<string, string> questionsAndAnswers = new Dictionary<string, string>();
            using (var stream = new StreamReader(_filePath))
            {
                _rawContent = stream.ReadToEnd();
                var splittedContent = _rawContent.Split(new string[] { "\\", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                splittedContent.RemoveWhiteSpace();
                questionsAndAnswers.AddSplittedContent(splittedContent);
            }
            return questionsAndAnswers;
        }
    }
}
