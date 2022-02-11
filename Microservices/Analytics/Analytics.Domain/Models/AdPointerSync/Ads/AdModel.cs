using System.Collections.Generic;

namespace Analytics.Domain.Models.AdPointerSync.Ads
{
    public class AdModel
    {
        public int totalPages { get; set; }
        public int totalElements { get; set; }

        public int number { get; set; }

        public Sort sort { get; set; }

        public int size { get; set; }

        public List<AdResponse> content { get; set; }
        public bool first { get; set; }
        public int numberOfElements { get; set; }

        public Pageable pageable { get; set; }
        public bool last { get; set; }
        public bool empty { get; set; }
    }
    public class AdResponse
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool isDefined { get; set; }
        public AdBrand brand { get; set; }
        public AdCompany company { get; set; }
        public AdIndustry industry { get; set; }
        public string externalId { get; set; }
        public string videoUrl { get; set; }
        public string videoImgUrl { get; set; }

    }
    public class AdBrand
    {
        public string id { get; set; }
        public string version { get; set; }
        public string createdOn { get; set; }
        public string modifiedOn { get; set; }
        public string name { get; set; }
        public string companyId { get; set; }
    }
    public class AdCompany
    {
        public string id { get; set; }
        public string version { get; set; }
        public string createdOn { get; set; }
        public string modifiedOn { get; set; }
        public string name { get; set; }
        public string industryId { get; set; }
    }
    public class AdIndustry
    {
        public string id { get; set; }
        public string version { get; set; }
        public string createdOn { get; set; }
        public string modifiedOn { get; set; }
        public string name { get; set; }

    }
    public class Pageable
    {
        public Sort sort { get; set; }
        public int offset { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public bool paged { get; set; }
        public bool unpaged { get; set; }
    }
    public class Sort
    {
        public bool sorted { get; set; }
        public bool unsorted { get; set; }
        public bool empty { get; set; }
    }
}
