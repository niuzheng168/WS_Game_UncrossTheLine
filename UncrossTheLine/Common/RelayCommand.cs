namespace WpfCommon.ModelBase
{
    using System;
    using System.Diagnostics;
    using System.Windows.Input;

    /// <summary>
    /// Relay commands from view to model
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Fields

        /// <summary>
        /// predicate delegate
        /// </summary>
        private readonly Predicate<object> canExecute;

        /// <summary>
        /// action delegate
        /// </summary>
        private readonly Action<object> execute;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class. 
        /// Initializes new instance of the RelayCommand class
        /// </summary>
        /// <param name="execute">
        /// execute action
        /// </param>
        public RelayCommand(Action<object> execute)
            : this(null, execute)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class. 
        /// Initializes new instance of the RelayCommand class
        /// </summary>
        /// <param name="canExecutePredicate">
        /// Can execute predicate
        /// </param>
        /// <param name="executeAction">
        /// execute action
        /// </param>
        public RelayCommand(Predicate<object> canExecutePredicate, Action<object> executeAction)
        {
            if (executeAction == null)
            {
                throw new ArgumentNullException("execute");
            }

            this.execute = executeAction;
            this.canExecute = canExecutePredicate;
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Can execute handler
        /// </summary>
        public event EventHandler CanExecuteChanged;

        #endregion

        #region Public Methods and Operators

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Can execute 
        /// </summary>
        /// <param name="parameter">
        /// parameter object
        /// </param>
        /// <returns>
        /// true or false
        /// </returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null ? true : this.canExecute(parameter);
        }

        /// <summary>
        /// execute action
        /// </summary>
        /// <param name="parameter">
        /// parameter object
        /// </param>
        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

        #endregion
    }
}
