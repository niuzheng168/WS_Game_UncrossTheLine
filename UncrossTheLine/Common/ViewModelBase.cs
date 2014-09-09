namespace WpfCommon.ModelBase
{
    using WpfCommon.ViewBase;

    /// <summary>
    ///     The view model base.
    /// </summary>
    public abstract class ViewModelBase : ModelBase
    {
        #region Fields

        /// <summary>
        ///     The _view.
        /// </summary>
        private IView _view;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the view.
        /// </summary>
        public IView View
        {
            get
            {
                return this._view;
            }

            set
            {
                this._view = value;
            }
        }

        #endregion

        /// <summary>
        /// The set view.
        /// </summary>
        /// <param name="view">
        /// The view.
        /// </param>
        public void SetView(IView view)
        {
            this._view = view;
        }
    }
}