using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Domain.Models.InstagramData.InstagramModel
{
    public class InstagramAccountModel
    {
        public InstagramBusinessAccount instagram_business_account { get; set; }
        public string id { get; set; }
    }
    public class InstagramBusinessAccount
    {
        public string id { get; set; }
    }
}
