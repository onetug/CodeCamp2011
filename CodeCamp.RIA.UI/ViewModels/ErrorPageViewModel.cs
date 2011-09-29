namespace CodeCamp.RIA.UI.ViewModels
{
    using System;
    using Caliburn.Micro;

    [ExportViewModel("ErrorPage")]
    public class ErrorPageViewModel : Screen, IErrorPageViewModel, IModule
    {
        Uri _requestedPage;

        public Uri RequestedPage
        {
            get { return _requestedPage; }
            set
            {
                _requestedPage = value;
                this.RequestedPageString = value.OriginalString;
                NotifyOfPropertyChange(() => RequestedPage);
            }
        }

        public string RequestedPageString { get; set; }


        #region IErrorPageViewModel Members

        private string _errorMessage;

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                this._errorMessage = value;
                NotifyOfPropertyChange(() => ErrorMessage);
            }
        }


        private bool isBusy;
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    NotifyOfPropertyChange(() => IsBusy);

                }
            }
        }
        #endregion
    }
}
