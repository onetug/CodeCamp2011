
namespace CodeCamp.RIA.Data.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // The MetadataTypeAttribute identifies LogMetadata as the class
    // that carries additional metadata for the Log class.
    [MetadataTypeAttribute(typeof(Log.LogMetadata))]
    public partial class Log
    {

        // This class allows you to attach custom attributes to properties
        // of the Log class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class LogMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private LogMetadata()
            {
            }

            public string AppDomainName { get; set; }

            [Include]
            public EntityCollection<CategoryLog> CategoryLogs { get; set; }

            public int EventID { get; set; }

            public string FormattedMessage { get; set; }
            [Key]
            public int LogID { get; set; }

            public string MachineName { get; set; }

            public string Message { get; set; }
            [Required]
            public int Priority { get; set; }

            public string ProcessID { get; set; }

            public string ProcessName { get; set; }

            public string Severity { get; set; }

            public string ThreadName { get; set; }
            [Required]
            public DateTime Timestamp { get; set; }

            public string Title { get; set; }

            public string Win32ThreadId { get; set; }
        }
    }
}
