using Analytics.Domain.Models.AdPointer.ChartReports;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Domain.Models.Views.AdPointer.ChartReports
{
    public class ChartReportCompaniesViewModel
    {
        public List<ChartReportNumberOfCompaniesModel> ChartReportNumberOfCompaniesModels { get; set; }
        public List<ChartReportTopPercentageCompaniesModel> ChartReportTopPercentageCompaniesModels { get; set; }
    }
}
