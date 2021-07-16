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
using JQuiz.Helper;

namespace JQuiz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HomeViewModel _hvm;
        public MainWindow()
        {
            InitializeComponent();       
            DataContext = new HomeViewModel();
            _hvm = DataContext as HomeViewModel;
            Unloaded += OnUnLoaded;
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _hvm.OnQuizStart += ChangeWindow;
        }

        private void OnUnLoaded(object sender, RoutedEventArgs e)
        {
            _hvm.OnQuizStart -= ChangeWindow;
            Loaded -= OnLoaded;
            Unloaded -= OnUnLoaded;
        }

        private void ChangeWindow(object sender, QuizEventArgs e)
        {
            e.CurrentQuizMode.StartQuiz(e.QuestionsAndAnswers, e.RawContent);
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ButtonState == e.LeftButton)
            {
                DragMove();
            }
        }
        private void TextMode_Click(object sender, RoutedEventArgs e)
        {
            _hvm.CurrentQuizMode = new TextQuizMode();
        }
        private void SelectionMode_Click(object sender, RoutedEventArgs e)
        {
            _hvm.CurrentQuizMode = new SelectionQuizMode();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
