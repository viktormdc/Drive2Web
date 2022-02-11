using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Domain.Models.AdPointer.ChartReports
{
    public class AdsWithBrandByChannel
    {
        public string AdId { get; set; }
        public string BrandId { get; set; }
        public DateTime AdEventTime { get; set; }
    }
}
