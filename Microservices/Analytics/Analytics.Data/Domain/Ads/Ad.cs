using System;
using System.Collections.Generic;
using System.Text;
using Analytics.Data.Domain.AdEvents;
using Analytics.Data.Domain.SearchFilters;

namespace Analytics.Data.Domain.Ads
{
    public class Ad
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is defined.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is defined; otherwise, <c>false</c>.
        /// </value>
        public bool IsDefined { get; set; }
        /// <summary>
        /// Gets or sets the brand identifier.
        /// </summary>
        /// <value>
        /// The brand identifier.
        /// </value>
        public string BrandId { get; set; }
        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>
        /// The company identifier.
        /// </value>
        public string CompanyId { get; set; }
        /// <summary>
        /// Gets or sets the industry identifier.
        /// </summary>
        /// <value>
        /// The industry identifier.
        /// </value>
        public string IndustryId { get; set; }
        /// <summary>
        /// Gets or sets the external identifier.
        /// </summary>
        /// <value>
        /// The external identifier.
        /// </value>
        public string ExternalId { get; set; }
        /// <summary>
        /// Gets or sets the video u rl.
        /// </summary>
        /// <value>
        /// The video u rl.
        /// </value>
        public string VideoURl { get; set; }
        /// <summary>
        /// Gets or sets the video img URL.
        /// </summary>
        /// <value>
        /// The video img URL.
        /// </value>
        public string VideoImgUrl { get; set; }

        #region Navigation Properties

        /// <summary>
        /// AdEvents
        /// </summary>
        public virtual IList<AdEvent> AdEvents { get; set; }

        /// <summary>
        /// AdEvents
        /// </summary>
        public virtual IList<SearchFilterAds> SearchFilterAds { get; set; }
        #endregion
    }
}
