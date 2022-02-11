using System;
using System.Collections.Generic;
using System.Text;
using Analytics.Data.Implemetation;

namespace Analytics.Data.Domain.Brands
{
  public class Brand
    {
        /// <summary>
        /// Gets or sets the id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the version
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the customerId
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the modifiedOn
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// Gets or sets the createdOn
        /// </summary>
        public DateTime CreatedOn { get; set; }
    }
}
