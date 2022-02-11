using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Domain.Channels
{
    public class Channel
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Version
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// CreatedOn
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// ModifiedOn
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// ExternalId
        /// </summary>
        public string ExternalId { get; set; }

        /// <summary>
        /// LogoPath
        /// </summary>
        public string LogoPath { get; set; }
    }
}
