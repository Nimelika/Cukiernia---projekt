using System;
using System.Windows.Input;

namespace DesktopApp.Helpers
{
    public class BaseCommand : ICommand
    {
        private readonly Action _command;
        private readonly Func<bool> _canExecute;

        public BaseCommand(Action command, Func<bool> canExecute = null)
        {
            _command = command ?? throw new ArgumentNullException(nameof(command));
            _canExecute = canExecute;
        }

        public bool IsAllowed => _canExecute == null || _canExecute();

        public void Execute(object parameter)
        {
            _command();
        }

        public bool CanExecute(object parameter)
        {
            return IsAllowed;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
