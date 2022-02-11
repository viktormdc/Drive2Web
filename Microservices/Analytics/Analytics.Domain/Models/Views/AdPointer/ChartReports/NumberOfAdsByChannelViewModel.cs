using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Domain.Models.Views.AdPointer.ChartReports
{
    public class NumberOfAdsByChannelViewModel
    {
        public string name { get; set; }
        public int totalValue { get; set; }
        public List<SeriaOfAd> series { get; set; }
    }

    public class SeriaOfAd
    {
        public string name { get; set; }
        public int value { get; set; }
    }
}
