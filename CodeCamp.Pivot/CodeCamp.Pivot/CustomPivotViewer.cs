using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Pivot;
using System.Windows.Shapes;
using Microsoft.Pivot.Internal.Views;

namespace CodeCamp.Pivot
{
    /// <summary>
    /// Override of PivotViewer that uses a delegate for GetCustomActionsForItem
    /// </summary>
    public class CustomPivotViewer : PivotViewer
    {

        // For demo
        private Grid GridContainer { get; set; }
        private Grid GridViews { get; set; }

        public Visibility TileVisibility
        {
            get
            {
                return GridViews.Children[0].Visibility;
            }
            set
            {
                GridViews.Children[0].Visibility = value;
            }
        }

        public Visibility FilterVisibility
        {
            get
            {
                return GridViews.Children[2].Visibility;
            }
            set
            {
                GridViews.Children[2].Visibility = value;
                if (value == Visibility.Collapsed)
                {
                    GridViews.ColumnDefinitions[0].Width = new GridLength(0);
                }
                else
                {
                    GridViews.ColumnDefinitions[0].Width = GridLength.Auto;
                }
            }
        }

        public Visibility InfoVisibility
        {
            get
            {
                if (GridViews.ColumnDefinitions[2].Width.Value == 0)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
            set
            {
                if (value == Visibility.Collapsed)
                {
                    GridViews.ColumnDefinitions[2].Width = new GridLength(0);
                }
                else
                {
                    GridViews.ColumnDefinitions[2].Width = GridLength.Auto;
                }
            }
        }

        public Brush GridBackground
        {
            get { return GridContainer.Background; }
            set
            {
                if (GridContainer!= null)
                    GridContainer.Background = value;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // Locate UI Elements
            Grid partContainer = (Grid)this.GetTemplateChild("PART_Container");
            CollectionViewerView cvv = ((CollectionViewerView)(partContainer).Children[0]);
            Grid container = cvv.Content as Grid;
            Grid viewerGrid = container.Children[1] as Grid;

            this.GridContainer = container;
            this.GridViews = viewerGrid;

            var colorYellow = Color.FromArgb(0xb0, 0xff, 0xe2, 0x39);
            var colorOrangeRed = Color.FromArgb(0xb0, 0xf2, 0x68, 0x25);
            var colorGreen = Color.FromArgb(0xb0, 0x17, 0x88, 0x42);
            var colorBlue = Color.FromArgb(0xb0, 0x38, 0x6e, 0xb6);
            string _bgrd = "";
            Brush background;
            GradientStopCollection gsc1 = new GradientStopCollection {
                        new GradientStop { Color = colorBlue, Offset = 1.0 },
                        new GradientStop { Color = colorGreen, Offset = .82},
                        new GradientStop { Color = colorOrangeRed, Offset = 0.5 },
                        new GradientStop { Color = colorYellow, Offset = 0.0 }
                    };
            background = new RadialGradientBrush(gsc1)
            {
                Center = new Point(1.0, 1.0),
                GradientOrigin = new Point(1.0, 1.0),
                RadiusX = 1.0,
                RadiusY = 1.0
            };
            _bgrd = "RadialGradientBrush";
            GridBackground = background;

        }

        /// <summary>
        /// Callback that this viewer will call to get custom actions for a specific item
        /// </summary>
        public Func<string, List<CustomAction>> GetCustomActionsForItemCallback
        {
            get;
            set;
        }

        /// <summary>
        /// Return an add or remove custom action for the item, depending on whether it is in the cart
        /// </summary>
        protected override List<CustomAction> GetCustomActionsForItem(string itemId)
        {
            List<CustomAction> customActions = null;

            if (GetCustomActionsForItemCallback != null)
            {
                customActions = GetCustomActionsForItemCallback(itemId);
            }

            return customActions;
        }
    }
}
