using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Domain.Models.AdPointer.SocialNetworkReach
{
   public class YoutubeDataReachModel
    {
        public string name { get; set; }
        public List<YoutubeDataReachSeriesModel> series { get; set; }
    }

    public class YoutubeDataReachSeriesModel
    {
        public string name { get; set; }
        public int value { get; set; }
    }
}
