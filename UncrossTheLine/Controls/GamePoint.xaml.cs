// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GamePoint.xaml.cs" company="">
//   
// </copyright>
// <summary>
//   The game point.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using NoLineCross.ViewModel;
using WpfCommon.ModelBase;
using WpfCommon.ViewBase;

namespace UncrossTheLine.Controls
{
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// The game point.
    /// </summary>
    public sealed partial class GamePoint : UserControl, IView
    {
        /// <summary>
        /// The point move event handler.
        /// </summary>
        public delegate void PointMoveEventHandler();

        /// <summary>
        /// The on point move.
        /// </summary>
        public event PointMoveEventHandler OnPointMoveCompleted;


        /// <summary>
        /// Initializes a new instance of the <see cref="GamePoint"/> class.
        /// </summary>
        public GamePoint()
        {
            InitializeComponent();
        }

        private void ThumbPoint_OnPointerExited(object sender, PointerRoutedEventArgs e)
        {
            Control render = sender as Control;
            VisualStateManager.GoToState(render, "Normal", true);
        }

        private void ThumbPoint_OnPointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Control render = sender as Control;
            VisualStateManager.GoToState(render, "PointerOn", true);
        }

        private void ThumbPoint_OnShowNeighbor(GamePoint target)
        {
            VisualStateManager.GoToState(target._thumbPoint, "ShowNeighbor", true);
        }

        private void ThumbPoint_OnResetToNormal(GamePoint target)
        {
            VisualStateManager.GoToState(target._thumbPoint, "Normal", true);
        }


        private void ThumbPoint_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            Thumb moveThumb = sender as Thumb;
            GamePoint point = FindParent<GamePoint>(moveThumb);

            if (point != null)
            {
                PointViewModel pv = point.ViewModel as PointViewModel;
                foreach (PointViewModel nei in pv.Neighbors)
                {
                    GamePoint p = nei.View as GamePoint;
                    ThumbPoint_OnShowNeighbor(p);
                }

                Canvas c = FindParent<Canvas>(point);
                double minLeft = 0;
                double maxLeft = c.ActualWidth - (PointViewModel.Radio * 2);
                double minTop = 0;
                double maxTop = c.ActualHeight - (PointViewModel.Radio * 2);


                double left = Canvas.GetLeft(point);
                double top = Canvas.GetTop(point);

                double newLeft = left + e.HorizontalChange;
                double newTop = top + e.VerticalChange;

                if (newLeft > maxLeft)
                {
                    newLeft = maxLeft;
                }

                if (newLeft < minLeft)
                {
                    newLeft = minLeft;
                }

                if (newTop > maxTop)
                {
                    newTop = maxTop;
                }

                if (newTop < minTop)
                {
                    newTop = minTop;
                }

                Canvas.SetLeft(point, newLeft);
                Canvas.SetTop(point, newTop);

                point.OnPointMoving(new Point(newLeft, newTop));
            }
        }

        /// <summary>
        /// The on position changed.
        /// </summary>
        /// <param name="lastPosition">
        /// The last position.
        /// </param>
        public void OnPointMoving(Point lastPosition)
        {
            PointViewModel viewModel = this.ViewModel as PointViewModel;
            viewModel.CurPosition = lastPosition;
            foreach (LineViewModel line in viewModel.Lines)
            {
                line.RefreshSegmentCoordinate();
            }
        }

        public static T FindChildren<T>(DependencyObject child) where T : DependencyObject
        {
            // get parent item
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            // we've reached the end of the tree
            if (parentObject == null)
            {
                return null;
            }

            // check if the parent matches the type we're looking for
            T parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }

            return FindParent<T>(parentObject);
        }

        public static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            // get parent item
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            // we've reached the end of the tree
            if (parentObject == null)
            {
                return null;
            }

            // check if the parent matches the type we're looking for
            T parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }

            return FindParent<T>(parentObject);
        }

        private void ThumbPoint_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            Thumb moveThumb = sender as Thumb;
            GamePoint point = FindParent<GamePoint>(moveThumb);

            if (point != null)
            {
                PointViewModel pv = point.ViewModel as PointViewModel;
                foreach (PointViewModel nei in pv.Neighbors)
                {
                    GamePoint p = nei.View as GamePoint;
                    ThumbPoint_OnResetToNormal(p);
                }

                if (this.OnPointMoveCompleted != null)
                {
                    this.OnPointMoveCompleted();
                }
            }
        }

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
    }
}