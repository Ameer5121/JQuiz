using JQuiz.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using JQuiz.Helper.Extensions;

namespace JQuiz.Views
{
    /// <summary>
    /// Interaction logic for SelectionQuizWindow.xaml
    /// </summary>
    public partial class SelectionQuizWindow : Window
    {
        public SelectionQuizWindow(Dictionary<string, string> questionsAndAnswers, string rawContent)
        {
            InitializeComponent();
            DataContext = new SelectionQuizViewModel(questionsAndAnswers, rawContent);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == e.LeftButton)
            {
                DragMove();
            }
        }

        private void ChangeWindow(object sender, RoutedEventArgs e)
        {
            this.ChangeToMainWindow();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
