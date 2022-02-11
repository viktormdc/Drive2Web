using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Domain.Models.AdPointer.ChartReports
{
    public class AdsWithCompanyByChannel
    {
        public string AdId { get; set; }
        public string CompanyId { get; set; }
        public DateTime AdEventTime { get; set; }
    }
}
