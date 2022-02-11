using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Domain.Models.AdPointer.SocialNetworkReach
{
    public class FacebookDataReachModel
    {
        public string name { get; set; }
        public List<FacebookDataReachSeriesModel> series { get; set; }
    }
    public class FacebookDataReachSeriesModel
    {
        public string name { get; set; }
        public int value { get; set; }
    }
}
