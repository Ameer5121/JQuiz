using JQuiz.Events;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using JQuiz.Views;

namespace JQuiz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new HomeViewModel();
            Unloaded += OnUnLoaded;
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            (DataContext as HomeViewModel).OnQuizStart += ChangeWindow;
        }

        private void OnUnLoaded(object sender, RoutedEventArgs e)
        {
            (DataContext as HomeViewModel).OnQuizStart -= ChangeWindow;
            Loaded -= OnLoaded;
            Unloaded -= OnUnLoaded;
        }

        private void ChangeWindow(object sender, QuizEventArgs e)
        {
            QuizWindow quizWindow = new QuizWindow(e.QuestionsAndAnswers, e.RawContent);
            quizWindow.Show();
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ButtonState == e.LeftButton)
            {
                DragMove();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
