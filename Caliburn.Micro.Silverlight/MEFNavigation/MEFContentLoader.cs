using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics.Contracts;

namespace Caliburn.Micro
{
    /// <summary>
    /// MEF Navigation Content Loader
    /// </summary>
    public class MEFContentLoader : ContentLoaderBase
    {

        private static IConductor _theConductor;

        /// <summary>
        /// The Conductor of Screens
        /// </summary>
        public static IConductor TheConductor
        {
            get
            {
                if (_theConductor == null)
                {
                    _theConductor = new Conductor<Screen>.Collection.OneActive();
                    var activator = _theConductor as IActivate;
                    if (activator != null)
                        activator.Activate();
                }

                return _theConductor;
            }
        }

        private static Dictionary<Uri, string> _uriMap = new Dictionary<Uri, string>();
        private static List<string> _loadedXaps = new List<string>();
        private static AggregateCatalog _catalog = new AggregateCatalog();

        /// <summary>
        /// CreateLoader Base
        /// </summary>
        /// <returns></returns>
        protected override LoaderBase CreateLoader()
        {
            return new Loader(this);
        }


        // 
        /// <summary>
        /// XapProperty as a backing store for the Container
        /// </summary>
        /// <remarks>
        /// Using a DependencyProperty as the backing store for Container.  This enables animation, styling, binding, etc...
        /// </remarks>
        public static readonly DependencyProperty XapProperty =
            DependencyProperty.RegisterAttached("Xap", typeof(string), typeof(HyperlinkButton), new PropertyMetadata(null, XapPropertyChangedCallback));

        /// <summary>
        /// Get the Xap Dependency Property
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetXap(DependencyObject obj)
        {
            return (string)obj.GetValue(XapProperty);
        }

        /// <summary>
        /// Set the Xap Dependency Property
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetXap(DependencyObject obj, string value)
        {
            obj.SetValue(XapProperty, value);
        }

        /// <summary>
        /// Callback for the Dependency Property Changed event
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        public static void XapPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var button = (HyperlinkButton)d;
            _uriMap[button.NavigateUri] = (string)e.NewValue;
        }

        /// <summary>
        /// Pages in the catalog
        /// </summary>
        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<Lazy<Page, IExportPageMetadata>> Pages { get; set; }

        /// <summary>
        /// ViewModels in the catalog
        /// </summary>
        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<Lazy<Screen, IExportViewModelMetadata>> ViewModels { get; set; }


        private bool _initialized;
        private void Initialize()
        {

            try
            {
                CompositionHost.Initialize(new DeploymentCatalog(), _catalog);
            }
            catch (InvalidOperationException ex)
            {
                //Will throw an exception if it is already initialized by the bootstrapper.
                var msg = ex.Message;
            }
            CompositionInitializer.SatisfyImports(this);
            this._initialized = true;
        }

        private class Loader : LoaderBase
        {
            private MEFContentLoader parent;
            public Loader(MEFContentLoader parent)
            {
                this.parent = parent;
            }

            public override void Load(Uri targetUri, Uri currentUri)
            {
                try
                {
                    if (!parent._initialized)
                        parent.Initialize();
                   
                    var saveTargetUri = targetUri;
                    if (!targetUri.IsAbsoluteUri)
                        targetUri = new Uri("MEF:///" + targetUri.OriginalString);

                    string xap = null;

                    if (_uriMap.TryGetValue(saveTargetUri, out xap) && xap != null && !_loadedXaps.Contains(xap))
                    {
                        var dc = new DeploymentCatalog(xap);
                        dc.DownloadCompleted += (s, e) =>
                        {
                            if (e.Error != null)
                            {
                                this.Error(e.Error);
                                return;
                            }
                            _catalog.Catalogs.Add(dc);
                            _loadedXaps.Add(xap);
                            NavigateToPage(targetUri);
                        };
                        dc.DownloadAsync();
                    }
                    else
                        NavigateToPage(targetUri);
                }
                catch (Exception e)
                {
                    Error(e);
                }
            }
            
            private void NavigateToPage(Uri targetUri)
            {

                string errorMessage = string.Empty;
                bool hasAuthenticationError = false;

                var viewModel = GetViewModel(targetUri);

                if (viewModel != null)
                {
                    if (viewModel is IRequiresAuthentication)
                    {
                        var vm = viewModel as IRequiresAuthentication;
                        if (vm.RequiresAuthentication)
                        {
                            if (vm.IsAuthenticated)
                            {

                            }
                            else
                            {
                                //Is Not Authenticated.
                                viewModel = null;
                                hasAuthenticationError = true;
                                errorMessage = "Please log in.";
                            }
                        }
                    }
                }
                
                Page page = null;

                if(!hasAuthenticationError)
                    page = GetPage(targetUri);
                
                if (viewModel != null && page != null)
                {
                    BindViewModelAndPage(viewModel, page);
                }
                else
                {
                    var errorPage = GetErrorPage();
                    var errorViewModel = GetErrorViewModel();
                    if (errorViewModel != null && errorPage != null)
                    {
                        IErrorPageViewModel errorPageViewModel = errorViewModel as IErrorPageViewModel;
                        if (errorPageViewModel != null)
                        {
                            errorPageViewModel.RequestedPage = targetUri;
                            if (hasAuthenticationError)
                            {

                            }
                            else
                            {
                                if (viewModel == null)
                                {
                                    errorMessage += " The ViewModel was not found.";
                                }

                                if (page == null)
                                {
                                    errorMessage += " The page could not be created.";
                                }
                            }
                            errorPageViewModel.ErrorMessage = errorMessage;
                        }
                        BindViewModelAndPage(errorViewModel, errorPage);
                        Complete(errorPage);
                        return;
                    }
                }
                
                Complete(page);
            }

            private Screen GetErrorViewModel()
            {
                var vm = GetViewModel(ErrorPageViewModel);
                if (vm == null)
                {
                    throw new Exception("The Errorpage ViewModel was not found.");
                }
                return vm;
            }

            public const string ErrorPage = "/ErrorPage";
            public const string ErrorPageViewModel = "ErrorPage";

            private Page GetErrorPage()
            {
                try
                {
                    var pageContext = parent.Pages.Single((page) => page.Metadata.NavigateUri == ErrorPage);
                    if (pageContext != null)
                    {
                        return pageContext.Value;
                    }
                }
                catch (System.InvalidOperationException ex)
                {
                    //No Error page was found.
                    throw new Exception("Error Page was not found.", ex);
                }
                return null;
            }

            private void BindViewModelAndPage(Screen screen, Page page)
            {
                ViewModelBinder.Bind(screen, page, screen);
                
                

                if (TheConductor != null)
                {
                    TheConductor.ActivateItem(screen);
                    //IActivate activator = screen as IActivate;
                    //if (activator != null)
                    //{
                    //    activator.Activate();
                    //}
                }
                
                
            }

            private Page GetPage(Uri targetUri)
            {
                try {

                    var errorPageContext = parent.Pages.FirstOrDefault((aScreen) => CompareUri( aScreen, targetUri));
                    if(errorPageContext!=null)
                        return errorPageContext.Value;
                }
                catch (System.InvalidOperationException) {
                    //Not Found
                }
                return null;
            }

            private Screen GetViewModel(Uri targetVM)
            {
                return GetViewModel(GetStringFromUri(targetVM.AbsolutePath));
            }

            private Screen GetViewModel(string targetVM)
            {

                //string targetVM = GetStringFromUri(targetUri.AbsolutePath);
                try
                {

                    var pageContext = parent.ViewModels.FirstOrDefault((aScreen) =>  aScreen.Metadata.ViewModelName == targetVM);

                    if (pageContext != null)
                    {
                        return pageContext.Value;
                    }
                }
                catch (System.InvalidOperationException )
                {
                    //Not found;
                }
                return null;
            }

            private string GetStringFromUri(string uriString)
            {
                Contract.Requires(uriString != null);
                return new string(uriString.Where(c => Char.IsLetterOrDigit(c)).ToArray());
            }

            private bool CompareViewModel(Lazy<Screen, IExportViewModelMetadata> factory, string targetVM)
            {
                string name = factory.Metadata.ViewModelName;
                return targetVM.Equals(name);
            }

            private bool CompareUri(Lazy<Page, IExportPageMetadata> factory, Uri targetUri)
            {
                Contract.Requires(((Caliburn.Micro.IExportPageMetadata)factory.Metadata).NavigateUri!=null);
                Uri uri = GetTrimmedUri(factory.Metadata.NavigateUri);
                if (!uri.IsAbsoluteUri)
                    uri = new Uri("MEF:///" + uri.OriginalString);
                return targetUri.Equals(uri);
            }

            private static Uri GetTrimmedUri(string uriString)
            {
                Contract.Requires(uriString != null);
                if (uriString.Contains('?'))
                    uriString = uriString.Substring(0, uriString.IndexOf('?') + 1);
                Uri result;
                try
                {
                    result = new Uri(uriString, UriKind.RelativeOrAbsolute);
                }
                catch
                {
                    result = new Uri(uriString, UriKind.Relative);
                }
                return result;
            }

            public override void Cancel()
            {
            }
        }


        

        //#region INotifyPropertyChanged Members

        //public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        //#endregion
    }
}
