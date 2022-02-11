using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Domain.Models.TwitterData.HelperModels
{
    public class PublicMetrics
    {
        public int followers_count { get; set; }
        public int following_count { get; set; }
        public int tweet_count { get; set; }
        public int listed_count { get; set; }
    }
}
