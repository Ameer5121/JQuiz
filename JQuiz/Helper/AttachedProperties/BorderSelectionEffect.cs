using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace JQuiz.Helper.AttachedProperties
{
    class BorderSelectionEffect
    {
        private static Border _currentBorder;

        public static bool GetSelectionEffect(DependencyObject obj)
        {
            return (bool)obj.GetValue(SelectionEffect);
        }

        public static void SetSelectionEffect(DependencyObject obj, bool value)
        {
            obj.SetValue(SelectionEffect, value);
        }

        public static readonly DependencyProperty SelectionEffect =
            DependencyProperty.RegisterAttached("SelectionEffect", typeof(bool), typeof(BorderSelectionEffect), new PropertyMetadata(false, OnLoaded));

        private static void OnLoaded(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Border border = d as Border;
            border.MouseDown += Border_MouseDown;
        }

        private static void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Border border = sender as Border;
            if (border == _currentBorder)
            {
                return;
            }
            else if (_currentBorder != null)
            {
                _currentBorder.BorderBrush = new SolidColorBrush(Colors.Gray);
            }
            _currentBorder = border;
            _currentBorder.BorderBrush = new SolidColorBrush(Colors.ForestGreen);
        }
    }
}
