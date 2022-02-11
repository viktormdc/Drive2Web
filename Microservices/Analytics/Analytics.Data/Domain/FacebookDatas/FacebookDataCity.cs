using Analytics.Data.Implemetation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Domain.FacebookDatas
{
    public class FacebookDataCity : BaseEntity
    {
        /// <summary>
        /// Gets or sets the facebook data identifier.
        /// </summary>
        /// <value>
        /// The facebook data identifier.
        /// </value>
        public int FacebookDataId { get; set; }
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }
        /// <summary>
        /// Gets or sets the page fans city.
        /// </summary>
        /// <value>
        /// The page fans city.
        /// </value>
        public int PageFansCity { get; set; }

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
