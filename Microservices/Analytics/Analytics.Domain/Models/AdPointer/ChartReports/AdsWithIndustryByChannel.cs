using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Domain.Models.AdPointer.ChartReports
{
    public class AdsWithIndustryByChannel
    {
        public string AdId { get; set; }
        public string IndustryId { get; set; }
        public DateTime AdEventTime { get; set; }
    }
}
