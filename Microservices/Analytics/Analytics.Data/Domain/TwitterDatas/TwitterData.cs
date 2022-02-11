using Analytics.Data.Domain.SocialNetworks;
using Analytics.Data.Implemetation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Domain.TwitterDatas
{
    public class TwitterData :BaseEntity
    {
        /// <summary>
        /// Gets or sets the social network identifier.
        /// </summary>
        /// <value>
        /// The social network identifier.
        /// </value>
        public int SocialNetworkId { get; set; }

        /// <summary>
        /// Gets or sets total followers.
        /// </summary>
        /// <value>
        /// The total followers.
        /// </value>
        public int TotalFollowers { get; set; }

        /// <summary>
        /// Gets or sets total followings.
        /// </summary>
        /// <value>
        /// The total followings.
        /// </value>
        public int TotalFollowings { get; set; }

        /// <summary>
        /// Gets or sets total tweets.
        /// </summary>
        /// <value>
        /// The total tweets.
        /// </value>
        public int TotalTweets { get; set; }

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


        #region NavigationProperties

        /// <summary>
        /// SocialNetwork
        /// </summary>
        public virtual SocialNetwork SocialNetwork { get; set;}

        #endregion

    }
}
