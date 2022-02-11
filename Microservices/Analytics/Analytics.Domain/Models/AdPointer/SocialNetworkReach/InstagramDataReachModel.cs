using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Domain.Models.AdPointer.SocialNetworkReach
{
    public class InstagramDataReachModel
    {
        public string name { get; set; }
        public List<InstagramDataReachSeriesModel> series { get; set; }
    }
    public class InstagramDataReachSeriesModel
    {
        public string name { get; set; }
        public int value { get; set; }
    }
}
