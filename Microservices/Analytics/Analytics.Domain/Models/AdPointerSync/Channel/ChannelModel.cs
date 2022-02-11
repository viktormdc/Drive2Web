using System;

namespace Analytics.Domain.Models.AdPointerSync.Channel
{
    public class ChannelModel
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Version
        /// </summary>
        public string version { get; set; }

        /// <summary>
        /// CreatedOn
        /// </summary>
        public DateTime createdOn { get; set; }

        /// <summary>
        /// ModifiedOn
        /// </summary>
        public DateTime modifiedOn { get; set; }

        /// <summary>
        /// ExternalId
        /// </summary>
        public string externalId { get; set; }

        /// <summary>
        /// LogoPath
        /// </summary>
        public string logoPath { get; set; }

    }
}
