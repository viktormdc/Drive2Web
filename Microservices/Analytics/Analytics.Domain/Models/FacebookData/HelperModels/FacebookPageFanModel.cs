using Analytics.Domain.Models.FacebookData.HelperModels.DataPages;
using Analytics.Domain.Models.FacebookData.HelperModels.PagePaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Domain.Models.FacebookData.HelperModels
{
    public class FacebookPageFanModel
    {
        public DataPageFanModel[] data { get; set; }
        public PagePagingModel paging { get; set; }
    }
}
