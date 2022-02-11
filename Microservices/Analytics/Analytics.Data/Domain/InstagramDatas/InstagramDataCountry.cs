using Analytics.Data.Implemetation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Domain.InstagramDatas
{
   public class InstagramDataCountry : BaseEntity
    {
        /// <summary>
        /// Gets or sets the instagram data identifier.
        /// </summary>
        /// <value>
        /// The instagram data identifier.
        /// </value>
        public int InstagramDataId { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; }
        /// <summary>
        /// Gets or sets the followers country.
        /// </summary>
        /// <value>
        /// The followers country.
        /// </value>
        public int FollowersCountry { get; set; }

        #region NavigationProperties

        /// <summary>
        /// Gets or sets the facebook data.
        /// </summary>
        /// <value>
        /// The facebook data.
        /// </value>
        public virtual InstagramData InstagramData { get; set; }

        #endregion
    }
}
