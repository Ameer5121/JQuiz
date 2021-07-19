using JQuiz.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JQuiz.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Action<SelectionType> execute;
        private readonly Action execute2;
        private readonly Action<string> execute3;

        private readonly Func<bool> canExecute;
        public RelayCommand(Action<SelectionType> execute) : this(execute, canExecute: null)
        {
        }
        public RelayCommand(Action execute) : this(execute, canExecute: null)
        {
        }
        public RelayCommand(Action<string> execute) : this(execute, canExecute: null)
        {
        }

        public RelayCommand(Action<SelectionType> execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            this.execute = execute;
            this.canExecute = canExecute;
        }
        public RelayCommand(Action execute2, Func<bool> canExecute)
        {
            if (execute2 == null)
                throw new ArgumentNullException("execute2");

            this.execute2 = execute2;
            this.canExecute = canExecute;
        }

        public RelayCommand(Action<string> execute3, Func<bool> canExecute)
        {
            if (execute3 == null)
                throw new ArgumentNullException("execute3");

            this.execute3 = execute3;
            this.canExecute = canExecute;
        }


        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }



        public bool CanExecute(object parameter)
        {
            return this.canExecute == null ? true : this.canExecute();
        }

        public void Execute(object parameter)
        {
            if (execute != null)
            {
                this.execute((SelectionType)int.Parse((string)parameter));
            }
            else if(execute2 != null) execute2();
            else if (execute3 != null)
            {
                this.execute3(parameter as string);
            }
        }
    }
}
