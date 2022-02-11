using Analytics.Data.Domain.FacebookDatas;
using Analytics.Data.Domain.TwitterDatas;
using Analytics.Data.Domain.LinkedInDatas;
using Analytics.Data.Implemetation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Analytics.Data.Domain.InstagramDatas;
using Analytics.Data.Domain.GoogleDatas;
using Analytics.Data.Domain.YoutubeDatas;

namespace Analytics.Data.Domain.SocialNetworks
{
  
    public class SocialNetwork : BaseEntity
    {
        
        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        /// <value>
        /// The application identifier.
        /// </value>      
        public string AppId { get; set; }
        
        /// <summary>
        /// Gets or sets the application secret.
        /// </summary>
        /// <value>
        /// The application secret.
        /// </value>      
        public string AppSecret { get; set; }
        
        /// <summary>
        /// Gets or sets the application key.
        /// </summary>
        /// <value>
        /// The application key.
        /// </value>    
        public string AppKey { get; set; }
      
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>     
        public string AccessToken { get; set; }
       
        /// <summary>
        /// Gets or sets the expire time.
        /// </summary>
        /// <value>
        /// The expire time.
        /// </value>      
        public DateTime ExpireTime { get; set; }
      
        /// <summary>
        /// Gets or sets the view identifier.
        /// </summary>
        /// <value>
        /// The view identifier.
        /// </value>
        public string ViewScreen { get; set; }
       
        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        public string ClientId { get; set; }
      
        /// <summary>
        /// Gets or sets the type of the network.
        /// </summary>
        /// <value>
        /// The type of the network.
        /// </value>      
        public int NetworkType { get; set; }
      
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
        /// FacebookData
        /// </summary>
        public virtual List<FacebookData> FacebookData { get; set; }

        /// <summary>
        /// TwitterData
        /// </summary>
        public virtual List<TwitterData> TwitterData { get; set; }

        /// <summary>
        /// LinkedinData
        /// </summary>
        public virtual List<LinkedInData> LinkedInData { get; set; }

        /// <summary>
        /// Gets or sets the instagram data.
        /// </summary>
        /// <value>
        /// The instagram data.
        /// </value>
        public virtual List<InstagramData> InstagramData { get; set; }

        /// <summary>
        /// Gets or sets the google data.
        /// </summary>
        /// <value>
        /// The google data.
        /// </value>
        public virtual List<GoogleData> GoogleData { get; set; }

        /// <summary>
        /// Gets or sets the youtube data.
        /// </summary>
        /// <value>
        /// The youtube data.
        /// </value>
        public virtual List<YoutubeData> YoutubeData { get; set; }

    }
}
