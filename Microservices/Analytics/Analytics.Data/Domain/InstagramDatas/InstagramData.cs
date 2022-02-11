using Analytics.Data.Domain.SocialNetworks;
using Analytics.Data.Implemetation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Domain.InstagramDatas
{
    public class InstagramData : BaseEntity
    {
        /// <summary>
        /// Gets or sets the social network identifier.
        /// </summary>
        /// <value>
        /// The social network identifier.
        /// </value>
        public int SocialNetworkId { get; set; }
        /// <summary>
        /// Gets or sets the instagram followers.
        /// </summary>
        /// <value>
        /// The instagram followers.
        /// </value>
        public int InstagramFollowers { get; set; }
        /// <summary>
        /// Gets or sets the profile impression.
        /// </summary>
        /// <value>
        /// The profile impression.
        /// </value>
        public int ProfileImpression { get; set; }
        /// <summary>
        /// Gets or sets the profile reach.
        /// </summary>
        /// <value>
        /// The profile reach.
        /// </value>
        public int ProfileReach { get; set; }
        /// <summary>
        /// Gets or sets the profile view.
        /// </summary>
        /// <value>
        /// The profile view.
        /// </value>
        public int ProfileView { get; set; }
        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        /// <value>
        /// The created on.
        /// </value>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// Gets or sets the modified on.
        /// </summary>
        /// <value>
        /// The modified on.
        /// </value>
        public DateTime ModifiedOn { get; set; }

        public virtual List<InstagramDataCountry> InstagramDataCountry { get; set; }

        public virtual List<InstagramDataCity> InstagramDataCity { get; set; }

        public virtual List<InstagramDataGenderAndAge> InstagramDataGenderAndAge { get; set; }

        #region NavigationProperties

        /// <summary>
        /// SocialNetwork
        /// </summary>
        public virtual SocialNetwork SocialNetwork { get; set; }

        #endregion
    }
}
