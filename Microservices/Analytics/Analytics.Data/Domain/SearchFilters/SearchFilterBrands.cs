using Analytics.Data.Implemetation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Domain.SearchFilters
{
    public class SearchFilterBrands : BaseEntity
    {
        /// <summary>
        /// Gets or sets the search filter identifier.
        /// </summary>
        /// <value>
        /// The search filter identifier.
        /// </value>
        public string SearchFilterId { get; set; }
        /// <summary>
        /// Gets or sets the brand identifier.
        /// </summary>
        /// <value>
        /// The brand identifier.
        /// </value>
        public string BrandId { get; set; }
    }
}
