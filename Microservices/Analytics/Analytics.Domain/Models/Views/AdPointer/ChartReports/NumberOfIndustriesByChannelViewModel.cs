using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Domain.Models.Views.AdPointer.ChartReports
{
    public class NumberOfIndustriesByChannelViewModel
    {
        public string name { get; set; }
        public int totalValue { get; set; }
        public List<SeriaOfIndustry> series { get; set; }
    }

    public class SeriaOfIndustry
    {
        public string name { get; set; }
        public int value { get; set; }
    }
}
