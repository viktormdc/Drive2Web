using System.Collections.Generic;

namespace Analytics.Domain.Models.AdPointerSync.AdEvents
{
    public class AdEventModel
    {

        public int totalPages { get; set; }
        public int totalElements { get; set; }

        public int number { get; set; }

        public Sort sort { get; set; }

        public int size { get; set; }

        public List<AdEventResponse> content { get; set; }
        public bool first { get; set; }
        public int numberOfElements { get; set; }

        public Pageable pageable { get; set; }
        public bool last { get; set; }
        public bool empty { get; set; }

    }
    public class AdEventResponse
    {
        public string id { get; set; }
        public AdEventAd ad { get; set; }
        public AdEventChannel channel { get; set; }
        public string adEventTime { get; set; }
        public string adStartEventTime { get; set; }
        public string adEndEventTime { get; set; }
        public string programId { get; set; }
        public string programName { get; set; }
        public string programGenre { get; set; }
        public double? programPrice { get; set; }
        public int? programImpressions { get; set; }
    }

    public class AdEventAd
    {
        public string id { get; set; }
        public int version { get; set; }
        public string createdOn { get; set; }
        public string modifiedOn { get; set; }
        public bool isDefined { get; set; }
        public string name { get; set; }

        public string brandId { get; set; }
        public string companyId { get; set; }
        public string industryId { get; set; }
        public string externalId { get; set; }
        public string videoUrl { get; set; }
        public string videoImgUrl { get; set; }

    }
    public class AdEventChannel
    {
        public string id { get; set; }
        public int version { get; set; }
        public string createdOn { get; set; }
        public string modifiedOn { get; set; }
        public string name { get; set; }
        public string externalId { get; set; }
        public string logoPath { get; set; }

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
