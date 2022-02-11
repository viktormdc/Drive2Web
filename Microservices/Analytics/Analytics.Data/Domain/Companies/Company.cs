using Analytics.Data.Domain.Industries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Domain.Companies
{
    public class Company
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
        /// IndustryId
        /// </summary>
        public string IndustryId { get; set; }

        //#region NavigationProperties

        ///// <summary>
        ///// Industry
        ///// </summary>
        //public virtual Industry Industry { get; set; }

        //#endregion
    }
}
