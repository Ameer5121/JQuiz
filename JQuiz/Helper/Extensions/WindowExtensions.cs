using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JQuiz.Helper.Extensions
{
    public static class WindowExtensions
    {
        public static void ChangeToMainWindow(this Window window)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            window.Close();
        }
    }
}
