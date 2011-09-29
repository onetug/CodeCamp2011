using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Pivot;
using System.Windows.Shapes;

namespace CodeCamp.Pivot
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            Loaded += Control_Loaded;
        }
        private void Control_Loaded(object sender, RoutedEventArgs e)
        {

            // Setup custom strings to replace some of PivotViewer's default strings (comment this line to use all defaults)
            //System.Windows.Pivot.PivotViewer.SetResourceManager(SampleLocalizedStrings.ResourceManager);

            // Load the initial collection
            var appLaunchPoint = App.Current.Host.Source.OriginalString;
            var appStart = appLaunchPoint.ToUpper().IndexOf("CLIENTBIN");
            var appHostUri = appLaunchPoint.Substring(0, appStart);
            string initialCollectionUri = new Uri(appHostUri + "Sessions.cxml").ToString();
            try
            {
                SessionsPivot.CollectionLoadingFailed +=
                    new EventHandler<CollectionErrorEventArgs>(SessionsPivot_CollectionLoadingFailed);
                SessionsPivot.LoadCollection(initialCollectionUri, string.Empty);
                // Enable the travel log (comment this line to disable the travel log)
                TravelLog.Create(SessionsPivot, new Uri(initialCollectionUri));
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = ex.Message;
                ErrorMessage.Visibility = Visibility.Visible;
            }
        }


        #region Error handling

        /// <summary>
        /// Basic error reporting when the collection fails to load
        /// </summary>
        private void SessionsPivot_CollectionLoadingFailed(object sender, CollectionErrorEventArgs args)
        {
            string errorMessage = args.ErrorCode.ToString();
            string details = string.Format("Error parsing {0}", SessionsPivot.CollectionUri);
            ErrorMessage.Text = errorMessage + Environment.NewLine + details;
            ErrorMessage.Visibility = Visibility.Visible;

        }

        #endregion

        #region Link handling (metadata link clicked, item double-clicked)

        /// <summary>
        /// Handle links clicked in the metadata pane
        /// </summary>
        private void SessionsPivot_LinkClicked(object sender, LinkEventArgs args)
        {
            OpenLink(args.Link.ToString());
        }

        /// <summary>
        /// Handle double-clicks on collection items
        /// </summary>
        private void SessionsPivot_ItemDoubleClicked(object sender, ItemEventArgs args)
        {
            string linkUriString = SessionsPivot.GetItem(args.ItemId).Href;
            if (!string.IsNullOrWhiteSpace(linkUriString))
            {
                SessionsPivot.CurrentItemId = args.ItemId;
                OpenLink(linkUriString);
            }
            //else
            //{
            //    ErrorWindow errorWin = new ErrorWindow("No Associated Web Page", "The item that was double-clicked has no value for the 'Href' field");
            //    errorWin.Show();
            //}
        }

        /// <summary>
        /// Open a URI to either a collection or a web page
        /// </summary>
        private void OpenLink(string linkUri)
        {
            if (linkUri.Contains(".cxml"))
            {
                // Link points to a collection file, open it in place
                LoadCollection(linkUri);
            }
            //else
            //{
            //    // Link points to a normal web page. Open it in a new tab.
            //    HtmlPage.Window.Navigate(new Uri(linkUri, UriKind.RelativeOrAbsolute), "PimpThePivotViewerTargetFrame");
            //}
        }

        /// <summary>
        /// Helper to load a collection from a Uri that may contain a viewerState fragment
        /// </summary>
        private void LoadCollection(string collectionUri)
        {
            string baseCollectionUri;
            string viewerState;
            BreakupCollectionUri(collectionUri, out baseCollectionUri, out viewerState);
            SessionsPivot.LoadCollection(baseCollectionUri, viewerState);
        }

        /// <summary>
        /// Break a collection Uri into the base Uri and the viewerState fragment
        /// </summary>
        private void BreakupCollectionUri(string collectionUri, out string baseCollectionUri, out string viewerState)
        {
            string linkUriString = collectionUri;
            string[] linkParts = linkUriString.Split(new char[] { '#' });
            baseCollectionUri = linkParts[0];
            viewerState = (linkParts.Length > 1) ? linkParts[1] : string.Empty;
        }

        #endregion

        protected object GetPropertyValue(string propertyName, object entity)
        {
            string[] propertyNames = propertyName.Split('.');

            if (propertyNames.Length == 0 || entity == null)
            {
                return null;
            }

            string firstPropertyName = propertyNames[0];

            PropertyInfo[] properties = entity.GetType().GetProperties();

            // Loop thru to find the one we want
            foreach (PropertyInfo pi in properties)
            {
                if (pi.Name == firstPropertyName)
                {
                    //this is to prevent comparisons against lists of objects, but allows comparison against properties of those lists
                    //if (pi.PropertyType.GetInterface("IEnumerable") != null && propertyNames.Length == 1)
                    if (propertyNames.Length == 1)
                    {
                        //this is needed because strings are IEnumerable and without this we can't compare string to string easily
                        if (typeof(string) == pi.PropertyType)
                        {
                            return pi.GetValue(entity, null);
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else if (propertyNames.Length > 1)
                    {
                        return GetPropertyValue(propertyNames.ToString(), pi.GetValue(entity, null));
                    }
                    else
                    {
                        return pi.GetValue(entity, null);
                    }
                }
            }

            return null;
        }


    }
}
