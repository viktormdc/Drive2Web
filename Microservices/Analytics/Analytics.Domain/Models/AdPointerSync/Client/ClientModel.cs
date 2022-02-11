using System;
using System.Collections.Generic;

namespace Analytics.Domain.Models.AdPointerSync.Client
{
    public class ClientModel
    {
        /// <summary>
        /// Gets or sets the totalPages
        /// </summary>
        public int totalPages { get; set; }

        /// <summary>
        /// Gets or sets the totalElements
        /// </summary>
        public int totalElements { get; set; }

        /// <summary>
        /// Gets or sets the number
        /// </summary>
        public int number { get; set; }

        /// <summary>
        /// Gets or sets the sort
        /// </summary>
        public Sort sort { get; set; }

        /// <summary>
        /// Gets or sets the size
        /// </summary>
        public int size { get; set; }

        /// <summary>
        /// Gets or sets the content
        /// </summary>
        public List<UserResponse> content { get; set; }

        /// <summary>
        /// Gets or sets the first
        /// </summary>
        public bool first { get; set; }

        /// <summary>
        /// Gets or sets the numberOfElements
        /// </summary>
        public int numberOfElements { get; set; }

        /// <summary>
        /// Gets or sets the pageable
        /// </summary>
        public Pageable pageable { get; set; }

        /// <summary>
        /// Gets or sets the last
        /// </summary>
        public bool last { get; set; }

        /// <summary>
        /// Gets or sets the empty
        /// </summary>
        public bool empty { get; set; }

    }

    public class Sort
    {
        /// <summary>
        /// Gets or sets the sorted
        /// </summary>
        public bool sorted { get; set; }

        /// <summary>
        /// Gets or sets the unsorted
        /// </summary>
        public bool unsorted { get; set; }

        /// <summary>
        /// Gets or sets the empty
        /// </summary>
        public bool empty { get; set; }
    }

    public class UserResponse
    {
        /// <summary>
        /// Gets or sets the id
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Gets or sets the role
        /// </summary>
        public string role { get; set; }

        /// <summary>
        /// Gets or sets the enabled
        /// </summary>
        public bool enabled { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the phone
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// Gets or sets the createdOn
        /// </summary>
        public DateTime createdOn { get; set; }

        /// <summary>
        /// Gets or sets the modifiedOn
        /// </summary>
        public DateTime modifiedOn { get; set; }

        public Subscription subscription { get; set; }
    }

    public class Pageable
    {
        /// <summary>
        /// Gets or sets the sort
        /// </summary>
        public Sort sort { get; set; }

        /// <summary>
        /// Gets or sets the offset
        /// </summary>
        public int offset { get; set; }

        /// <summary>
        /// Gets or sets the pageNumber
        /// </summary>
        public int pageNumber { get; set; }

        /// <summary>
        /// Gets or sets the pageSize
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// Gets or sets the paged
        /// </summary>
        public bool paged { get; set; }

        /// <summary>
        /// Gets or sets the unpaged
        /// </summary>
        public bool unpaged { get; set; }

    }

    public class Subscription
    {
        /// <summary>
        /// Gets or sets the id
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Gets or sets the version
        /// </summary>
        public int version { get; set; }

        /// <summary>
        /// Gets or sets the createdOn
        /// </summary>
        public DateTime createdOn { get; set; }

        /// <summary>
        /// Gets or sets the modifiedOn
        /// </summary>
        public DateTime modifiedOn { get; set; }

        /// <summary>
        /// Gets or sets the status
        /// </summary>
        public string status { get; set; }


        /// <summary>
        /// Gets or sets the startDate
        /// </summary>
        public DateTime startDate { get; set; }

        /// <summary>
        /// Gets or sets the endDate
        /// </summary>
        public DateTime endDate{ get; set; }

        /// <summary>
        /// Gets or sets the userId
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// Gets or sets the planId
        /// </summary>
        public string planId { get; set; }

        /// <summary>
        /// Gets or sets the type
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Gets or sets the paymentInterval
        /// </summary>
        public string paymentInterval { get; set; }

        /// <summary>
        /// Gets or sets the price
        /// </summary>
        public double price { get; set; }

        /// <summary>
        /// Gets or sets the searchFiltersLimit
        /// </summary>
        public int searchFiltersLimit { get; set; }

        /// <summary>
        /// Gets or sets the searchFilterIndustriesLimit
        /// </summary>
        public int searchFilterIndustriesLimit { get; set; }

        /// <summary>
        /// Gets or sets the searchFilterCompaniesLimit
        /// </summary>
        public int searchFilterCompaniesLimit { get; set; }

        /// <summary>
        /// Gets or sets the searchFilterBrandsLimit
        /// </summary>
        public int searchFilterBrandsLimit { get; set; }
    }

}
