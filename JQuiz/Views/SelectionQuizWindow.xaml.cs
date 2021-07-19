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
        private List<Button> _selectableButtons;
        private Button _currentSelectedButton;
        public SelectionQuizWindow(Dictionary<string, string> questionsAndAnswers, string rawContent)
        {
            InitializeComponent();
            DataContext = new SelectionQuizViewModel(questionsAndAnswers, rawContent);
            _selectableButtons = new List<Button>();
            Loaded += OnLoaded;
            Unloaded -= OnUnLoaded;
        }
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            (DataContext as SelectionQuizViewModel).Reset += ResetBorderBrush;
        }
        private void OnUnLoaded(object sender, RoutedEventArgs e)
        {
            (DataContext as SelectionQuizViewModel).Reset -= ResetBorderBrush;
            Loaded -= OnLoaded;
            Unloaded -= OnUnLoaded;
        }

        private void Button_Loaded(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            _selectableButtons.Add(button);
        }

        private void ChangeButtonBorder(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == _currentSelectedButton) return;
            else if (_currentSelectedButton != null && button != _currentSelectedButton)
            {
                _currentSelectedButton.BorderBrush = new SolidColorBrush(Colors.Gray);
            }
            _currentSelectedButton = button;
            _currentSelectedButton.BorderBrush = new SolidColorBrush(Colors.ForestGreen);

        }
        private void ResetBorderBrush(object sender, EventArgs e)
        {
            _currentSelectedButton.BorderBrush = new SolidColorBrush(Colors.Gray);
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
