using Analytics.Domain.Models.AdPointer.ChartReports;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Domain.Models.Views.AdPointer.ChartReports
{
    public class ChartReportIndustriesViewModel
    {
        public List<ChartReportNumberOfIndustriesModel> ChartReportNumberOfIndustriesModels { get; set; }
        public List<ChartReportTopPercentageIndustriesModel> ChartReportTopPercentageIndustriesModels { get; set; }
    }
}
