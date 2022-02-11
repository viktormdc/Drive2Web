using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Domain.Models.Views.AdPointer.ChartReports
{
    public class NumberOfCompaniesByChannelViewModel
    {
        public string name { get; set; }
        public int totalValue { get; set; }
        public List<SeriaOfCompany> series { get; set; }
    }

    public class SeriaOfCompany
    {
        public string name { get; set; }
        public int value { get; set; }
    }
}
