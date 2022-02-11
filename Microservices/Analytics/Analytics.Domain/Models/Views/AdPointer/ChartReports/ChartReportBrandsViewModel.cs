using Analytics.Domain.Models.AdPointer.ChartReports;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Domain.Models.Views.AdPointer.ChartReports
{
     public class ChartReportBrandsViewModel
    {
        public List<ChartReportNumberOfBrandsModel> ChartReportNumberOfBrandsModels { get; set; }
        public List<ChartReportTopPercentageBrandsModel> ChartReportTopPercentageBrandsModels { get; set; }
    }
}
