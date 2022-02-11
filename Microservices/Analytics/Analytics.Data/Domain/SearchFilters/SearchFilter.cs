using System;
using System.Collections.Generic;
using System.Text;
using Analytics.Data.Domain.Ads;
using Analytics.Data.Domain.Clinets;

namespace Analytics.Data.Domain.SearchFilters
{
    public class SearchFilter
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public int Version { get; set;}
        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        /// <value>
        /// The created on.
        /// </value>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// Gets or sets the modified on.
        /// </summary>
        /// <value>
        /// The modified on.
        /// </value>
        public DateTime ModifiedOn { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; }

        #region Navigation Properties

        /// <summary>
        /// AdEvents
        /// </summary>
        public virtual IList<SearchFilterAds> SearchFilterAds { get; set; }

        /// <summary>
        /// Client
        /// </summary>
        public virtual Client Client { get; set; }

        #endregion

    }
}

