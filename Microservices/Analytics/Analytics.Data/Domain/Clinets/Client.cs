using System;
using System.Collections.Generic;
using System.Text;
using Analytics.Data.Domain.SearchFilters;

namespace Analytics.Data.Domain.Clinets
{
    public class Client
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the Role
        /// </summary>
        public int Role { get; set; }

        /// <summary>
        /// Gets or sets the Enabled
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Phone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOn
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the ModifiedOn
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        #region Navigation Properties

        /// <summary>
        /// SearchFilter
        /// </summary>
        public virtual IList<SearchFilter> SearchFilter { get; set; }

        #endregion
    }
}
