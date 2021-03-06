using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InstrumentPriceMonitor.Util
{
	public class RelayCommand<T> : ICommand
	{
		private readonly Action<T> _execute;
		private readonly Predicate<T> _canExecute;


		public RelayCommand(Action<T> execute) : this(execute, null) { }

		public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
		{
			_execute = execute ?? throw new ArgumentNullException(nameof(execute));
			_canExecute = canExecute;
		}

		public RelayCommand(Action execute, Func<bool> canExecute = null)
		{
			if (execute == null)
			{
				throw new ArgumentNullException(nameof(execute));
			}
			_execute = _ => execute.Invoke();
			_canExecute = _ => canExecute?.Invoke() ?? true;
		}

		public bool CanExecute(object parameter)
		{
			return _canExecute?.Invoke((T)parameter) ?? true;
		}


		public event EventHandler CanExecuteChanged;

		public void Execute(object parameter)
		{
			_execute((T)parameter);
		}

		public void RaiseCanExecuteChanged()
		{
			var ehandler = CanExecuteChanged;
			ehandler?.Invoke(this, EventArgs.Empty);
		}

	}

	public class RelayCommand : RelayCommand<object>
	{
		public RelayCommand(Action<object> action, Predicate<object> canExecute = null) : base(action, canExecute) { }
		public RelayCommand(Action action, Func<bool> canExecute = null) : base(action, canExecute) { }

	}
}
