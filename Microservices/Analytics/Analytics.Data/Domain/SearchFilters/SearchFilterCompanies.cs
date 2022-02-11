using Analytics.Data.Implemetation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Domain.SearchFilters
{
    public class SearchFilterCompanies : BaseEntity
    {
        /// <summary>
        /// Gets or sets the search filter identifier.
        /// </summary>
        /// <value>
        /// The search filter identifier.
        /// </value>
        public string SearchFilterId { get; set; }
        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>
        /// The company identifier.
        /// </value>
        public string CompanyId { get; set; }
    }
}
