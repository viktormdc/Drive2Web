using Analytics.Data.Domain.SocialNetworks;
using Analytics.Data.Implemetation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Analytics.Data.Domain.FacebookDatas
{
    
    public class FacebookData : BaseEntity
    {
        /// <summary>
        /// Gets or sets the social network identifier.
        /// </summary>
        /// <value>
        /// The social network identifier.
        /// </value>
    public int SocialNetworkId { get; set; }

        /// <summary>
        /// Gets or sets the total likes.
        /// </summary>
        /// <value>
        /// The total likes.
        /// </value>        
        public int TotalLikes { get; set; }
     
        /// <summary>
        /// Creates new likes.
        /// </summary>
        /// <value>
        /// The new likes.
        /// </value>
        public int NewLikes { get; set; }
      
        /// <summary>
        /// Gets or sets the un likes.
        /// </summary>
        /// <value>
        /// The un likes.
        /// </value>
        public int UnLikes { get; set; }
       
        /// <summary>
        /// Gets or sets the page fans online per day.
        /// </summary>
        /// <value>
        /// The page fans online per day.
        /// </value>
        public int PageFansOnlinePerDay { get; set; }
      
        /// <summary>
        /// Gets or sets the total reach.
        /// </summary>
        /// <value>
        /// The total reach.
        /// </value>
        public int TotalReach { get; set; }
       
        /// <summary>
        /// Gets or sets the organic reach.
        /// </summary>
        /// <value>
        /// The organic reach.
        /// </value>
        public int OrganicReach { get; set; }
       
        /// <summary>
        /// Gets or sets the paid reach.
        /// </summary>
        /// <value>
        /// The paid reach.
        /// </value>
        public int PaidReach { get; set; }
        /// <summary>
        /// Gets or sets the virar reach.
        /// </summary>
        /// <value>
        /// The virar reach.
        /// </value>
        public int ViralReach { get; set; }
      
        /// <summary>
        /// Gets or sets the non viral reach.
        /// </summary>
        /// <value>
        /// The non viral reach.
        /// </value>
        public int NonViralReach { get; set; }
      
        /// <summary>
        /// Gets or sets the total reach of post.
        /// </summary>
        /// <value>
        /// The total reach of post.
        /// </value>
        public int TotalReachOfPost { get; set; }
      
        /// <summary>
        /// Gets or sets the page views.
        /// </summary>
        /// <value>
        /// The page views.
        /// </value>
        public int PageViews { get; set; }
       
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
        /// FacebookDataCountry
        /// </summary>
        public virtual List<FacebookDataCountry> FacebookDataCountry { get; set; }

        /// <summary>
        /// FacebookDataCity
        /// </summary>
        public virtual List<FacebookDataCity> FacebookDataCity { get; set; }

        /// <summary>
        /// FacebookDataGenderAndAge
        /// </summary>
        public virtual List<FacebookDataGenderAndAge> FacebookDataGenderAndAge { get; set; }
      

        /// <summary>
        /// SocialNetwork
        /// </summary>
        public virtual SocialNetwork SocialNetwork { get; set; }

        #endregion
    }
}
