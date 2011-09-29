using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Caliburn.Micro
{
    /// <summary>
    ///   Defines a Page export that associates a Page with a Uri.
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
    public class ExportPageAttribute : ExportAttribute
    {
        /// <summary>
        ///   Creates an ExportPageAttribute.
        /// </summary>
        public ExportPageAttribute()
            : base(typeof(Page))
        {
        }

        /// <summary>
        ///   Creates an ExportPageAttribute with the given NavigateUri.
        /// </summary>
        /// <param name = "navigateUri"></param>
        public ExportPageAttribute(string navigateUri)
            : this()
        {
            NavigateUri = navigateUri;
        }

        /// <summary>
        ///   Gets or sets the NavigateUri for the ExportPageAttribute.
        /// </summary>
        public string NavigateUri { get; set; }
    }

    /// <summary>
    ///   Defines a Page export that associates a Page with a Uri.
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
    public class ExportViewModelAttribute : ExportAttribute
    {
        /// <summary>
        ///   Creates an ExportPageAttribute.
        /// </summary>
        public ExportViewModelAttribute()
            : base(typeof(Screen))
        {
        }

        /// <summary>
        ///   Creates an ExportPageAttribute with the given NavigateUri.
        /// </summary>
        /// <param name = "viewModelName"></param>
        public ExportViewModelAttribute(string viewModelName)
            : this()
        {
            ViewModelName = viewModelName;
        }

        /// <summary>
        ///   Gets or sets the NavigateUri for the ExportPageAttribute.
        /// </summary>
        public string ViewModelName { get; set; }
    }
}
