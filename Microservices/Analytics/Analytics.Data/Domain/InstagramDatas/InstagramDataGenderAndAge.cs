using Analytics.Data.Implemetation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Domain.InstagramDatas
{
    public class InstagramDataGenderAndAge : BaseEntity
    {
        /// <summary>
        /// Gets or sets the instagram data identifier.
        /// </summary>
        /// <value>
        /// The instagram data identifier.
        /// </value>
        public int InstagramDataId { get; set; }

        /// <summary>
        /// Gets or sets the gender and age.
        /// </summary>
        /// <value>
        /// The gender and age.
        /// </value>
        public string GenderAndAge { get; set; }

        /// <summary>
        /// Gets or sets the followers gender and age.
        /// </summary>
        /// <value>
        /// The followers gender and age.
        /// </value>
        public int FollowersGenderAndAge { get; set; }

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
