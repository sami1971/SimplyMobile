using System;
using System.Windows.Input;

namespace SimplyMobile.Core
{
    public class CommandDelegate : ICommand
    {
        private readonly Predicate<object> canExecute;
        private readonly Action<object> execute;

        public CommandDelegate (Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        #region ICommand implementation
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return this.canExecute (parameter);
        }

        public void Execute(object parameter)
        {
            this.execute (parameter);
        }
        #endregion

        public void RaiseCanExecuteChanged()
        {
            if(CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}

