using System;
using System.Collections.Generic;
using System.Text;
using Analytics.Data.Domain.Ads;

namespace Analytics.Data.Domain.AdEvents
{
    public class AdEvent
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the ad identifier.
        /// </summary>
        /// <value>
        /// The ad identifier.
        /// </value>
        public string AdId { get; set; }
        /// <summary>
        /// Gets or sets the channel identifier.
        /// </summary>
        /// <value>
        /// The channel identifier.
        /// </value>
        public string ChannelId { get; set; }
        /// <summary>
        /// Gets or sets the ad event time.
        /// </summary>
        /// <value>
        /// The ad event time.
        /// </value>
        public DateTime AdEventTime { get; set; }
        /// <summary>
        /// Gets or sets the ad start event time.
        /// </summary>
        /// <value>
        /// The ad start event time.
        /// </value>
        public DateTime AdStartEventTime { get; set; }
        /// <summary>
        /// Gets or sets the ad end event time.
        /// </summary>
        /// <value>
        /// The ad end event time.
        /// </value>
        public DateTime AdEndEventTime { get; set; }
        /// <summary>
        /// Gets or sets the program identifier.
        /// </summary>
        /// <value>
        /// The program identifier.
        /// </value>
        public string ProgramId {get;set;}
        /// <summary>
        /// Gets or sets the name of the program.
        /// </summary>
        /// <value>
        /// The name of the program.
        /// </value>
        public string ProgramName { get; set; }
        /// <summary>
        /// Gets or sets the program genre.
        /// </summary>
        /// <value>
        /// The program genre.
        /// </value>
        public string ProgramGenre { get; set; }
        /// <summary>
        /// Gets or sets the program price.
        /// </summary>
        /// <value>
        /// The program price.
        /// </value>
        public double? ProgramPrice { get; set; }
        /// <summary>
        /// Gets or sets the program impressions.
        /// </summary>
        /// <value>
        /// The program impressions.
        /// </value>
        public int? ProgramImpressions { get; set; }

        #region NavigationProperties

        /// <summary>
        /// Ad
        /// </summary>
        public virtual Ad Ad { get; set; }

        #endregion
    }
}
