using Analytics.Data.Implemetation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Domain.GoogleDatas
{
    public class GoogleDataAge : BaseEntity
    {
        /// <summary>
        /// Gets or sets the google data identifier.
        /// </summary>
        /// <value>
        /// The google data identifier.
        /// </value>
        public int GoogleDataId { get; set; }
        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Age { get; set; }
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

        #region NavigationProperties

        /// <summary>
        /// Gets or sets the google data.
        /// </summary>
        /// <value>
        /// The facebook data.
        /// </value>
        public virtual GoogleData GoogleData { get; set; }
        #endregion
    }
}

