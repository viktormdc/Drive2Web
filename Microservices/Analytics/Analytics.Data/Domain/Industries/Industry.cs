using Analytics.Data.Domain.Companies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Domain.Industries
{
   public class Industry
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

        ///// <summary>
        ///// Company
        ///// </summary>
        //public virtual List<Company> Company { get; set; }
    }
}
