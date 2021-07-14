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
    /// Interaction logic for QuizWindow.xaml
    /// </summary>
    public partial class TextQuizWindow : Window
    {
        public TextQuizWindow(Dictionary<string, string> questionsAndAnswers, string rawContent)
        {
            InitializeComponent();         
            DataContext = new TextQuizViewModel(questionsAndAnswers, rawContent);
            Unloaded += OnUnLoaded;
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            (DataContext as TextQuizViewModel).OnAnswerCheck += ResetTextBoxFocus;
        }

        private void OnUnLoaded(object sender, RoutedEventArgs e)
        {
            (DataContext as TextQuizViewModel).OnAnswerCheck -= ResetTextBoxFocus;
            Loaded -= OnLoaded;
            Unloaded -= OnUnLoaded;
        }

        private void ChangeWindow(object sender, RoutedEventArgs e)
        {
            this.ChangeToMainWindow();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == e.LeftButton)
            {
                DragMove();
            }
        }
        private void ResetTextBoxFocus(object sender, EventArgs e)
        {
            InputTextBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up && !InputTextBox.IsFocused)
            {
                Keyboard.Focus(InputTextBox);
            }
        }

        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                ResetTextBoxFocus(this, EventArgs.Empty);
                e.Handled = true;
            }
        }

    }
}
