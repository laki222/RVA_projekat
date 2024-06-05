using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.Class
{
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged = delegate { };

        private Action callback;

        private Func<bool> canExecuteMethod;

        public Command(Action callbackFunc, Func<bool> canExecute = null)
        {
            callback = callbackFunc;
            canExecuteMethod = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecuteMethod != null ? canExecuteMethod() : callback != null;
        }

        public void Execute(object parameter)
        {
            if (callback != null)
                callback();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }


    }


    public class Command<T> : ICommand
    {
        public event EventHandler CanExecuteChanged = delegate { };

        private Action<T> callback;
        private Func<bool> canExecuteMethod;



        public Command(Action<T> callbackFunc, Func<bool> canExecute = null)
        {
            callback = callbackFunc;
            canExecuteMethod = canExecute;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            return canExecuteMethod != null ? canExecuteMethod() : callback != null;
        }

        public void Execute(object parameter)
        {
            if (callback != null)
                callback((T)parameter);
        }
    }


    class RevertableCommand : Command
    {
        private Action revert;



        public RevertableCommand(Action executeFunc, Action revertFunc) : base(executeFunc)
        {
            revert = revertFunc;
        }

        public void Revert()
        {
            if (revert != null)
                revert();
        }
    }

    enum ActionType
    {
        Edit,
        Delete,
        Add
    }

    class UndoRedoAction
    {

    }





}
