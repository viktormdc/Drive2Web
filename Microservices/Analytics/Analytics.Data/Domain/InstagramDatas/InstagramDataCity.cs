using Analytics.Data.Implemetation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Domain.InstagramDatas
{
   public class InstagramDataCity : BaseEntity
    {
        /// <summary>
        /// Gets or sets the instagram data identifier.
        /// </summary>
        /// <value>
        /// The instagram data identifier.
        /// </value>
        public int InstagramDataId { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }
        /// <summary>
        /// Gets or sets the followers city.
        /// </summary>
        /// <value>
        /// The followers city.
        /// </value>
        public int FollowersCity { get; set; }

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
