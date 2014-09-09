using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
using NoLineCross.ViewModel;
using UncrossTheLine.Controls;
using WpfCommon.ModelBase;
using WpfCommon.ViewBase;

namespace UncrossTheLine
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, IView
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.ViewModel = new GameViewModel();
            this.ViewModel.View = this;
            
           this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            (this.ViewModel as GameViewModel).GenerateGame(4);
            this.SyncViewModelToUI();
        }

        /// <summary>
        ///     Gets or sets the view model.
        /// </summary>
        public ViewModelBase ViewModel
        {
            get
            {
                return this.DataContext as ViewModelBase;
            }

            set
            {
                this.DataContext = value;
            }
        }

        public Canvas GameCanvas
        {
            get { return this._gameCanvas; }
        }

        public void JumpToNextLevel()
        {
            GameViewModel gameViewModel = this.ViewModel as GameViewModel;

            int level = gameViewModel.Level + 1;
            gameViewModel.GenerateGame(level);

            this.SyncViewModelToUI();
        }

        private void SyncViewModelToUI()
        {
            this._gameCanvas.Children.Clear();
            GameViewModel gameViewModel = this.ViewModel as GameViewModel;

            if (gameViewModel != null)
            {
                foreach (PointViewModel pointViewModel in gameViewModel.GamePoints)
                {
                    GamePoint point = new GamePoint();
                    point.OnPointMoveCompleted += this.WinStateCheck;

                    point.ViewModel = pointViewModel;
                    pointViewModel.View = point;

                    this._gameCanvas.Children.Add(point);
                    Canvas.SetLeft(point, pointViewModel.CurPosition.X);
                    Canvas.SetTop(point, pointViewModel.CurPosition.Y);
                    Canvas.SetZIndex(point, 10);
                }

                foreach (LineViewModel lineViewModel in gameViewModel.GameLines)
                {
                    this._gameCanvas.Children.Add(lineViewModel.Segment);
                }
            }
        }

        private void WinStateCheck()
        {
            GameViewModel gameViewModel = this.ViewModel as GameViewModel;

            if (gameViewModel.WinStateCheck())
            {
                var messageDialog = new MessageDialog("Win! Ready for next level?");

                messageDialog.Commands.Add(new UICommand(
                    "Next Level",
                    this.NextLevel));

                messageDialog.Commands.Add(new UICommand(
                       "Close"));

                // Set the command that will be invoked by default
                messageDialog.DefaultCommandIndex = 0;

                // Set the command to be invoked when escape is pressed
                messageDialog.CancelCommandIndex = 1;

                // Show the message dialog
                messageDialog.ShowAsync();
            }
        }

        private void NextLevel(IUICommand command)
        {
            this.JumpToNextLevel();
        }

        private void JumpToLevelButton_Click(object sender, RoutedEventArgs e)
        {
            int level = int.Parse(this.LevelPickerTextBox.Text);

            GameViewModel gameViewModel = this.ViewModel as GameViewModel;

            gameViewModel.GenerateGame(level);

            this.SyncViewModelToUI();
        }
    }
}
