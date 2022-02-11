using Analytics.Data.Implemetation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Domain.SearchFilters
{
    public class SearchFilterIndustrys : BaseEntity
    {
        /// <summary>
        /// Gets or sets the search filter identifier.
        /// </summary>
        /// <value>
        /// The search filter identifier.
        /// </value>
        public string SearchFilterId { get; set; }
        /// <summary>
        /// Gets or sets the industry identifier.
        /// </summary>
        /// <value>
        /// The industry identifier.
        /// </value>
        public string IndustryId { get; set; }
    }
}
