using Analytics.Domain.Models.InstagramData.HelperModels.DataPages;
using Analytics.Domain.Models.InstagramData.HelperModels.PagePaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Domain.Models.InstagramData.InstagramModel
{
    public class InstagramProfileModel
    {
        public ProfileDataModel[] data { get; set; }
        public PagePagingModel paging { get; set; }
    }
}
