using System;

namespace Caliburn.Micro
{
    /// <summary>
    /// Interface for ErrorPage
    /// </summary>
    public interface IErrorPageViewModel
    {
        /// <summary>
        /// Uri of the requested page
        /// </summary>
        Uri RequestedPage { get; set; }
        /// <summary>
        /// Error Message to be displayed
        /// </summary>
        string ErrorMessage { get; set; }
    }
}
