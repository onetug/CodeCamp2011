namespace Caliburn.Micro
{
    /// <summary>
    ///   Defines Page Export metadata.
    /// </summary>
    public interface IExportPageMetadata
    {
        /// <summary>
        ///   The Uri for the page.
        /// </summary>
        string NavigateUri { get; }
    }

    /// <summary>
    ///   Defines Page Export metadata.
    /// </summary>
    public interface IExportViewModelMetadata
    {
        /// <summary>
        ///   The ViewModelName.
        /// </summary>
        string ViewModelName { get; }

        ///// <summary>
        /////   The Uri for the page.
        ///// </summary>
        //string NavigateUri { get; }

    }
}
