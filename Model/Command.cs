using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace YoutubeMusic.Model
{
    class Command : ICommand
    {
        private readonly Action _execute;
        private readonly Predicate<object> _canExecute;

        public Command(Action execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public Command(Action execute) : this(execute, null) { }


        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke();
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

    }
}
