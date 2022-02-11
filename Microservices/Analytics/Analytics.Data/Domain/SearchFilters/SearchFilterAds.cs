using Analytics.Data.Implemetation;
using System;
using System.Collections.Generic;
using System.Text;
using Analytics.Data.Domain.Ads;

namespace Analytics.Data.Domain.SearchFilters
{
    public class SearchFilterAds : BaseEntity
    {
        /// <summary>
        /// Gets or sets the search filter identifier.
        /// </summary>
        /// <value>
        /// The search filter identifier.
        /// </value>
        public string SearchFilterId { get; set; }
        /// <summary>
        /// Gets or sets the ad identifier.
        /// </summary>
        /// <value>
        /// The ads identifier.
        /// </value>
        public string AdId { get; set; }

        #region NavigationProperties

        /// <summary>
        /// SocialNetwork
        /// </summary>
        public virtual Ad Ad { get; set; }

        /// <summary>
        /// SearchFilter
        /// </summary>
        public virtual SearchFilter SearchFilter { get; set; }
        #endregion
    }
}
