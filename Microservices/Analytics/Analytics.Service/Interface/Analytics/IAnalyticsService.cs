using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Analytics.Domain.Models.AdPointer.ChartReports;
using Analytics.Domain.Models.AdPointer.SocialNetworkReach;
using Analytics.Domain.Models.Helper.SelectListItem;
using Analytics.Domain.Models.Views.AdPointer.ChartReports;
using Analytics.Domain.Models.Views.AdPointer.SocialNetworkRech;

namespace Analytics.Service.Interface.Analytics
{
    public interface IAnalyticsService
    {
        Task<List<SelectListItem>> GetSearchFilter(string userId);
        Task<ChartReportAdsViewModel> GenerateChartReportAds(ChartReportModel model);
        Task<ChartReportBrandsViewModel> GenerateChartReportBrands(ChartReportModel model);
        Task<ChartReportCompaniesViewModel> GenerateChartReportCompanies(ChartReportModel model);
        Task<ChartReportIndustriesViewModel> GenerateChartReportIndustries(ChartReportModel model);
        Task<SocialNetworkReachViewModel> GenerateSocialNetworkReach(SocialNetworkReachModel model);
        Task<List<SelectListItem>> GetSearchFilterAds(string filterSearchId);
        Task<List<SelectListItem>> GetChannels();
        Task<List<NumberOfAdsByChannelViewModel>> GenerateNumberOfAdsByChannel(NumberOfAdsByChannelModel model);
        Task<List<SelectListItem>> GetSearchFilterBrands(string filterSearchId);
        Task<List<NumberOfBrandsByChannelViewModel>> GenerateNumberOfBrandsByChannel(NumberOfBrandsByChannelModel model);
        Task<List<SelectListItem>> GetSearchFilterCompanies(string filterSearchId);
        Task<List<NumberOfCompaniesByChannelViewModel>> GenerateNumberOfCompaniesByChannel(NumberOfCompaniesByChannelModel model);
        Task<List<SelectListItem>> GetSearchFilterIndustries(string filterSearchId);
        Task<List<NumberOfIndustriesByChannelViewModel>> GenerateNumberOfIndustriesByChannel(NumberOfIndustriesByChannelModel model);
        Task InsertLogError(string shortMessage, string exceptionMessage, string customerId);
    }
}
