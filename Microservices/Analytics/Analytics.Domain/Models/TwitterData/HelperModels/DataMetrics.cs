using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Domain.Models.TwitterData.HelperModels
{
    public class DataMetrics
    {
        public string id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public PublicMetrics public_metrics { get; set; }
    }
}
