using Analytics.Data.Domain.SocialNetworks;
using Analytics.Data.Implemetation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Domain.YoutubeDatas
{
   public class YoutubeData : BaseEntity
    {
        /// <summary>
        /// Gets or sets the social network identifier.
        /// </summary>
        /// <value>
        /// The social network identifier.
        /// </value>
        public int SocialNetworkId { get; set; }
        /// <summary>
        /// Gets or sets the views.
        /// </summary>
        /// <value>
        /// The views.
        /// </value>
        public int Views { get; set; }
        /// <summary>
        /// Gets or sets the likes.
        /// </summary>
        /// <value>
        /// The likes.
        /// </value>
        public int Likes { get; set; }
        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public int Comments { get; set; }
        /// <summary>
        /// Gets or sets the subscribers.
        /// </summary>
        /// <value>
        /// The subscribers.
        /// </value>
        public int Subscribers { get; set; }
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
        public virtual SocialNetwork SocialNetwork { get; set; }

        #endregion

    }
}
