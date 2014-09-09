// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NextOrJumpLevel.xaml.cs" company="">
//   
// </copyright>
// <summary>
//   The next or jump level.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

using Windows.UI.Xaml.Input;

namespace UncrossTheLine
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// The next or jump level.
    /// </summary>
    public sealed partial class NextOrJumpLevel : UserControl
    {
        private bool _isPopupContainerTapped;

        public int CurrentLevel { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NextOrJumpLevel"/> class.
        /// </summary>
        public NextOrJumpLevel()
        {
            InitializeComponent();
            this.Height = Window.Current.Bounds.Height;
            this.Width = Window.Current.Bounds.Width;

            PopupContainer.Width = this.Width;
            PopupContainer.Height = this.Height / 5;
        }

        /// <summary>
        /// The show.
        /// </summary>
        public void Show()
        {
            if (Popup.IsOpen == false)
            {
                Popup.IsOpen = true;
            }
        }

        public void Close()
        {
            if (Popup.IsOpen == true)
            {
                Popup.IsOpen = false;
            }
        }

        private void PopupContainer_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void OutsideContainer_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            Popup.IsOpen = false;
        }

        private void NextLevel_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            CurrentLevel++;
        }
    }
}