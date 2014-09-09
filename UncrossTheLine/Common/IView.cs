namespace WpfCommon.ViewBase
{
    using WpfCommon.ModelBase;

    /// <summary>
    /// The View interface.
    /// </summary>
    public interface IView
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        ViewModelBase ViewModel { get; set; }

        #endregion
    }
}
