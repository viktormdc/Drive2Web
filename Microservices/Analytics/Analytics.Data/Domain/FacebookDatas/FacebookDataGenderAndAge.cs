using Analytics.Data.Implemetation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Domain.FacebookDatas
{
    public class FacebookDataGenderAndAge : BaseEntity
    {
        /// <summary>
        /// Gets or sets the facebook data identifier.
        /// </summary>
        /// <value>
        /// The facebook data identifier.
        /// </value>
        public int FacebookDataId { get; set; }
        /// <summary>
        /// Gets or sets the gender and age.
        /// </summary>
        /// <value>
        /// The gander and age.
        /// </value>
        public string GenderAndAge { get; set; }
        /// <summary>
        /// Gets or sets the page fans gender and age.
        /// </summary>
        /// <value>
        /// The page fans gender and age.
        /// </value>
        public int PageFansGenderAndAge { get; set; }

        #region NavigationProperties

        /// <summary>
        /// Gets or sets the facebook data.
        /// </summary>
        /// <value>
        /// The facebook data.
        /// </value>
        public virtual FacebookData FacebookData { get; set; }

        #endregion
    }
}
