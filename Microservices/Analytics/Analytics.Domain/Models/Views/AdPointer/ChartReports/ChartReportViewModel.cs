using System;
using System.Collections.Generic;
using System.Text;
using Analytics.Domain.Models.AdPointer.ChartReports;

namespace Analytics.Domain.Models.Views.AdPointer.ChartReports
{
   public class ChartReportViewModel
    {
        public List<ChartReportNumberOfAdsModel> ChartReportNumberOfAdsModels { get; set; }
        public List<ChartReportTopPercentageAdsModel> ChartReportTopPercentageAdsModels { get; set; }
    }
}
