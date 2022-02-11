using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Analytics.Data.Domain.AdEvents;
using Analytics.Data.Domain.Ads;
using Analytics.Data.Domain.Brands;
using Analytics.Data.Domain.Channels;
using Analytics.Data.Domain.Clinets;
using Analytics.Data.Domain.Companies;
using Analytics.Data.Domain.FacebookDatas;
using Analytics.Data.Domain.GoogleDatas;
using Analytics.Data.Domain.Industries;
using Analytics.Data.Domain.InstagramDatas;
using Analytics.Data.Domain.LinkedInDatas;
using Analytics.Data.Domain.SearchFilters;
using Analytics.Data.Domain.SocialNetworks;
using Analytics.Data.Domain.TwitterDatas;
using Analytics.Data.Domain.YoutubeDatas;
using Analytics.Data.Interface;
using Analytics.Domain.Enums.SocialNetworkEnum;
using Analytics.Domain.Models.AdPointer.ChartReports;
using Analytics.Domain.Models.AdPointer.SocialNetworkReach;
using Analytics.Domain.Models.Commands.Logger;
using Analytics.Domain.Models.Helper.SelectListItem;
using Analytics.Domain.Models.Views.AdPointer.ChartReports;
using Analytics.Domain.Models.Views.AdPointer.SocialNetworkRech;
using Analytics.Service.Interface.Analytics;
using Microsoft.Extensions.Logging;
using Rabbit.Domain.Core.Bus;

namespace Analytics.Service.Implementaion.Analytics
{
    public class AnalyticsService : IAnalyticsService

    {
        #region Fields

        private readonly IAnalyticsRepository<Brand> _brandRepository;
        private readonly IAnalyticsRepository<FacebookData> _facebookDataRepository;
        private readonly IAnalyticsRepository<SocialNetwork> _socialNetworkRepository;
        private readonly IAnalyticsRepository<SearchFilter> _searchFilteRepository;
        private readonly IAnalyticsRepository<AdEvent> _adEventRepository;
        private readonly IAnalyticsRepository<Ad> _adRepository;
        private readonly IAnalyticsRepository<SearchFilterAds> _searchFilterAdsRepository;
        private readonly IAnalyticsRepository<Client> _clientRepository;
        private readonly IAnalyticsRepository<SearchFilterBrands> _searchFilterBrandsRepository;
        private readonly IAnalyticsRepository<SearchFilterCompanies> _searchFilterCompaniesRepository;
        private readonly IAnalyticsRepository<Company> _companyRepository;
        private readonly IAnalyticsRepository<SearchFilterIndustrys> _searchFilterIndustrysRepository;
        private readonly IAnalyticsRepository<Industry> _industryRepository;
        private readonly IAnalyticsRepository<Channel> _channelRepository;
        private readonly IAnalyticsRepository<GoogleData> _googleDataRepository;
        private readonly IAnalyticsRepository<InstagramData> _instagramDataRepository;
        private readonly IAnalyticsRepository<YoutubeData> _youtubeDataRepository;
        private readonly IAnalyticsRepository<LinkedInData> _linkedinDataRepository;
        private readonly IAnalyticsRepository<TwitterData> _twitterDataRepository;
        private readonly IEventBus _eventBus;
        #endregion

        #region Ctor

        public AnalyticsService(IAnalyticsRepository<Brand> brandRepository, IAnalyticsRepository<FacebookData> facebookDataRepository,
            IAnalyticsRepository<SocialNetwork> socialNetworkRepository, IAnalyticsRepository<SearchFilter> searchFilteRepository,
            IAnalyticsRepository<AdEvent> adEventRepository, IAnalyticsRepository<Ad> adRepository,
            IAnalyticsRepository<SearchFilterAds> searchFilterAdsRepository, IAnalyticsRepository<Client> clientRepository,
           IAnalyticsRepository<SearchFilterBrands> searchFilterBrandsRepository, IAnalyticsRepository<SearchFilterCompanies> searchFilterCompaniesRepository,
           IAnalyticsRepository<Company> companyRepository, IAnalyticsRepository<SearchFilterIndustrys> searchFilterIndustrysRepository, IAnalyticsRepository<Industry> industryRepository,
            IAnalyticsRepository<Channel> channelRepository, IAnalyticsRepository<GoogleData> googleDataRepository, IAnalyticsRepository<InstagramData> instagramDataRepository,
            IAnalyticsRepository<YoutubeData> youtubeDataRepository, IAnalyticsRepository<LinkedInData> linkedinDataRepository, IAnalyticsRepository<TwitterData> twitterDataRepository , IEventBus eventBus)
        {
            this._brandRepository = brandRepository;
            this._facebookDataRepository = facebookDataRepository;
            this._socialNetworkRepository = socialNetworkRepository;
            this._searchFilteRepository = searchFilteRepository;
            this._adEventRepository = adEventRepository;
            this._adRepository = adRepository;
            this._searchFilterAdsRepository = searchFilterAdsRepository;
            this._clientRepository = clientRepository;
            this._searchFilterBrandsRepository = searchFilterBrandsRepository;
            this._searchFilterCompaniesRepository = searchFilterCompaniesRepository;
            this._companyRepository = companyRepository;
            this._searchFilterIndustrysRepository = searchFilterIndustrysRepository;
            this._industryRepository = industryRepository;
            this._channelRepository = channelRepository;
            this._googleDataRepository = googleDataRepository;
            this._instagramDataRepository = instagramDataRepository;
            this._youtubeDataRepository = youtubeDataRepository;
            this._linkedinDataRepository = linkedinDataRepository;
            this._twitterDataRepository = twitterDataRepository;
            this._eventBus = eventBus;

        }

        #endregion

        #region Public Methods

        #region AdPointer Report
        /// <summary>
        /// GetSearchFilter
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<SelectListItem>> GetSearchFilter(string userId)
        {
            var searchFilterItems = _searchFilteRepository.FindAsync(x => x.UserId == userId).Result.Select(x =>
                new SelectListItem()
                {
                    Key = x.Id,
                    Value = x.Name
                }).ToList();

            return searchFilterItems;
        }

        /// <summary>
        /// GenerateNumberOfAds
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ChartReportAdsViewModel> GenerateChartReportAds(ChartReportModel model)
        {
            var chartReportViewModels = new ChartReportAdsViewModel();
            var chartReportAds = (from ae in _adEventRepository.GetAllAsync().Result.ToList()
                                  join ad in _adRepository.GetAllAsync().Result.ToList() on ae.AdId equals ad.Id
                                  join sfa in _searchFilterAdsRepository.GetAllAsync().Result.ToList() on ad.Id equals sfa.AdId
                                  join sf in _searchFilteRepository.GetAllAsync().Result.ToList() on sfa.SearchFilterId equals sf.Id
                                  join cl in _clientRepository.GetAllAsync().Result.ToList() on sf.UserId equals cl.Id

                                  where cl.Id == model.UserId && sf.Id == model.SearchFilterItem.Key
                                                              && (ae.AdEventTime >= model.StartDate && ae.AdEventTime <= model.EndDate)
                                  select new
                                  {
                                      AdName = ae.Ad.Name

                                  }).GroupBy(x => x.AdName).ToList();

            var chartReportNumberOfAds = chartReportAds.Select(x => new ChartReportNumberOfAdsModel()
            {
                name = x.Key,
                value = x.Count()
            }).Take(30).ToList();
            chartReportViewModels.ChartReportNumberOfAdsModels = new List<ChartReportNumberOfAdsModel>();
            chartReportViewModels.ChartReportNumberOfAdsModels.AddRange(chartReportNumberOfAds);

            var chartReportTopPercentageAds = chartReportAds.Select(x => new ChartReportTopPercentageAdsModel()
            {
                name = x.Key,
                value = x.Count()
            }).OrderByDescending(x => x.value).Take(10).ToList();

            var topPercentageAdsOthers = chartReportAds.Select(x => new
            {
                name = "Other",
                value = x.Count()

            }).Skip(10).OrderByDescending(x => x.value).ToList();

            var chartReportTopPercentageAdsOthers = topPercentageAdsOthers.Select(x =>
                new ChartReportTopPercentageAdsModel()
                {
                    name = x.name,
                    value = topPercentageAdsOthers.Select(y => y.value).Sum()
                }).FirstOrDefault();
            if (chartReportTopPercentageAdsOthers != null)
            {
                chartReportTopPercentageAds.Add(chartReportTopPercentageAdsOthers);
            }

            chartReportViewModels.ChartReportTopPercentageAdsModels = new List<ChartReportTopPercentageAdsModel>();
            chartReportViewModels.ChartReportTopPercentageAdsModels.AddRange(chartReportTopPercentageAds);

            return chartReportViewModels;
        }

        /// <summary>
        /// Generates the chart report brands.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<ChartReportBrandsViewModel> GenerateChartReportBrands(ChartReportModel model)
        {
            var chartReportViewModels = new ChartReportBrandsViewModel();
            var chartReportBrands = (from ae in _adEventRepository.GetAllAsync().Result.ToList()
                                     join ad in _adRepository.GetAllAsync().Result.ToList() on ae.AdId equals ad.Id
                                     join sfb in _searchFilterBrandsRepository.GetAllAsync().Result.ToList() on ad.BrandId equals sfb.BrandId
                                     join br in _brandRepository.GetAllAsync().Result.ToList() on sfb.BrandId equals br.Id
                                     join sf in _searchFilteRepository.GetAllAsync().Result.ToList() on sfb.SearchFilterId equals sf.Id
                                     join cl in _clientRepository.GetAllAsync().Result.ToList() on sf.UserId equals cl.Id

                                     where cl.Id == model.UserId && sf.Id == model.SearchFilterItem.Key
                                                                 && (ae.AdEventTime >= model.StartDate && ae.AdEventTime <= model.EndDate)
                                     select new
                                     {
                                         BrandName = br.Name

                                     }).GroupBy(x => x.BrandName).ToList();

            var chartReportNumberOfBrands = chartReportBrands.Select(x => new ChartReportNumberOfBrandsModel()
            {
                name = x.Key,
                value = x.Count()
            }).Take(30).OrderByDescending(x => x.value).ToList();
            chartReportViewModels.ChartReportNumberOfBrandsModels = new List<ChartReportNumberOfBrandsModel>();
            chartReportViewModels.ChartReportNumberOfBrandsModels.AddRange(chartReportNumberOfBrands);

            var chartReportTopPercentageBrands = chartReportBrands.Select(x => new ChartReportTopPercentageBrandsModel()
            {
                name = x.Key,
                value = x.Count()
            }).OrderByDescending(x => x.value).Take(10).ToList();

            var topPercentageBrandsOthers = chartReportBrands.Select(x => new
            {
                name = "Other",
                value = x.Count()

            }).Skip(10).OrderByDescending(x => x.value).ToList();

            var chartReportTopPercentageBrandsOthers = topPercentageBrandsOthers.Select(x =>
                new ChartReportTopPercentageBrandsModel()
                {
                    name = x.name,
                    value = topPercentageBrandsOthers.Select(y => y.value).Sum()
                }).FirstOrDefault();
            if (chartReportTopPercentageBrandsOthers != null)
            {
                chartReportTopPercentageBrands.Add(chartReportTopPercentageBrandsOthers);
            }

            chartReportViewModels.ChartReportTopPercentageBrandsModels = new List<ChartReportTopPercentageBrandsModel>();
            chartReportViewModels.ChartReportTopPercentageBrandsModels.AddRange(chartReportTopPercentageBrands);

            return chartReportViewModels;
        }

        /// <summary>
        /// Generates the chart report companies.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<ChartReportCompaniesViewModel> GenerateChartReportCompanies(ChartReportModel model)
        {
            var chartReportViewModels = new ChartReportCompaniesViewModel();
            var chartReportCompanies = (from ae in _adEventRepository.GetAllAsync().Result.ToList()
                                        join ad in _adRepository.GetAllAsync().Result.ToList() on ae.AdId equals ad.Id
                                        join sfc in _searchFilterCompaniesRepository.GetAllAsync().Result.ToList() on ad.CompanyId equals sfc.CompanyId
                                        join cm in _companyRepository.GetAllAsync().Result.ToList() on sfc.CompanyId equals cm.Id
                                        join sf in _searchFilteRepository.GetAllAsync().Result.ToList() on sfc.SearchFilterId equals sf.Id
                                        join cl in _clientRepository.GetAllAsync().Result.ToList() on sf.UserId equals cl.Id

                                        where cl.Id == model.UserId && sf.Id == model.SearchFilterItem.Key
                                                                    && (ae.AdEventTime >= model.StartDate && ae.AdEventTime <= model.EndDate)
                                        select new
                                        {
                                            CompanyName = cm.Name

                                        }).GroupBy(x => x.CompanyName).ToList();

            var chartReportNumberOfCompanies = chartReportCompanies.Select(x => new ChartReportNumberOfCompaniesModel()
            {
                name = x.Key,
                value = x.Count()
            }).Take(30).OrderByDescending(x => x.value).ToList();
            chartReportViewModels.ChartReportNumberOfCompaniesModels = new List<ChartReportNumberOfCompaniesModel>();
            chartReportViewModels.ChartReportNumberOfCompaniesModels.AddRange(chartReportNumberOfCompanies);

            var chartReportTopPercentageCompanies = chartReportCompanies.Select(x => new ChartReportTopPercentageCompaniesModel()
            {
                name = x.Key,
                value = x.Count()
            }).OrderByDescending(x => x.value).Take(10).ToList();

            var topPercentageCompaniesOthers = chartReportCompanies.Select(x => new
            {
                name = "Other",
                value = x.Count()

            }).Skip(10).OrderByDescending(x => x.value).ToList();

            var chartReportTopPercentageCompaniesOthers = topPercentageCompaniesOthers.Select(x =>
                new ChartReportTopPercentageCompaniesModel()
                {
                    name = x.name,
                    value = topPercentageCompaniesOthers.Select(y => y.value).Sum()
                }).FirstOrDefault();
            if (chartReportTopPercentageCompaniesOthers != null)
            {
                chartReportTopPercentageCompanies.Add(chartReportTopPercentageCompaniesOthers);
            }

            chartReportViewModels.ChartReportTopPercentageCompaniesModels = new List<ChartReportTopPercentageCompaniesModel>();
            chartReportViewModels.ChartReportTopPercentageCompaniesModels.AddRange(chartReportTopPercentageCompanies);

            return chartReportViewModels;
        }

        /// <summary>
        /// Generates the chart report industries.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<ChartReportIndustriesViewModel> GenerateChartReportIndustries(ChartReportModel model)
        {
            var chartReportViewModels = new ChartReportIndustriesViewModel();
            var chartReportIndustries = (from ae in _adEventRepository.GetAllAsync().Result.ToList()
                                         join ad in _adRepository.GetAllAsync().Result.ToList() on ae.AdId equals ad.Id
                                         join sfi in _searchFilterIndustrysRepository.GetAllAsync().Result.ToList() on ad.IndustryId equals sfi.IndustryId
                                         join ind in _industryRepository.GetAllAsync().Result.ToList() on sfi.IndustryId equals ind.Id
                                         join sf in _searchFilteRepository.GetAllAsync().Result.ToList() on sfi.SearchFilterId equals sf.Id
                                         join cl in _clientRepository.GetAllAsync().Result.ToList() on sf.UserId equals cl.Id

                                         where cl.Id == model.UserId && sf.Id == model.SearchFilterItem.Key
                                                                     && (ae.AdEventTime >= model.StartDate && ae.AdEventTime <= model.EndDate)
                                         select new
                                         {
                                             IndustryName = ind.Name

                                         }).GroupBy(x => x.IndustryName).ToList();

            var chartReportNumberOfIndustries = chartReportIndustries.Select(x => new ChartReportNumberOfIndustriesModel()
            {
                name = x.Key,
                value = x.Count()
            }).Take(30).OrderByDescending(x => x.value).ToList();
            chartReportViewModels.ChartReportNumberOfIndustriesModels = new List<ChartReportNumberOfIndustriesModel>();
            chartReportViewModels.ChartReportNumberOfIndustriesModels.AddRange(chartReportNumberOfIndustries);

            var chartReportTopPercentageIndustries = chartReportIndustries.Select(x => new ChartReportTopPercentageIndustriesModel()
            {
                name = x.Key,
                value = x.Count()
            }).OrderByDescending(x => x.value).Take(10).ToList();

            var topPercentageIndustriesOthers = chartReportIndustries.Select(x => new
            {
                name = "Other",
                value = x.Count()

            }).Skip(10).OrderByDescending(x => x.value).ToList();

            var chartReportTopPercentageIndustriesOthers = topPercentageIndustriesOthers.Select(x =>
                new ChartReportTopPercentageIndustriesModel()
                {
                    name = x.name,
                    value = topPercentageIndustriesOthers.Select(y => y.value).Sum()
                }).FirstOrDefault();
            if (chartReportTopPercentageIndustriesOthers != null)
            {
                chartReportTopPercentageIndustries.Add(chartReportTopPercentageIndustriesOthers);
            }

            chartReportViewModels.ChartReportTopPercentageIndustriesModels = new List<ChartReportTopPercentageIndustriesModel>();
            chartReportViewModels.ChartReportTopPercentageIndustriesModels.AddRange(chartReportTopPercentageIndustries);

            return chartReportViewModels;
        }

        /// <summary>
        /// GetSearchFilterAds
        /// </summary>
        /// <param name="filterSearchId"></param>
        /// <returns></returns>
        public async Task<List<SelectListItem>> GetSearchFilterAds(string filterSearchId)
        {
            var searchFilterAdItems = (from ad in _adRepository.GetAllAsync().Result.ToList()
                                       join sfa in _searchFilterAdsRepository.GetAllAsync().Result.ToList() on ad.Id equals sfa.AdId
                                       where sfa.SearchFilterId == filterSearchId
                                       select new SelectListItem()
                                       {
                                           Key = ad.Id,
                                           Value = ad.Name

                                       }).ToList();

            return searchFilterAdItems;
        }

        /// <summary>
        /// GetChannels
        /// </summary>
        /// <returns></returns>
        public async Task<List<SelectListItem>> GetChannels()
        {
            var channelItems = _channelRepository.GetAllAsync().Result.Select(x =>
                new SelectListItem()
                {
                    Key = x.Id,
                    Value = x.Name
                }).ToList();

            return channelItems;
        }

        /// <summary>
        /// GenerateNumberOfAdsByChannel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<List<NumberOfAdsByChannelViewModel>> GenerateNumberOfAdsByChannel(NumberOfAdsByChannelModel model)
        {
            var numberOfAdsByChannelViewModel = new List<NumberOfAdsByChannelViewModel>();

            foreach (var channelId in model.SearchChannelItems)
            {
                var adsByChannel = new NumberOfAdsByChannelViewModel();
                adsByChannel.series = new List<SeriaOfAd>();
                var startDate = Convert.ToDateTime(model.StartDate);
                var endDate = Convert.ToDateTime(model.EndDate);
                var channelName = _channelRepository.FindAsync(x => x.Id == channelId).Result.SingleOrDefault();
                var adsByChannelList = _adEventRepository.FindAsync(x => x.ChannelId == channelId).Result.ToList();

                while (endDate >= startDate)
                {

                    var seriaOfAd = new SeriaOfAd();
                    foreach (var adId in model.SearchAdItems)
                    {
                        seriaOfAd.value = seriaOfAd.value + adsByChannelList.Where(x => x.AdId == adId && x.AdEventTime.Date == startDate.Date).Count();

                    }
                    seriaOfAd.name = startDate.Date.ToShortDateString();

                    adsByChannel.name = channelName.Name;
                    adsByChannel.totalValue = adsByChannel.totalValue + seriaOfAd.value;
                    adsByChannel.series.Add(seriaOfAd);
                    startDate = startDate.AddDays(1);
                }
                numberOfAdsByChannelViewModel.Add(adsByChannel);
            }
            return numberOfAdsByChannelViewModel;
        }

        /// <summary>
        /// Gets the search filter brands.
        /// </summary>
        /// <param name="filterSearchId">The filter search identifier.</param>
        /// <returns></returns>
        public async Task<List<SelectListItem>> GetSearchFilterBrands(string filterSearchId)
        {

            var searchFilterBrandsItems = (from br in _brandRepository.GetAllAsync().Result.ToList()
                                           join sfb in _searchFilterBrandsRepository.GetAllAsync().Result.ToList() on br.Id equals sfb.BrandId
                                           where sfb.SearchFilterId == filterSearchId
                                           select new SelectListItem()
                                           {
                                               Key = br.Id,
                                               Value = br.Name

                                           }).ToList();

            return searchFilterBrandsItems;
        }

        public async Task<List<NumberOfBrandsByChannelViewModel>> GenerateNumberOfBrandsByChannel(NumberOfBrandsByChannelModel model)
        {
            var numberOfBrandsByChannelViewModel = new List<NumberOfBrandsByChannelViewModel>();

            foreach (var channelId in model.SearchChannelItems)
            {
                var brandsByChannel = new NumberOfBrandsByChannelViewModel();
                brandsByChannel.series = new List<SeriaOfBrand>();
                var startDate = Convert.ToDateTime(model.StartDate);
                var endDate = Convert.ToDateTime(model.EndDate);
                var channelName = _channelRepository.FindAsync(x => x.Id == channelId).Result.SingleOrDefault();


                var adsWithBrandByChannels = (from ad in _adRepository.GetAllAsync().Result
                                              join ade in _adEventRepository.GetAllAsync().Result on ad.Id equals ade.AdId
                                              where ade.ChannelId == channelId
                                              select new AdsWithBrandByChannel()
                                              {
                                                  AdId = ad.Id,
                                                  BrandId = ad.BrandId,
                                                  AdEventTime = ade.AdEventTime

                                              }).ToList();

                while (endDate >= startDate)
                {

                    var seriaOfBrand = new SeriaOfBrand();
                    foreach (var brandId in model.SearchBrandlItems)
                    {
                        seriaOfBrand.value = seriaOfBrand.value + adsWithBrandByChannels.Where(x => x.BrandId == brandId && x.AdEventTime.Date == startDate.Date).Count();

                    }
                    seriaOfBrand.name = startDate.Date.ToShortDateString();

                    brandsByChannel.name = channelName.Name;
                    brandsByChannel.totalValue = brandsByChannel.totalValue + seriaOfBrand.value;
                    brandsByChannel.series.Add(seriaOfBrand);
                    startDate = startDate.AddDays(1);
                }
                numberOfBrandsByChannelViewModel.Add(brandsByChannel);
            }
            return numberOfBrandsByChannelViewModel;
        }

        public async Task<List<SelectListItem>> GetSearchFilterCompanies(string filterSearchId)
        {

            var searchFilterCompaniesItems = (from cm in _companyRepository.GetAllAsync().Result.ToList()
                                              join sfc in _searchFilterCompaniesRepository.GetAllAsync().Result.ToList() on cm.Id equals sfc.CompanyId
                                              where sfc.SearchFilterId == filterSearchId
                                              select new SelectListItem()
                                              {
                                                  Key = cm.Id,
                                                  Value = cm.Name

                                              }).ToList();

            return searchFilterCompaniesItems;
        }

        public async Task<List<NumberOfCompaniesByChannelViewModel>> GenerateNumberOfCompaniesByChannel(NumberOfCompaniesByChannelModel model)
        {
            var numberOfCompaniesByChannelViewModel = new List<NumberOfCompaniesByChannelViewModel>();

            foreach (var channelId in model.SearchChannelItems)
            {
                var companiesByChannel = new NumberOfCompaniesByChannelViewModel();
                companiesByChannel.series = new List<SeriaOfCompany>();
                var startDate = Convert.ToDateTime(model.StartDate);
                var endDate = Convert.ToDateTime(model.EndDate);
                var channelName = _channelRepository.FindAsync(x => x.Id == channelId).Result.SingleOrDefault();


                var adsWithCompanyByChannels = (from ad in _adRepository.GetAllAsync().Result
                                                join ade in _adEventRepository.GetAllAsync().Result on ad.Id equals ade.AdId
                                                where ade.ChannelId == channelId
                                                select new AdsWithCompanyByChannel()
                                                {
                                                    AdId = ad.Id,
                                                    CompanyId = ad.CompanyId,
                                                    AdEventTime = ade.AdEventTime

                                                }).ToList();

                while (endDate >= startDate)
                {

                    var seriaOfCompany = new SeriaOfCompany();
                    foreach (var companyId in model.SearchCompaniesItems)
                    {
                        seriaOfCompany.value = seriaOfCompany.value + adsWithCompanyByChannels.Where(x => x.CompanyId == companyId && x.AdEventTime.Date == startDate.Date).Count();

                    }
                    seriaOfCompany.name = startDate.Date.ToShortDateString();

                    companiesByChannel.name = channelName.Name;
                    companiesByChannel.totalValue = companiesByChannel.totalValue + seriaOfCompany.value;
                    companiesByChannel.series.Add(seriaOfCompany);
                    startDate = startDate.AddDays(1);
                }
                numberOfCompaniesByChannelViewModel.Add(companiesByChannel);
            }
            return numberOfCompaniesByChannelViewModel;
        }

        public async Task<List<SelectListItem>> GetSearchFilterIndustries(string filterSearchId)
        {

            var searchFilterCompaniesItems = (from ind in _industryRepository.GetAllAsync().Result.ToList()
                                              join sfi in _searchFilterIndustrysRepository.GetAllAsync().Result.ToList() on ind.Id equals sfi.IndustryId
                                              where sfi.SearchFilterId == filterSearchId
                                              select new SelectListItem()
                                              {
                                                  Key = ind.Id,
                                                  Value = ind.Name

                                              }).ToList();

            return searchFilterCompaniesItems;
        }

        public async Task<List<NumberOfIndustriesByChannelViewModel>> GenerateNumberOfIndustriesByChannel(NumberOfIndustriesByChannelModel model)
        {
            var numberOfIndustriesByChannelViewModel = new List<NumberOfIndustriesByChannelViewModel>();

            foreach (var channelId in model.SearchChannelItems)
            {
                var industriesByChannel = new NumberOfIndustriesByChannelViewModel();
                industriesByChannel.series = new List<SeriaOfIndustry>();
                var startDate = Convert.ToDateTime(model.StartDate);
                var endDate = Convert.ToDateTime(model.EndDate);
                var channelName = _channelRepository.FindAsync(x => x.Id == channelId).Result.SingleOrDefault();


                var adsWithIndustryByChannels = (from ad in _adRepository.GetAllAsync().Result
                                                 join ade in _adEventRepository.GetAllAsync().Result on ad.Id equals ade.AdId
                                                 where ade.ChannelId == channelId
                                                 select new AdsWithIndustryByChannel()
                                                 {
                                                     AdId = ad.Id,
                                                     IndustryId = ad.IndustryId,
                                                     AdEventTime = ade.AdEventTime

                                                 }).ToList();

                while (endDate >= startDate)
                {

                    var seriaOfIndustry = new SeriaOfIndustry();
                    foreach (var industryId in model.SearchIndustryItems)
                    {
                        seriaOfIndustry.value = seriaOfIndustry.value + adsWithIndustryByChannels.Where(x => x.IndustryId == industryId && x.AdEventTime.Date == startDate.Date).Count();

                    }
                    seriaOfIndustry.name = startDate.Date.ToShortDateString();

                    industriesByChannel.name = channelName.Name;
                    industriesByChannel.totalValue = industriesByChannel.totalValue + seriaOfIndustry.value;
                    industriesByChannel.series.Add(seriaOfIndustry);
                    startDate = startDate.AddDays(1);
                }
                numberOfIndustriesByChannelViewModel.Add(industriesByChannel);
            }
            return numberOfIndustriesByChannelViewModel;
        }

        #endregion

        #region SocialNetwork Report
        /// <summary>
        /// GenerateSocialNetworkReach
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<SocialNetworkReachViewModel> GenerateSocialNetworkReach(SocialNetworkReachModel model)
        {

            var socialNetworkReachViewModel = new SocialNetworkReachViewModel();

            //Google Analytic Reach
            socialNetworkReachViewModel.GoogleDataReachModels = new List<GoogleDataReachModel>();
            socialNetworkReachViewModel.GoogleDataReachModels.AddRange(this.GetGoogleDataReaches(model));

            //Facebook Data Reach
            socialNetworkReachViewModel.FacebookDataPageViewsModels = new List<FacebookDataReachModel>();
            socialNetworkReachViewModel.FacebookDataPageViewsModels.AddRange(this.GetFacebookDataReaches(model));

            //Instagram Data Reach
            socialNetworkReachViewModel.InstagramDataProfileViewsModels = new List<InstagramDataReachModel>();
            socialNetworkReachViewModel.InstagramDataProfileViewsModels.AddRange(this.GetInstagramDataReaches(model));

            //Youtube Data Reach
            socialNetworkReachViewModel.YoutubeDataViewsModels = new List<YoutubeDataReachModel>();
            socialNetworkReachViewModel.YoutubeDataViewsModels.AddRange(this.GetYoutubeDataReaches(model));

            //LinkedIn Data Reach
            socialNetworkReachViewModel.LinkedInDataReachModels = new List<LinkedInDataReachModel>();
            socialNetworkReachViewModel.LinkedInDataReachModels.AddRange(GetLinkedInDataReaches(model));

            //Twitter Data Reach
            socialNetworkReachViewModel.TwitterDataReachModels = new List<TwitterDataReachModel>();
            socialNetworkReachViewModel.TwitterDataReachModels.AddRange(this.GetTwitterReachModels(model));

            return socialNetworkReachViewModel;

        }

        #endregion

        #region Logs
        public async Task InsertLogError(string shortMessage, string exceptionMessage, string customerId)
        {
            var loggerCommandCompleted = new CreateLoggerCommand()
            {
                LogLevel = LogLevel.Error.ToString(),
                ShortMessage = shortMessage,
                ExceptionMessage = exceptionMessage,
                CustomerId = customerId,
                CreatedOn = DateTime.Now

            };
            await this._eventBus.SendCommand(loggerCommandCompleted);
        }
        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// GetGoogleDataReaches
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private List<GoogleDataReachModel> GetGoogleDataReaches(SocialNetworkReachModel model)
        {
            var googleDataReachModels = new List<GoogleDataReachModel>();
            //Google Data
            var googleDataReachSeriesModels = (from gd in _googleDataRepository.GetAllAsync().Result
                                               join sn in _socialNetworkRepository.GetAllAsync().Result on gd.SocialNetworkId equals sn.Id
                                               join cl in _clientRepository.GetAllAsync().Result on sn.ClientId equals cl.Id
                                               where cl.Id == model.UserId && sn.NetworkType == (int)SocialNetworkEnum.GoogleAnalytic
                                                                           && (gd.CreatedOn.Date >= model.StartDate && gd.CreatedOn.Date <= model.EndDate)
                                               select new GoogleDataReachSeriesModel()
                                               {

                                                   name = gd.CreatedOn.ToShortDateString(),
                                                   value = gd.PageViews

                                               }).OrderBy(x => x.name).ToList();


            var googleDataReachModel = new GoogleDataReachModel();
            googleDataReachModel.name = "Web Site Reach";
            googleDataReachModel.series = new List<GoogleDataReachSeriesModel>();
            googleDataReachModel.series.AddRange(googleDataReachSeriesModels);
            googleDataReachModels.Add(googleDataReachModel);
            double averagePageViews = 0;

            var averageStartDate = Convert.ToDateTime(model.StartDate).AddMonths(-1);
            var averageEndDate = Convert.ToDateTime(model.StartDate);

            var averageGoogleDataReachSeriesModels = (from gd in _googleDataRepository.GetAllAsync().Result
                                                      join sn in _socialNetworkRepository.GetAllAsync().Result on gd.SocialNetworkId equals sn.Id
                                                      join cl in _clientRepository.GetAllAsync().Result on sn.ClientId equals cl.Id
                                                      where cl.Id == model.UserId && sn.NetworkType == (int)SocialNetworkEnum.GoogleAnalytic
                                                                                  && (gd.CreatedOn.Date >= averageStartDate && gd.CreatedOn.Date <= averageEndDate)
                                                      select new GoogleDataReachSeriesModel()
                                                      {

                                                          name = gd.CreatedOn.ToShortDateString(),
                                                          value = gd.PageViews

                                                      }).ToList();


            if (averageGoogleDataReachSeriesModels.Count > 0)
            {
                averagePageViews = averageGoogleDataReachSeriesModels.Select(x => Convert.ToInt32(x.value)).Average(x => x);
                averageGoogleDataReachSeriesModels = new List<GoogleDataReachSeriesModel>();
                foreach (var googleDataReachSeriesModel in googleDataReachSeriesModels)
                {
                    var newAverageGoogleDataReachSeriesModel = new GoogleDataReachSeriesModel();
                    newAverageGoogleDataReachSeriesModel.name = googleDataReachSeriesModel.name;
                    newAverageGoogleDataReachSeriesModel.value = Convert.ToInt32(Math.Round(averagePageViews, 0));
                    averageGoogleDataReachSeriesModels.Add(newAverageGoogleDataReachSeriesModel);
                }
            }
         

            var averageGoogleDataReachModel = new GoogleDataReachModel();
            averageGoogleDataReachModel.name = "Average";
            averageGoogleDataReachModel.series = new List<GoogleDataReachSeriesModel>();
            averageGoogleDataReachModel.series.AddRange(averageGoogleDataReachSeriesModels);



            googleDataReachModels.Add(averageGoogleDataReachModel);

            return googleDataReachModels;
        }

        /// <summary>
        /// GetFacebookDataReaches
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private List<FacebookDataReachModel> GetFacebookDataReaches(SocialNetworkReachModel model)
        {
            var facebookDataReachModels = new List<FacebookDataReachModel>();

            var facebookDataReachSeriesModels = (from fb in _facebookDataRepository.GetAllAsync().Result
                                                 join sn in _socialNetworkRepository.GetAllAsync().Result on fb.SocialNetworkId equals sn.Id
                                                 join cl in _clientRepository.GetAllAsync().Result on sn.ClientId equals cl.Id
                                                 where cl.Id == model.UserId && sn.NetworkType == (int)SocialNetworkEnum.Facebook
                                                                             && (fb.CreatedOn >= model.StartDate && fb.CreatedOn <= model.EndDate)
                                                 select new FacebookDataReachSeriesModel()
                                                 {

                                                     name = fb.CreatedOn.ToShortDateString(),
                                                     value = fb.PageViews

                                                 }).OrderBy(x => x.name).ToList();
            var facebookDataReachModel = new FacebookDataReachModel();
            facebookDataReachModel.name = "Facebook page reach";
            facebookDataReachModel.series = new List<FacebookDataReachSeriesModel>();
            facebookDataReachModel.series.AddRange(facebookDataReachSeriesModels);
            facebookDataReachModels.Add(facebookDataReachModel);

            double averageFacebookPageViews = 0;
            var averageStartDate = Convert.ToDateTime(model.StartDate).AddMonths(-1);
            var averageEndDate = Convert.ToDateTime(model.StartDate);


            var averageFacebookPageViewsSeriesModels = (from fb in _facebookDataRepository.GetAllAsync().Result
                                                        join sn in _socialNetworkRepository.GetAllAsync().Result on fb.SocialNetworkId equals sn.Id
                                                        join cl in _clientRepository.GetAllAsync().Result on sn.ClientId equals cl.Id
                                                        where cl.Id == model.UserId && sn.NetworkType == (int)SocialNetworkEnum.Facebook
                                                                                    && (fb.CreatedOn.Date >= averageStartDate && fb.CreatedOn.Date <= averageEndDate)
                                                        select new FacebookDataReachSeriesModel()
                                                        {

                                                            name = fb.CreatedOn.ToShortDateString(),
                                                            value = fb.PageViews

                                                        }).ToList();
            if (averageFacebookPageViewsSeriesModels.Count > 0)
            {
                averageFacebookPageViews = averageFacebookPageViewsSeriesModels.Select(x => Convert.ToInt32(x.value)).Average(x => x);
                averageFacebookPageViewsSeriesModels = new List<FacebookDataReachSeriesModel>();
                foreach (var facebookDataReachSeriesModel in facebookDataReachSeriesModels)
                {
                    var newAverageFacebookPageViewsSeriesModel = new FacebookDataReachSeriesModel();
                    newAverageFacebookPageViewsSeriesModel.name = facebookDataReachSeriesModel.name;
                    newAverageFacebookPageViewsSeriesModel.value = Convert.ToInt32(Math.Round(averageFacebookPageViews, 0));
                    averageFacebookPageViewsSeriesModels.Add(newAverageFacebookPageViewsSeriesModel);
                }
            }
        

            var averageFacebookDataReachModel = new FacebookDataReachModel();
            averageFacebookDataReachModel.name = "Average";
            averageFacebookDataReachModel.series = new List<FacebookDataReachSeriesModel>();
            averageFacebookDataReachModel.series.AddRange(averageFacebookPageViewsSeriesModels);
            facebookDataReachModels.Add(averageFacebookDataReachModel);

            return facebookDataReachModels;
        }

        /// <summary>
        /// GetFacebookDataReaches
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private List<InstagramDataReachModel> GetInstagramDataReaches(SocialNetworkReachModel model)
        {
            var instagramDataReachModels = new List<InstagramDataReachModel>();
            var instagramDataReachSeriesModels = (from ins in _instagramDataRepository.GetAllAsync().Result
                                                  join sn in _socialNetworkRepository.GetAllAsync().Result on ins.SocialNetworkId equals sn.Id
                                                  join cl in _clientRepository.GetAllAsync().Result on sn.ClientId equals cl.Id
                                                  where cl.Id == model.UserId && sn.NetworkType == (int)SocialNetworkEnum.Instagram
                                                                              && (ins.CreatedOn.Date >= model.StartDate && ins.CreatedOn.Date <= model.EndDate)
                                                  select new InstagramDataReachSeriesModel()
                                                  {

                                                      name = ins.CreatedOn.ToShortDateString(),
                                                      value = ins.ProfileView

                                                  }).OrderBy(x => x.name).ToList();
            var instagramDataReachModel = new InstagramDataReachModel();
            instagramDataReachModel.name = "Intagram page reach";
            instagramDataReachModel.series = new List<InstagramDataReachSeriesModel>();
            instagramDataReachModel.series.AddRange(instagramDataReachSeriesModels);
            instagramDataReachModels.Add(instagramDataReachModel);

            double averageInstagramReach = 0;
            var averageStartDate = Convert.ToDateTime(model.StartDate).AddMonths(-1);
            var averageEndDate = Convert.ToDateTime(model.StartDate);


            var averageInstagramReachSeriesModels = (from ins in _instagramDataRepository.GetAllAsync().Result
                                                     join sn in _socialNetworkRepository.GetAllAsync().Result on ins.SocialNetworkId equals sn.Id
                                                     join cl in _clientRepository.GetAllAsync().Result on sn.ClientId equals cl.Id
                                                     where cl.Id == model.UserId && sn.NetworkType == (int)SocialNetworkEnum.Instagram
                                                                                 && (ins.CreatedOn.Date >= averageStartDate && ins.CreatedOn.Date <= averageEndDate)
                                                     select new InstagramDataReachSeriesModel()
                                                     {

                                                         name = ins.CreatedOn.ToShortDateString(),
                                                         value = ins.ProfileView

                                                     }).ToList();
            if (averageInstagramReachSeriesModels.Count > 0)
            {
                averageInstagramReach = averageInstagramReachSeriesModels.Select(x => Convert.ToInt32(x.value)).Average(x => x);
                averageInstagramReachSeriesModels = new List<InstagramDataReachSeriesModel>();
                foreach (var instagramDataReachSeriesModel in instagramDataReachSeriesModels)
                {
                    var newAverageInstagramReachSeriesModel = new InstagramDataReachSeriesModel();
                    newAverageInstagramReachSeriesModel.name = instagramDataReachSeriesModel.name;
                    newAverageInstagramReachSeriesModel.value = Convert.ToInt32(Math.Round(averageInstagramReach, 0));
                    averageInstagramReachSeriesModels.Add(newAverageInstagramReachSeriesModel);
                }
            }


            var averageInstagramDataReachModel = new InstagramDataReachModel();
            averageInstagramDataReachModel.name = "Average";
            averageInstagramDataReachModel.series = new List<InstagramDataReachSeriesModel>();
            averageInstagramDataReachModel.series.AddRange(averageInstagramReachSeriesModels);
            instagramDataReachModels.Add(averageInstagramDataReachModel);

            return instagramDataReachModels;
        }

        /// <summary>
        /// GetYoutubeDataReaches
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private List<YoutubeDataReachModel> GetYoutubeDataReaches(SocialNetworkReachModel model)
        {
            var youtubeDataReachModels = new List<YoutubeDataReachModel>();

            var youtubeDataReachSeriesModels = (from yt in _youtubeDataRepository.GetAllAsync().Result
                                                join sn in _socialNetworkRepository.GetAllAsync().Result on yt.SocialNetworkId equals sn.Id
                                                join cl in _clientRepository.GetAllAsync().Result on sn.ClientId equals cl.Id
                                                where cl.Id == model.UserId && sn.NetworkType == (int)SocialNetworkEnum.Youtube
                                                                            && (yt.CreatedOn.Date >= model.StartDate && yt.CreatedOn.Date <= model.EndDate)
                                                select new YoutubeDataReachSeriesModel()
                                                {

                                                    name = yt.CreatedOn.ToShortDateString(),
                                                    value = yt.Views

                                                }).OrderBy(x => x.name).ToList();
            var youtubeDataReachModel = new YoutubeDataReachModel();
            youtubeDataReachModel.name = "Youtube page reach";
            youtubeDataReachModel.series = new List<YoutubeDataReachSeriesModel>();
            youtubeDataReachModel.series.AddRange(youtubeDataReachSeriesModels);
            youtubeDataReachModels.Add(youtubeDataReachModel);

            double averageYoutubeReach = 0;
            var averageStartDate = Convert.ToDateTime(model.StartDate).AddMonths(-1);
            var averageEndDate = Convert.ToDateTime(model.StartDate);


            var averageYoutubeReachSeriesModels = (from yt in _youtubeDataRepository.GetAllAsync().Result
                                                   join sn in _socialNetworkRepository.GetAllAsync().Result on yt.SocialNetworkId equals sn.Id
                                                   join cl in _clientRepository.GetAllAsync().Result on sn.ClientId equals cl.Id
                                                   where cl.Id == model.UserId && sn.NetworkType == (int)SocialNetworkEnum.Youtube
                                                                               && (yt.CreatedOn.Date >= averageStartDate && yt.CreatedOn.Date <= averageEndDate)
                                                   select new YoutubeDataReachSeriesModel()
                                                   {

                                                       name = yt.CreatedOn.ToShortDateString(),
                                                       value = yt.Views

                                                   }).ToList();

            if (averageYoutubeReachSeriesModels.Count > 0)
            {
                averageYoutubeReach = averageYoutubeReachSeriesModels.Select(x => Convert.ToInt32(x.value)).Average(x => x);
                averageYoutubeReachSeriesModels = new List<YoutubeDataReachSeriesModel>();
                foreach (var youtubeDataReachSeriesModel in youtubeDataReachSeriesModels)
                {
                    var newAverageYoutubeReachSeriesModel = new YoutubeDataReachSeriesModel();
                    newAverageYoutubeReachSeriesModel.name = youtubeDataReachSeriesModel.name;
                    newAverageYoutubeReachSeriesModel.value = Convert.ToInt32(Math.Round(averageYoutubeReach, 0));
                    averageYoutubeReachSeriesModels.Add(newAverageYoutubeReachSeriesModel);
                }
            }

          

            var averageYoutubeReachModel = new YoutubeDataReachModel();
            averageYoutubeReachModel.name = "Average";
            averageYoutubeReachModel.series = new List<YoutubeDataReachSeriesModel>();
            averageYoutubeReachModel.series.AddRange(averageYoutubeReachSeriesModels);
            youtubeDataReachModels.Add(averageYoutubeReachModel);

            return youtubeDataReachModels;
        }

        /// <summary>
        /// GetLinkedInDataReaches
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private List<LinkedInDataReachModel> GetLinkedInDataReaches(SocialNetworkReachModel model)
        {
            var linkedInDataReachModels = new List<LinkedInDataReachModel>();

            var linkedInDataReachSeriesModels = (from ln in _linkedinDataRepository.GetAllAsync().Result
                                                 join sn in _socialNetworkRepository.GetAllAsync().Result on ln.SocialNetworkId equals sn.Id
                                                 join cl in _clientRepository.GetAllAsync().Result on sn.ClientId equals cl.Id
                                                 where cl.Id == model.UserId && sn.NetworkType == (int)SocialNetworkEnum.LinkedIn
                                                                             && (ln.CreatedOn.Date >= model.StartDate && ln.CreatedOn.Date <= model.EndDate)
                                                 select new LinkedInDataSeriesModel()
                                                 {

                                                     name = ln.CreatedOn.ToShortDateString(),
                                                     value = ln.TotalFollowers

                                                 }).OrderBy(x => x.name).ToList();
            var linkedInDataReachModel = new LinkedInDataReachModel();
            linkedInDataReachModel.name = "Instagram page reach";
            linkedInDataReachModel.series = new List<LinkedInDataSeriesModel>();
            linkedInDataReachModel.series.AddRange(linkedInDataReachSeriesModels);
            linkedInDataReachModels.Add(linkedInDataReachModel);

            double averageInstagramReach = 0;
            var averageStartDate = Convert.ToDateTime(model.StartDate).AddMonths(-1);
            var averageEndDate = Convert.ToDateTime(model.StartDate);

            var averageLinkedInReachSeriesModels = (from ln in _linkedinDataRepository.GetAllAsync().Result
                                                    join sn in _socialNetworkRepository.GetAllAsync().Result on ln.SocialNetworkId equals sn.Id
                                                    join cl in _clientRepository.GetAllAsync().Result on sn.ClientId equals cl.Id
                                                    where cl.Id == model.UserId && sn.NetworkType == (int)SocialNetworkEnum.LinkedIn
                                                                                && (ln.CreatedOn.Date >= averageStartDate && ln.CreatedOn.Date <= averageEndDate)
                                                    select new LinkedInDataSeriesModel()
                                                    {

                                                        name = ln.CreatedOn.ToShortDateString(),
                                                        value = ln.TotalFollowers

                                                    }).ToList();
            if (averageLinkedInReachSeriesModels.Count > 0)
            {
                averageInstagramReach = averageLinkedInReachSeriesModels.Select(x => Convert.ToInt32(x.value)).Average(x => x);
                averageLinkedInReachSeriesModels = new List<LinkedInDataSeriesModel>();
                foreach (var linkedInDataReachSeriesModel in linkedInDataReachSeriesModels)
                {
                    var newAverageLinkedInReachSeriesModels = new LinkedInDataSeriesModel();
                    newAverageLinkedInReachSeriesModels.name = linkedInDataReachSeriesModel.name;
                    newAverageLinkedInReachSeriesModels.value = Convert.ToInt32(Math.Round(averageInstagramReach, 0));
                    averageLinkedInReachSeriesModels.Add(newAverageLinkedInReachSeriesModels);
                }
            }


            var averageLinkedInReachModel = new LinkedInDataReachModel();
            averageLinkedInReachModel.name = "Average";
            averageLinkedInReachModel.series = new List<LinkedInDataSeriesModel>();
            averageLinkedInReachModel.series.AddRange(averageLinkedInReachSeriesModels);
            linkedInDataReachModels.Add(averageLinkedInReachModel);

            return linkedInDataReachModels;
        }

        /// <summary>
        /// GetTwitterReachModels
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private List<TwitterDataReachModel> GetTwitterReachModels(SocialNetworkReachModel model)
        {
            var twitterDataReachModels = new List<TwitterDataReachModel>();

            var twitterDataReachSeriesModels = (from tw in _twitterDataRepository.GetAllAsync().Result
                                                join sn in _socialNetworkRepository.GetAllAsync().Result on tw.SocialNetworkId equals sn.Id
                                                join cl in _clientRepository.GetAllAsync().Result on sn.ClientId equals cl.Id
                                                where cl.Id == model.UserId && sn.NetworkType == (int)SocialNetworkEnum.Twitter
                                                                            && (tw.CreatedOn.Date >= model.StartDate && tw.CreatedOn.Date <= model.EndDate)
                                                select new TwitterDataSeriesModel()
                                                {

                                                    name = tw.CreatedOn.ToShortDateString(),
                                                    value = tw.TotalFollowers

                                                }).OrderBy(x => x.name).ToList();
            var twitterDataReachModel = new TwitterDataReachModel();
            twitterDataReachModel.name = "Twitter page reach";
            twitterDataReachModel.series = new List<TwitterDataSeriesModel>();
            twitterDataReachModel.series.AddRange(twitterDataReachSeriesModels);
            twitterDataReachModels.Add(twitterDataReachModel);

            double averageTwitterReach = 0;
            var averageStartDate = Convert.ToDateTime(model.StartDate).AddMonths(-1);
            var averageEndDate = Convert.ToDateTime(model.StartDate);


            var averageTwitterReachSeriesModels = (from tw in _twitterDataRepository.GetAllAsync().Result
                                                   join sn in _socialNetworkRepository.GetAllAsync().Result on tw.SocialNetworkId equals sn.Id
                                                   join cl in _clientRepository.GetAllAsync().Result on sn.ClientId equals cl.Id
                                                   where cl.Id == model.UserId && sn.NetworkType == (int)SocialNetworkEnum.Twitter
                                                                               && (tw.CreatedOn.Date >= averageStartDate && tw.CreatedOn.Date <= averageEndDate)
                                                   select new TwitterDataSeriesModel()
                                                   {

                                                       name = tw.CreatedOn.ToShortDateString(),
                                                       value = tw.TotalFollowers

                                                   }).ToList();
            if (averageTwitterReachSeriesModels.Count > 0)
            {
                averageTwitterReach = averageTwitterReachSeriesModels.Select(x => Convert.ToInt32(x.value)).Average(x => x);

                averageTwitterReachSeriesModels = new List<TwitterDataSeriesModel>();
                foreach (var twitterDataReachSeriesModel in twitterDataReachSeriesModels)
                {
                    var newAverageTwitterReachSeriesModel = new TwitterDataSeriesModel();
                    newAverageTwitterReachSeriesModel.name = twitterDataReachSeriesModel.name;
                    newAverageTwitterReachSeriesModel.value = Convert.ToInt32(Math.Round(averageTwitterReach, 0));
                    averageTwitterReachSeriesModels.Add(newAverageTwitterReachSeriesModel);
                }
            }

         

            var averageTwitterReachModel = new TwitterDataReachModel();
            averageTwitterReachModel.name = "Average";
            averageTwitterReachModel.series = new List<TwitterDataSeriesModel>();
            averageTwitterReachModel.series.AddRange(averageTwitterReachSeriesModels);
            twitterDataReachModels.Add(averageTwitterReachModel);
            return twitterDataReachModels;
        }
        #endregion



    }
}
