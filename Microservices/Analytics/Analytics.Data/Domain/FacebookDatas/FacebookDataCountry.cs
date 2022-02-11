using Analytics.Data.Implemetation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Domain.FacebookDatas
{
    public class FacebookDataCountry :BaseEntity
    {
        /// <summary>
        /// Gets or sets the facebook data identifier.
        /// </summary>
        /// <value>
        /// The facebook data identifier.
        /// </value>
        public int FacebookDataId { get; set; }
        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; }
        /// <summary>
        /// Gets or sets the page fans country.
        /// </summary>
        /// <value>
        /// The page fans country.
        /// </value>
        public int PageFansCountry { get; set; }

        #region NavigationProperties

        /// <summary>
        /// Gets or sets the facebook data.
        /// </summary>
        /// <value>
        /// The facebook data.
        /// </value>
        public virtual FacebookData FacebookData { get; set; }

        #endregion

    }
}
