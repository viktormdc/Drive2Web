using System.Collections.Generic;

namespace Analytics.Domain.Models.AdPointerSync.SearchFilters
{
    public class SearchFilterModel
    {
        public string id { get; set; }
        public int version { get; set; }
        public string createdOn { get; set; }
        public string modifiedOn { get; set; }
        public string name { get; set; }
        public string userId { get; set; }

        public List<string> industryIds { get; set; }
        public List<string> companyIds { get; set; }
        public List<string> brandIds { get; set; }
        public List<string> adIds { get; set; }
    }
}
