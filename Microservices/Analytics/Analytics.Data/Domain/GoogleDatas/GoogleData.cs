using Analytics.Data.Domain.SocialNetworks;
using Analytics.Data.Implemetation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Domain.GoogleDatas
{
    public class GoogleData : BaseEntity
    {
        /// <summary>
        /// Gets or sets the social network identifier.
        /// </summary>
        /// <value>
        /// The social network identifier.
        /// </value>
        public int SocialNetworkId { get; set; }
        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public int Users { get; set; }
        /// <summary>
        /// Creates new users.
        /// </summary>
        /// <value>
        /// The new users.
        /// </value>
        public int NewUsers { get; set; }
        /// <summary>
        /// Gets or sets the page views.
        /// </summary>
        /// <value>
        /// The page views.
        /// </value>
        public int PageViews { get; set; }
        /// <summary>
        /// Gets or sets the sessions.
        /// </summary>
        /// <value>
        /// The sessions.
        /// </value>
        public int Sessions { get; set; }
        /// <summary>
        /// Gets or sets the sessions per user.
        /// </summary>
        /// <value>
        /// The sessions per user.
        /// </value>
        public double SessionsPerUser { get; set; }
        /// <summary>
        /// Gets or sets the page view per session.
        /// </summary>
        /// <value>
        /// The page view per session.
        /// </value>
        public double PageViewPerSession { get; set; }
        /// <summary>
        /// Gets or sets the average duration of the session.
        /// </summary>
        /// <value>
        /// The average duration of the session.
        /// </value>
        public double AvgSessionDuration { get; set; }
        /// <summary>
        /// Gets or sets the bounce rate.
        /// </summary>
        /// <value>
        /// The bounce rate.
        /// </value>
        public double BounceRate { get; set; }
        /// <summary>
        /// Gets or sets the users from desktop.
        /// </summary>
        /// <value>
        /// The users from desktop.
        /// </value>
        public int UsersFromDesktop { get; set; }
        /// <summary>
        /// Creates new usersfromdesktop.
        /// </summary>
        /// <value>
        /// The new users from desktop.
        /// </value>
        public int NewUsersFromDesktop { get; set; }
        /// <summary>
        /// Gets or sets the users from mobile.
        /// </summary>
        /// <value>
        /// The users from mobile.
        /// </value>
        public int UsersFromMobile { get; set; }
        /// <summary>
        /// Creates new usersfrommobile.
        /// </summary>
        /// <value>
        /// The new users from mobile.
        /// </value>
        public int NewUsersFromMobile { get; set; }

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

        /// <summary>
        /// Gets or sets the direct acquisition traffic channel.
        /// </summary>
        /// <value>
        /// The direct acquisition traffic channel.
        /// </value>
        public int DirectAcquisitionTrafficChannel { get; set; }
        /// <summary>
        /// Gets or sets the organic search acquisition traffic channel.
        /// </summary>
        /// <value>
        /// The organic search acquisition traffic channel.
        /// </value>
        public int OrganicSearchAcquisitionTrafficChannel { get; set; }
        /// <summary>
        /// Gets or sets the referral acquisition traffic channel.
        /// </summary>
        /// <value>
        /// The referral acquisition traffic channel.
        /// </value>
        public int ReferralAcquisitionTrafficChannel { get; set; }
        /// <summary>
        /// Gets or sets the social acquisition traffic channel.
        /// </summary>
        /// <value>
        /// The social acquisition traffic channel.
        /// </value>
        public int SocialAcquisitionTrafficChannel { get; set; }

        public virtual List<GoogleDataCountry> GoogleDataCountry { get; set; }

        public virtual List<GoogleDataCity> GoogleDataCity { get; set; }

        public virtual List<GoogleDataGender> GoogleDataGender { get; set; }

        public virtual List<GoogleDataAge> GoogleDataAge { get; set; }

        #region NavigationProperties

        /// <summary>
        /// SocialNetwork
        /// </summary>
        public virtual SocialNetwork SocialNetwork { get; set; }

        #endregion
    }
}
