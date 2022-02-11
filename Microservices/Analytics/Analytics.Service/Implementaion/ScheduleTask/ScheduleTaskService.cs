using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using Analytics.Data.Domain.SocialNetworks;
using Analytics.Data.Domain.TwitterDatas;
using Analytics.Data.Domain.YoutubeDatas;
using Analytics.Data.Implemetation;
using Analytics.Data.Interface;
using Analytics.Domain.Enums.AdPointerEnums;
using Analytics.Domain.Enums.SocialNetworkEnum;
using Analytics.Domain.Errors;
using Analytics.Domain.Models.AdPointerSync;
using Analytics.Domain.Models;
using Analytics.Domain.Models.Commands.Logger;
using Analytics.Domain.Models.FacebookData.FacebookModel;
using Analytics.Domain.Models.GoogleData;
using Analytics.Domain.Models.InstagramData.InstagramModel;
using Analytics.Domain.Models.TwitterData.TwitterModel;
using Analytics.Domain.Models.YoutubeData;
using Analytics.Service.Interface.ScheduleTask;
using Facebook;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rabbit.Domain.Core.Bus;
using System.Reflection;
using System.ComponentModel;
using Analytics.Data.Domain.Programs;
using Analytics.Data.Domain.SearchFilters;
using Analytics.Domain.Models.AdPointerSync.AdEvents;
using Analytics.Domain.Models.AdPointerSync.Ads;
using Analytics.Domain.Models.AdPointerSync.AuthToken;
using Analytics.Domain.Models.AdPointerSync.Brands;
using Analytics.Domain.Models.AdPointerSync.Channel;
using Analytics.Domain.Models.AdPointerSync.Client;
using Analytics.Domain.Models.AdPointerSync.Company;
using Analytics.Domain.Models.AdPointerSync.Industry;
using Analytics.Domain.Models.AdPointerSync.Program;
using Analytics.Domain.Models.AdPointerSync.SearchFilters;
using Analytics.Domain.Models.AdPointerSync.Program;

namespace Analytics.Service.Implementaion.ScheduleTask
{
    public class ScheduleTaskService : IScheduleTaskService
    {

        #region Fields

        private readonly IAnalyticsRepository<FacebookData> _facebookDataRepository;
        private readonly IAnalyticsRepository<TwitterData> _twitterDataRepository;
        private readonly IAnalyticsRepository<SocialNetwork> _socialNetworkRepository;
        private readonly IAnalyticsRepository<FacebookDataCountry> _facebookDataCountryRepository;
        private readonly IAnalyticsRepository<FacebookDataCity> _facebookDataCityRepository;
        private readonly IAnalyticsRepository<FacebookDataGenderAndAge> _facebookDataGenderAndAgeRepository;
        private readonly IAnalyticsRepository<InstagramData> _instagramDataRepository;
        private readonly IAnalyticsRepository<InstagramDataCountry> _instagramDataCountryRepository;
        private readonly IAnalyticsRepository<InstagramDataCity> _instagramDataCityRepository;
        private readonly IAnalyticsRepository<InstagramDataGenderAndAge> _instagramDataGenderAndAgeRepository;
        private readonly IAnalyticsRepository<LinkedInData> _linkedinDataRepository;
        private readonly IAnalyticsRepository<Industry> _industryRepository;
        private readonly IAnalyticsRepository<GoogleData> _googleDataRepository;
        private readonly IAnalyticsRepository<GoogleDataCountry> _googleDataCountryRepository;
        private readonly IAnalyticsRepository<GoogleDataCity> _googleDataCityRepository;
        private readonly IAnalyticsRepository<GoogleDataGender> _googleDataGenderRepository;
        private readonly IAnalyticsRepository<GoogleDataAge> _googleDataAgeRepository;
        private readonly IAnalyticsRepository<Brand> _brandRepository;
        private readonly IAnalyticsRepository<YoutubeData> _youtubeDataRepository;
        private readonly IAnalyticsRepository<Company> _companyRepository;
        private readonly IAnalyticsRepository<Channel> _channelRepository;
        private readonly IAnalyticsRepository<Ad> _adRepository;
        private readonly IAnalyticsRepository<AdEvent> _adEventRepository;
        private readonly IAnalyticsRepository<Client> _clientRepository;
        private readonly IAnalyticsRepository<SearchFilter> _searchFilterRepository;
        private readonly IAnalyticsRepository<SearchFilterAds> _searchFilterAdsRepository;
        private readonly IAnalyticsRepository<SearchFilterBrands> _searchFilterBrandsRepository;
        private readonly IAnalyticsRepository<SearchFilterIndustrys> _searchFilterIndustrysRepository;
        private readonly IAnalyticsRepository<SearchFilterCompanies> _searchFilterCompaniesRepository;
        private readonly IAnalyticsRepository<Program> _programRepository;
        private readonly IEventBus _eventBus;
        private IConfiguration _configuration { get; }

        #endregion

        #region Ctor

        public ScheduleTaskService(
            IAnalyticsRepository<FacebookData> facebookDataRepository,
            IAnalyticsRepository<TwitterData> twitterDataRepository,
            IAnalyticsRepository<SocialNetwork> socialNetworkRepository,
            IAnalyticsRepository<FacebookDataCountry> facebookDataCountryRepository,
            IAnalyticsRepository<FacebookDataCity> facebookDataCityRepository,
            IAnalyticsRepository<FacebookDataGenderAndAge> facebookDataGenderAndAgeRepository,
            IAnalyticsRepository<InstagramData> instagramDataRepository,
            IAnalyticsRepository<InstagramDataCountry> instagramDataCountryRepository,
            IAnalyticsRepository<InstagramDataCity> instagramDataCityRepository,
            IAnalyticsRepository<InstagramDataGenderAndAge> instagramDataGenderAndAgeRepository,
            IAnalyticsRepository<LinkedInData> linkedinDataRepository,
            IAnalyticsRepository<Industry> industryRepository,
            IAnalyticsRepository<GoogleData> googleDataRepository,
            IAnalyticsRepository<GoogleDataCountry> googleDataCountryRepository,
            IAnalyticsRepository<GoogleDataCity> googleDataCityRepository,
            IAnalyticsRepository<GoogleDataGender> googleDataGenderRepository,
            IAnalyticsRepository<GoogleDataAge> googleDataAgeRepository,
            IAnalyticsRepository<Brand> brandRepository,
            IAnalyticsRepository<YoutubeData> youtubeDataRepository,
            IAnalyticsRepository<Company> companyRepository,
            IAnalyticsRepository<Channel> channelRepository,
            IAnalyticsRepository<Ad> adRepositor,
            IAnalyticsRepository<AdEvent> adEventRepository,
            IAnalyticsRepository<Client> clientRepository,
            IAnalyticsRepository<SearchFilter> searchFilterRepository,
            IAnalyticsRepository<SearchFilterAds> searchFilterAdsRepository,
            IAnalyticsRepository<SearchFilterBrands> searchFilterBrandsRepository,
            IAnalyticsRepository<SearchFilterIndustrys> searchFilterIndustrysRepository,
            IAnalyticsRepository<SearchFilterCompanies> searchFilterCompaniesRepository,
            IAnalyticsRepository<Program> programRepository,
            IEventBus eventBus,
            IConfiguration configuration


            )
        {

            this._facebookDataRepository = facebookDataRepository;
            this._twitterDataRepository = twitterDataRepository;
            this._socialNetworkRepository = socialNetworkRepository;
            this._facebookDataCountryRepository = facebookDataCountryRepository;
            this._facebookDataCityRepository = facebookDataCityRepository;
            this._facebookDataGenderAndAgeRepository = facebookDataGenderAndAgeRepository;
            this._instagramDataRepository = instagramDataRepository;
            this._instagramDataCountryRepository = instagramDataCountryRepository;
            this._instagramDataCityRepository = instagramDataCityRepository;
            this._instagramDataGenderAndAgeRepository = instagramDataGenderAndAgeRepository;
            this._linkedinDataRepository = linkedinDataRepository;
            this._industryRepository = industryRepository;
            this._googleDataRepository = googleDataRepository;
            this._googleDataCountryRepository = googleDataCountryRepository;
            this._googleDataCityRepository = googleDataCityRepository;
            this._googleDataGenderRepository = googleDataGenderRepository;
            this._googleDataAgeRepository = googleDataAgeRepository;
            this._brandRepository = brandRepository;
            this._youtubeDataRepository = youtubeDataRepository;
            this._companyRepository = companyRepository;
            this._eventBus = eventBus;
            this._configuration = configuration;
            this._channelRepository = channelRepository;
            this._adRepository = adRepositor;
            this._adEventRepository = adEventRepository;
            this._clientRepository = clientRepository;
            this._searchFilterRepository = searchFilterRepository;
            this._searchFilterAdsRepository = searchFilterAdsRepository;
            this._searchFilterBrandsRepository = searchFilterBrandsRepository;
            this._searchFilterIndustrysRepository = searchFilterIndustrysRepository;
            this._searchFilterCompaniesRepository = searchFilterCompaniesRepository;
            this._programRepository = programRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// ProcessingFacebookApi
        /// </summary>
        public async Task ProcessingFacebookApi()
        {
            var companyId = string.Empty;

            var socialNetworks = await _socialNetworkRepository.FindAsync(x => x.NetworkType == (int)SocialNetworkEnum.Facebook);

            foreach (var socialNetwork in socialNetworks)
            {
                try
                {
                    //Processing FacebookApi Data
                    companyId = socialNetwork.ClientId;
                    var client = new FacebookClient(socialNetwork.AccessToken);

                    var facebookAccount = client.Get("/me/accounts");

                    var result = JsonConvert.DeserializeObject<FacebookUserAccountModel>(facebookAccount.ToString());

                    var pageAccessToken = result.data[0].access_token;
                    var pageName = result.data[0].name;
                    var pageId = result.data[0].id;


                    var startDate = (DateTime.Now.Ticks - 621355968000000000) / 10000000;
                    var endDate = (DateTime.Now.Ticks - 621355968000000000) / 10000000;
                    var pageClient = new FacebookClient(pageAccessToken);

                    //Total likes 
                    var getTotalLikes = pageClient.Get(pageId + "/insights/page_fans?&since=" + startDate + "&until=" + endDate);
                    var totalLikesResult = JsonConvert.DeserializeObject<FacebookPageFanModel>(getTotalLikes.ToString());
                    var totalLikes = totalLikesResult.data[0].values[0].value;

                    //New likes 
                    var getNewLikes = pageClient.Get(pageId + "/insights/page_fan_adds?&since=" + startDate + "&until=" + endDate);
                    var newLikesResult = JsonConvert.DeserializeObject<FacebookPageFanModel>(getNewLikes.ToString());
                    var newLikes = newLikesResult.data[0].values[0].value;

                    //Unlikes
                    var getUnLikes = pageClient.Get(pageId + "/insights/page_fan_removes_unique?&since=" + startDate + "&until=" + endDate);
                    var unLikesResult = JsonConvert.DeserializeObject<FacebookPageFanModel>(getUnLikes.ToString());
                    var unLikes = unLikesResult.data[0].values[0].value;

                    // Page likers (fans) online per day   
                    var today = (DateTime.Now.AddDays(-1).Ticks - 621355968000000000) / 10000000;
                    var yesterday = (DateTime.Now.AddDays(-2).Ticks - 621355968000000000) / 10000000;
                    var getFansOnlinePerDay = pageClient.Get(pageId + "/insights/page_fans_online_per_day?period=day&since=" + yesterday + "&until=" + today);
                    var fansOnlinePerDayResult = JsonConvert.DeserializeObject<FacebookPageFanModel>(getFansOnlinePerDay.ToString());
                    var fansOnlinePerDay = fansOnlinePerDayResult.data[0].values[0].value;


                    // Total Reach
                    var getTotalReach = pageClient.Get(pageId + "/insights/page_impressions_unique?&since=" + startDate + "&until=" + endDate);
                    var totalReachResult = JsonConvert.DeserializeObject<FacebookPageFanModel>(getTotalReach.ToString());
                    var totalReach = totalReachResult.data[0].values[0].value;

                    // Organic reach
                    var getOrganicReach = pageClient.Get(pageId + "/insights/page_impressions_unique?&since=" + startDate + "&until=" + endDate);
                    var organicReachResult = JsonConvert.DeserializeObject<FacebookPageFanModel>(getOrganicReach.ToString());
                    var organicReach = organicReachResult.data[0].values[0].value;

                    // Paid Reach  
                    var getPaidReach = pageClient.Get(pageId + "/insights/page_impressions_paid_unique?&since=" + startDate + "&until=" + endDate);
                    var paidReachResult = JsonConvert.DeserializeObject<FacebookPageFanModel>(getPaidReach.ToString());
                    var paidReach = paidReachResult.data[0].values[0].value;

                    // Viral reach
                    var getViralReach = pageClient.Get(pageId + "/insights/page_impressions_viral_unique?&since=" + startDate + "&until=" + endDate);
                    var viralReachResult = JsonConvert.DeserializeObject<FacebookPageFanModel>(getViralReach.ToString());
                    var viralReach = viralReachResult.data[0].values[0].value;

                    // Non-viral reach 
                    var getNonViralReach = pageClient.Get(pageId + "/insights/page_impressions_nonviral_unique?&since=" + startDate + "&until=" + endDate);
                    var nonViralReachResult = JsonConvert.DeserializeObject<FacebookPageFanModel>(getNonViralReach.ToString());
                    var nonViralReach = nonViralReachResult.data[0].values[0].value;

                    //	Total reach of posts
                    var getTotalReachOfPost = pageClient.Get(pageId + "/insights/page_posts_impressions_unique?&since=" + startDate + "&until=" + endDate);
                    var totalReachOfPostResult = JsonConvert.DeserializeObject<FacebookPageFanModel>(getTotalReachOfPost.ToString());
                    var totalReachOfPost = totalReachOfPostResult.data[0].values[0].value;

                    // People visiting your pages     
                    var getPeopleVisitingPage = pageClient.Get(pageId + "/insights/page_views_unique?&since=" + yesterday + "&until=" + today);
                    var peopleVisitingPageResult = JsonConvert.DeserializeObject<FacebookPageFanModel>(getPeopleVisitingPage.ToString());
                    var peopleVisitingPage = 0;
                    if (peopleVisitingPageResult.data.Length > 0)
                    {
                        peopleVisitingPage = peopleVisitingPageResult.data[0].values[0].value;
                    }


                    //Page Fans Country 
                    var getPageFansCountry = pageClient.Get(pageId + "/insights/page_fans_country");
                    var pageFansCountryResult = JsonConvert.DeserializeObject<FacebookPageFansCountryCityGender>(getPageFansCountry.ToString());

                    // Page Fans City 
                    var getPageFansCity = pageClient.Get(pageId + "/insights/page_fans_city");
                    var pageFansCityResult = JsonConvert.DeserializeObject<FacebookPageFansCountryCityGender>(getPageFansCity.ToString());

                    // Page Fans Gender and Age
                    var getPageFansGenderAndAge = pageClient.Get(pageId + "/insights/page_fans_gender_age");
                    var pageFansGenderAndAgeResult = JsonConvert.DeserializeObject<FacebookPageFansCountryCityGender>(getPageFansGenderAndAge.ToString());

                    using (var scope = new UnitOfWork())
                    {

                        var facebookData = new FacebookData()
                        {
                            SocialNetworkId = socialNetwork.Id,
                            TotalLikes = totalLikes,
                            NewLikes = newLikes,
                            UnLikes = unLikes,
                            PageFansOnlinePerDay = fansOnlinePerDay,
                            TotalReach = totalReach,
                            OrganicReach = organicReach,
                            PaidReach = paidReach,
                            ViralReach = viralReach,
                            NonViralReach = nonViralReach,
                            TotalReachOfPost = totalReachOfPost,
                            PageViews = peopleVisitingPage,
                            CreatedOn = DateTime.Now,
                            ModifiedOn = DateTime.Now
                        };
                        await _facebookDataRepository.InsertAsync(facebookData);

                        foreach (var pageFansCountry in pageFansCountryResult.data[0].values[0].value)
                        {
                            var facebookDataCountry = new FacebookDataCountry()
                            {
                                FacebookDataId = facebookData.Id,
                                Country = pageFansCountry.Key,
                                PageFansCountry = pageFansCountry.Value

                            };
                            await _facebookDataCountryRepository.InsertAsync(facebookDataCountry);
                        }

                        foreach (var pageFansCity in pageFansCityResult.data[0].values[0].value)
                        {
                            var facebookDataCity = new FacebookDataCity()
                            {
                                FacebookDataId = facebookData.Id,
                                City = pageFansCity.Key,
                                PageFansCity = pageFansCity.Value

                            };
                            await _facebookDataCityRepository.InsertAsync(facebookDataCity);
                        }

                        foreach (var pageFansGenderAndAge in pageFansGenderAndAgeResult.data[0].values[0].value)
                        {
                            var facebookDataGenderAndAge = new FacebookDataGenderAndAge()
                            {
                                FacebookDataId = facebookData.Id,
                                GenderAndAge = pageFansGenderAndAge.Key,
                                PageFansGenderAndAge = pageFansGenderAndAge.Value

                            };
                            await _facebookDataGenderAndAgeRepository.InsertAsync(facebookDataGenderAndAge);
                        }

                        scope.Commit();
                    }
                    var loggerCommandCompleted = new CreateLoggerCommand()
                    {
                        LogLevel = LogLevel.Information.ToString(),
                        ShortMessage = ErrorModel.ShortMessageFacebookCompletedProcessing,
                        ExceptionMessage = string.Empty,

                        CustomerId = companyId,
                        CreatedOn = DateTime.Now

                    };
                    await this._eventBus.SendCommand(loggerCommandCompleted);

                }
                catch (Exception exception)
                {
                    var loggerCommandError = new CreateLoggerCommand()
                    {
                        LogLevel = LogLevel.Error.ToString(),
                        ShortMessage = ErrorModel.ShortMessageFacebook,
                        ExceptionMessage = exception.ToString(),
                        CustomerId = companyId,
                        CreatedOn = DateTime.Now

                    };
                    await this._eventBus.SendCommand(loggerCommandError);

                }

            }

        }

        /// <summary>
        /// ProcessingInstagramApi
        /// </summary>
        public async Task ProcessingInstagramApi()
        {
            var socialNetworks = await _socialNetworkRepository.FindAsync(x => x.NetworkType == (int)SocialNetworkEnum.Instagram);
            foreach (var socialNetwork in socialNetworks)
            {
                try
                {


                    var pageClient = new FacebookClient(socialNetwork.AccessToken);
                    var userAccounts = pageClient.Get("/me/accounts");
                    var userAccountsResult = JsonConvert.DeserializeObject<InstagramUserAccountModel>(userAccounts.ToString());
                    var instagramPageName = userAccountsResult.data[0].name;
                    var instagramPageId = userAccountsResult.data[0].id;

                    //Get instagram account Id                  
                    var instagramAccount = pageClient.Get(instagramPageId + "?fields=instagram_business_account");
                    var instagramAccountResult = JsonConvert.DeserializeObject<InstagramAccountModel>(instagramAccount.ToString());
                    var instagramAccountId = instagramAccountResult.instagram_business_account.id;

                    //Get instagram Followers
                    var getInstagramFollowers = pageClient.Get(instagramAccountId + "?fields=followers_count");
                    var instagramFollowersResult = JsonConvert.DeserializeObject<InstagramFollowersModel>(getInstagramFollowers.ToString());
                    var instagramFollowers = instagramFollowersResult.followers_count;

                    //Profile impressions
                    var startDate = (DateTime.Now.AddDays(-1).Ticks - 621355968000000000) / 10000000;
                    var endDate = (DateTime.Now.Ticks - 621355968000000000) / 10000000;
                    var getProfileImpressions = pageClient.Get(instagramAccountId + "/insights?metric=impressions&period=day&since=" + startDate + "&until=" + endDate);
                    var profileImpressionsResult = JsonConvert.DeserializeObject<InstagramProfileModel>(getProfileImpressions.ToString());
                    var profileImpression = 0;
                    if (profileImpressionsResult.data.Length > 0)
                    {
                        profileImpression = profileImpressionsResult.data[0].values[0].value;
                    }

                    //Profile reach
                    var getProfileReach = pageClient.Get(instagramAccountId + "/insights?metric=reach&period=day&since=" + startDate + "&until=" + endDate);
                    var profileReachResult = JsonConvert.DeserializeObject<InstagramProfileModel>(getProfileReach.ToString());
                    var profileReach = 0;
                    if (profileReachResult.data.Length > 0)
                    {
                        profileReach = profileReachResult.data[0].values[0].value;
                    }

                    //Profile View
                    var getProfileView = pageClient.Get(instagramAccountId + "/insights?metric=profile_views&period=day&since=" + startDate + "&until=" + endDate);
                    var profileViewResult = JsonConvert.DeserializeObject<InstagramProfileModel>(getProfileView.ToString());
                    var profileView = 0;
                    if (profileViewResult.data.Length > 0)
                    {
                        profileView = profileViewResult.data[0].values[0].value;
                    }


                    //Audience country
                    var getFollowersCountry = pageClient.Get(instagramAccountId + "/insights?metric=audience_country&period=lifetime");
                    var followersCountryResult = JsonConvert.DeserializeObject<InstagramAudienceCountryCityGenderAndAgeModel>(getFollowersCountry.ToString());


                    //Audience city
                    var getFollowersCity = pageClient.Get(instagramAccountId + "/insights?metric=audience_city&period=lifetime");
                    var followersCityResult = JsonConvert.DeserializeObject<InstagramAudienceCountryCityGenderAndAgeModel>(getFollowersCity.ToString());

                    //Audience Gender and Age
                    var getFollowersGenderAndAge = pageClient.Get(instagramAccountId + "/insights?metric=audience_gender_age&period=lifetime");
                    var followersGenderAndAgeResult = JsonConvert.DeserializeObject<InstagramAudienceCountryCityGenderAndAgeModel>(getFollowersGenderAndAge.ToString());


                    using (var scope = new UnitOfWork())
                    {

                        var instagramData = new InstagramData()
                        {
                            SocialNetworkId = socialNetwork.Id,
                            InstagramFollowers = instagramFollowers,
                            ProfileImpression = profileImpression,
                            ProfileReach = profileReach,
                            ProfileView = profileView,
                            CreatedOn = DateTime.Now,
                            ModifiedOn = DateTime.Now
                        };
                        await _instagramDataRepository.InsertAsync(instagramData);

                        foreach (var followersCountry in followersCountryResult.data[0].values[0].value)
                        {
                            var instagramDataCountry = new InstagramDataCountry()
                            {
                                InstagramDataId = instagramData.Id,
                                Country = followersCountry.Key,
                                FollowersCountry = followersCountry.Value

                            };
                            await _instagramDataCountryRepository.InsertAsync(instagramDataCountry);
                        }

                        foreach (var followersCity in followersCityResult.data[0].values[0].value)
                        {
                            var instagramDataCity = new InstagramDataCity()
                            {
                                InstagramDataId = instagramData.Id,
                                City = followersCity.Key,
                                FollowersCity = followersCity.Value

                            };
                            await _instagramDataCityRepository.InsertAsync(instagramDataCity);
                        }

                        foreach (var followersGenderAndAge in followersGenderAndAgeResult.data[0].values[0].value)
                        {
                            var instagramDataGenderAndAge = new InstagramDataGenderAndAge()
                            {
                                InstagramDataId = instagramData.Id,
                                GenderAndAge = followersGenderAndAge.Key,
                                FollowersGenderAndAge = followersGenderAndAge.Value

                            };
                            await _instagramDataGenderAndAgeRepository.InsertAsync(instagramDataGenderAndAge);
                        }

                        scope.Commit();
                    }
                    var loggerCommandCompleted = new CreateLoggerCommand()
                    {
                        LogLevel = LogLevel.Information.ToString(),
                        ShortMessage = ErrorModel.ShortMessageInstagramCompletedProcessing,
                        ExceptionMessage = string.Empty,
                        CustomerId = socialNetwork.ClientId,
                        CreatedOn = DateTime.Now

                    };
                    await this._eventBus.SendCommand(loggerCommandCompleted);
                }
                catch (Exception exception)
                {
                    var loggerCommandError = new CreateLoggerCommand()
                    {
                        LogLevel = LogLevel.Error.ToString(),
                        ShortMessage = ErrorModel.ShortMessageInstagram,
                        ExceptionMessage = exception.ToString(),
                        CustomerId = socialNetwork.ClientId,
                        CreatedOn = DateTime.Now

                    };
                    await this._eventBus.SendCommand(loggerCommandError);

                }

            }
        }

        /// <summary>
        /// ProcessingTwiterApi
        /// </summary>
        public async Task ProcessingTwiterApi()
        {
            var companyId = string.Empty;

            var socialNetworks = await _socialNetworkRepository.FindAsync(x => x.NetworkType == (int)SocialNetworkEnum.Twitter);

            foreach (var socialNetwork in socialNetworks)
            {
                try
                {
                    //Processing Twitter Data
                    companyId = socialNetwork.ClientId;

                    // User Id URL
                    var screenname = socialNetwork.ViewScreen;
                    var userIdApi = "https://api.twitter.com/1.1/users/show.json?screen_name={0}";
                    var userIdUrl = string.Format(userIdApi, screenname);

                    // Create Web Request 
                    var type = "bearer";
                    HttpWebRequest userIdRequest = (HttpWebRequest)WebRequest.Create(userIdUrl);
                    userIdRequest.Headers.Add("Authorization", string.Format("{0} {1}", type, socialNetwork.AccessToken));
                    userIdRequest.Method = "Get";

                    // Get Web Response
                    WebResponse getResponse = userIdRequest.GetResponse();

                    // Get User Id
                    var userIdResult = string.Empty;
                    Dictionary<string, string> tokens = new Dictionary<string, string>();
                    if (getResponse.ContentLength > 0)
                    {
                        using (var reader = new StreamReader(getResponse.GetResponseStream()))
                        {
                            userIdResult = reader.ReadToEnd();
                        }
                        string[] userIds = userIdResult.Split(',');

                        if (userIds.Length > 0)
                        {
                            tokens.Add(userIds[0].Substring(0, userIds[0].IndexOf(":")),
                                userIds[0].Substring(userIds[0].IndexOf(":") + 1, userIds[0].Length - userIds[0].IndexOf(":") - 1));
                        }
                        else
                        {
                            throw new ApplicationException("The list of users can not be null or empty");
                        }

                    }
                    else
                    {
                        throw new ApplicationException("The Web Response can not be null or empty");
                    }

                    // User ID
                    var twitterUserId = tokens["{\"id\""];


                    // Public Metrics URL
                    var publicMetricsApi = "https://api.twitter.com/2/users/{0}?user.fields=public_metrics";
                    var publicMetricsUrl = string.Format(publicMetricsApi, twitterUserId);

                    // Create Web Request
                    var publicMetricsHeaderFormat = "{0} {1}";
                    HttpWebRequest publicMetricsRequest = (HttpWebRequest)WebRequest.Create(publicMetricsUrl);
                    publicMetricsRequest.Headers.Add("Authorization", string.Format(publicMetricsHeaderFormat, type, socialNetwork.AccessToken));
                    publicMetricsRequest.Method = "Get";

                    // Get Web Response                                        
                    WebResponse publicMetricsResponse = publicMetricsRequest.GetResponse();

                    // Get Public Metrics
                    var publicMetricsResult = string.Empty;
                    int twitterFollowers = 0;
                    int twitterFollowings = 0;
                    int tweetsCount = 0;
                    if (publicMetricsResponse.ContentLength > 0)
                    {
                        using (var publicMetricsReader = new StreamReader(publicMetricsResponse.GetResponseStream()))
                        {
                            publicMetricsResult = publicMetricsReader.ReadToEnd();
                        }
                        var twiterModel = JsonConvert.DeserializeObject<TwitterModel>(publicMetricsResult);

                        // Followers Count
                        twitterFollowers = twiterModel.data.public_metrics.followers_count;

                        // Followings Count
                        twitterFollowings = twiterModel.data.public_metrics.following_count;

                        // Tweets Count
                        tweetsCount = twiterModel.data.public_metrics.tweet_count;
                    }
                    else
                    {
                        throw new ApplicationException("The Response Public Metrics can not be null or empty");
                    }

                    // Insert in data
                    using (var scope = new UnitOfWork())
                    {
                        var twitterData = new TwitterData()
                        {
                            SocialNetworkId = socialNetwork.Id,
                            TotalFollowers = twitterFollowers,
                            TotalFollowings = twitterFollowings,
                            TotalTweets = tweetsCount,
                            CreatedOn = DateTime.Now,
                            ModifiedOn = DateTime.Now

                        };
                        await _twitterDataRepository.InsertAsync(twitterData);
                        scope.Commit();
                    }

                    //Logger Commnad
                    var loggerCommandCompleted = new CreateLoggerCommand()
                    {
                        LogLevel = LogLevel.Information.ToString(),
                        ShortMessage = ErrorModel.ShortMessageTwitterCompletedProcessing,
                        ExceptionMessage = string.Empty,
                        CustomerId = companyId,
                        CreatedOn = DateTime.Now

                    };
                    await this._eventBus.SendCommand(loggerCommandCompleted);

                }
                catch (Exception exception)
                {
                    var loggerCommandError = new CreateLoggerCommand()
                    {
                        LogLevel = LogLevel.Error.ToString(),
                        ShortMessage = ErrorModel.ShortMessageTwitter,
                        ExceptionMessage = exception.ToString(),
                        CustomerId = companyId,
                        CreatedOn = DateTime.Now

                    };
                    await this._eventBus.SendCommand(loggerCommandError);
                }

            }


        }

        /// <summary>
        /// ProcessingLinkedinApi
        /// </summary>
        public async Task ProcessingLinkedInApi()
        {
            var companyId = string.Empty;

            var socialNetworks = await _socialNetworkRepository.FindAsync(x => x.NetworkType == (int)SocialNetworkEnum.LinkedIn);
            foreach (var socialNetwork in socialNetworks)
            {

                try
                {
                    //Processing Twitter Data
                    companyId = socialNetwork.ClientId;

                    // Linkedin Api for Followers Count
                    var appId = socialNetwork.AppId;
                    var followersCountApi = "https://api.linkedin.com/v2/networkSizes/urn:li:organization:{0}?edgeType=CompanyFollowedByMember";
                    var followersCountUrl = string.Format(followersCountApi, appId);

                    // Create WebRequest
                    var type = "Bearer";
                    HttpWebRequest followersCountRequest = (HttpWebRequest)WebRequest.Create(followersCountUrl);
                    var followersCountHeaderFormat = "{0} {1}";
                    followersCountRequest.Headers.Add("Authorization", string.Format(followersCountHeaderFormat, type, socialNetwork.AccessToken));
                    followersCountRequest.Method = "Get";

                    // Get Web Response
                    WebResponse followersCountResponse = followersCountRequest.GetResponse();

                    // Get Followers Count
                    var followersCountString = string.Empty;
                    var followersCountResult = string.Empty;
                    Dictionary<string, string> parts = new Dictionary<string, string>();
                    if (followersCountResponse.ContentLength > 0)
                    {
                        using (var reader = new StreamReader(followersCountResponse.GetResponseStream()))
                        {
                            followersCountResult = reader.ReadToEnd();

                        }
                        string[] followersCountList = followersCountResult.Split(',');

                        if (followersCountList.Length > 0)
                        {
                            parts.Add(followersCountList[0].Substring(0, followersCountList[0].IndexOf(":")),
                                 followersCountList[0].Substring(followersCountList[0].IndexOf(":") + 1, followersCountList[0].Length - followersCountList[0].IndexOf(":") - 1));

                        }
                        else
                        {
                            throw new ApplicationException("The list can not be null or empty");
                        }
                    }
                    else
                    {
                        throw new ApplicationException("The Web Response can not be null or empty");
                    }

                    // Followers Count
                    followersCountString = parts["{\"firstDegreeSize\""];
                    followersCountString = followersCountString.Remove(followersCountString.Length - 1);

                    int followersCount = Int32.Parse(followersCountString);

                    //Insert in data
                    using (var scope = new UnitOfWork())
                    {
                        var linkedinData = new LinkedInData()
                        {
                            SocialNetworkId = socialNetwork.Id,
                            TotalFollowers = followersCount,
                            CreatedOn = DateTime.Now,
                            ModifiedOn = DateTime.Now

                        };
                        await _linkedinDataRepository.InsertAsync(linkedinData);
                        scope.Commit();
                    }

                    //Logger Commnad

                    var loggerCommandCompleted = new CreateLoggerCommand()
                    {
                        LogLevel = LogLevel.Information.ToString(),
                        ShortMessage = ErrorModel.ShortMessageLinkedInStartProcessing,
                        ExceptionMessage = string.Empty,
                        CustomerId = companyId,
                        CreatedOn = DateTime.Now

                    };
                    await this._eventBus.SendCommand(loggerCommandCompleted);
                }
                catch (Exception exception)
                {
                    var loggerCommandError = new CreateLoggerCommand()
                    {
                        LogLevel = LogLevel.Error.ToString(),
                        ShortMessage = ErrorModel.ShortMessageLinkeInCompletedProcessing,
                        ExceptionMessage = exception.ToString(),
                        CustomerId = companyId,
                        CreatedOn = DateTime.Now

                    };
                    await this._eventBus.SendCommand(loggerCommandError);
                }
            }
        }

        /// <summary>
        /// ProcessingAdPointer
        /// </summary>
        public async Task ProcessingAdPointer()
        {
            try
            {
                await this.GetAdPointerIndustry();
                await this.GetAdPointerBrands();
                await this.GetAdPointerCompany();
                await this.GetAdPointerChannel();
                await this.GetAdPointerAd(0);
                await this.GetAdPointerClient(0);
                await this.GetAdPointerProgram();
                await this.GetAdPointerSearchFilter();
                await this.GetAdPointerAdEvent(0);
            }
            catch (Exception exception)
            {
                var loggerCommandError = new CreateLoggerCommand()
                {
                    LogLevel = LogLevel.Error.ToString(),
                    ShortMessage = ErrorModel.ErrorMessageAdPointer,
                    ExceptionMessage = exception.ToString(),
                    CustomerId = null,
                    CreatedOn = DateTime.Now

                };
                await this._eventBus.SendCommand(loggerCommandError);
            }


        }

        /// <summary>
        /// ProcessingGoogleApi
        /// </summary>
        public async Task ProcessingGoogleApi()
        {
            var companyId = string.Empty;
            var socialNetworks = await _socialNetworkRepository.FindAsync(x => x.NetworkType == (int)SocialNetworkEnum.GoogleAnalytic);
            foreach (var socialNetwork in socialNetworks)
            {

                try
                {
                    var users = string.Empty;
                    var newusers = string.Empty;
                    var pageviews = string.Empty;
                    var sessions = string.Empty;
                    var sessionsPerUser = string.Empty;
                    var pageviewsPerSession = string.Empty;
                    var avgSessionDuration = string.Empty;
                    var bounceRate = string.Empty;
                    Row[] usersCountries;
                    Row[] usersCities;
                    Row[] usersGenders;
                    Row[] usersAges;
                    Row[] usersDeviceCategory;
                    Row[] acquisitionTrafficChannel;
                    var UsersFromDesktop = string.Empty;
                    var NewUsersFromDesktop = string.Empty;
                    var UsersFromMobile = string.Empty;
                    var NewUsersFromMobile = string.Empty;
                    var directAcquisitionTrafficChannel = "0";
                    var organicSearchAcquisitionTrafficChannel = "0";
                    var referralAcquisitionTrafficChannel = "0";
                    var socialAcquisitionTrafficChannel = "0";

                    var startDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    var endDate = DateTime.Now.ToString("yyyy-MM-dd");
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://analyticsreporting.googleapis.com/v4/reports:batchGet");
                    httpWebRequest.ContentType = "application/json; CHARSET=UTF-8";
                    httpWebRequest.Method = "POST";
                    httpWebRequest.Headers.Add("Authorization", "Bearer " + socialNetwork.AccessToken);
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = "{" +
                    "\"reportRequests\": [{" +
                        "\"viewId\":'" + socialNetwork.ViewScreen + "'," +
                        "\"dateRanges\" : [{\"" +
                         "startDate\" :'" + startDate + "' ," +
                         "\"endDate\" : '" + endDate + "'" +
                    "}]," +
                    "\"metrics\":[" +
                    "{\"expression\":'ga:users'}," +
                    "{\"expression\":'ga:newusers'}," +
                    "{\"expression\":'ga:pageviews'}," +
                    "{\"expression\":'ga:sessions'}," +
                    "{\"expression\":'ga:sessionsPerUser'}," +
                    "{\"expression\":'ga:pageviewsPerSession'}," +
                    "{\"expression\":'ga:avgSessionDuration'}," +
                    "{\"expression\":'ga:bounceRate'}" +
                      "]" +
                "}]}";
                        streamWriter.Write(json);
                    }

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var googleModel = JsonConvert.DeserializeObject<GoogleDataModel>(result);
                        users = googleModel.reports[0].data.totals[0].values[0];
                        newusers = googleModel.reports[0].data.totals[0].values[1];
                        pageviews = googleModel.reports[0].data.totals[0].values[2];
                        sessions = googleModel.reports[0].data.totals[0].values[3];
                        sessionsPerUser = googleModel.reports[0].data.totals[0].values[4];
                        pageviewsPerSession = googleModel.reports[0].data.totals[0].values[5];
                        avgSessionDuration = googleModel.reports[0].data.totals[0].values[6];
                        bounceRate = googleModel.reports[0].data.totals[0].values[7];
                    }


                    //Country
                    var httpWebRequestForCountry = (HttpWebRequest)WebRequest.Create("https://analyticsreporting.googleapis.com/v4/reports:batchGet");
                    httpWebRequestForCountry.ContentType = "application/json; CHARSET=UTF-8";
                    httpWebRequestForCountry.Method = "POST";
                    httpWebRequestForCountry.Headers.Add("Authorization", "Bearer " + socialNetwork.AccessToken);
                    using (var streamWriter = new StreamWriter(httpWebRequestForCountry.GetRequestStream()))
                    {
                        string json = "{" +
                    "\"reportRequests\": [{" +
                        "\"viewId\":'" + socialNetwork.ViewScreen + "'," +
                        "\"dateRanges\" : [{\"" +
                         "startDate\" :'" + startDate + "' ," +
                         "\"endDate\" : '" + endDate + "'" +
                    "}]," +
                    "\"metrics\":[" +
                    "{\"expression\":'ga:users'}," +
                    "{\"expression\":'ga:newusers'}" +
                      "]," +
                       "\"dimensions\":[" +
                         "{\"name\":'ga:country'}" +
                          "]" +
                        "}]}";
                        streamWriter.Write(json);
                    }
                    var httpResponseForCountry = (HttpWebResponse)httpWebRequestForCountry.GetResponse();

                    using (var streamReader = new StreamReader(httpResponseForCountry.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var googleModel = JsonConvert.DeserializeObject<GoogleDataModel>(result);
                        usersCountries = googleModel.reports[0].data.rows;

                    }

                    //City
                    var httpWebRequestForCity = (HttpWebRequest)WebRequest.Create("https://analyticsreporting.googleapis.com/v4/reports:batchGet");
                    httpWebRequestForCity.ContentType = "application/json; CHARSET=UTF-8";
                    httpWebRequestForCity.Method = "POST";
                    httpWebRequestForCity.Headers.Add("Authorization", "Bearer " + socialNetwork.AccessToken);
                    using (var streamWriter = new StreamWriter(httpWebRequestForCity.GetRequestStream()))
                    {
                        string json = "{" +
                    "\"reportRequests\": [{" +
                        "\"viewId\":'" + socialNetwork.ViewScreen + "'," +
                        "\"dateRanges\" : [{\"" +
                         "startDate\" :'" + startDate + "' ," +
                         "\"endDate\" : '" + endDate + "'" +
                    "}]," +
                    "\"metrics\":[" +
                    "{\"expression\":'ga:users'}," +
                    "{\"expression\":'ga:newusers'}" +
                      "]," +
                       "\"dimensions\":[" +
                         "{\"name\":'ga:city'}" +
                          "]" +
                        "}]}";
                        streamWriter.Write(json);
                    }
                    var httpResponseForCity = (HttpWebResponse)httpWebRequestForCity.GetResponse();

                    using (var streamReader = new StreamReader(httpResponseForCity.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var googleModel = JsonConvert.DeserializeObject<GoogleDataModel>(result);
                        usersCities = googleModel.reports[0].data.rows;

                    }

                    //Gender
                    var httpWebRequestForGender = (HttpWebRequest)WebRequest.Create("https://analyticsreporting.googleapis.com/v4/reports:batchGet");
                    httpWebRequestForGender.ContentType = "application/json; CHARSET=UTF-8";
                    httpWebRequestForGender.Method = "POST";
                    httpWebRequestForGender.Headers.Add("Authorization", "Bearer " + socialNetwork.AccessToken);
                    using (var streamWriter = new StreamWriter(httpWebRequestForGender.GetRequestStream()))
                    {
                        string json = "{" +
                    "\"reportRequests\": [{" +
                        "\"viewId\":'" + socialNetwork.ViewScreen + "'," +
                        "\"dateRanges\" : [{\"" +
                         "startDate\" :'" + startDate + "' ," +
                         "\"endDate\" : '" + endDate + "'" +
                    "}]," +
                    "\"metrics\":[" +
                    "{\"expression\":'ga:users'}," +
                    "{\"expression\":'ga:newusers'}" +
                      "]," +
                       "\"dimensions\":[" +
                         "{\"name\":'ga:userGender'}" +
                          "]" +
                        "}]}";
                        streamWriter.Write(json);
                    }
                    var httpResponseForGender = (HttpWebResponse)httpWebRequestForGender.GetResponse();

                    using (var streamReader = new StreamReader(httpResponseForGender.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var googleModel = JsonConvert.DeserializeObject<GoogleDataModel>(result);
                        usersGenders = googleModel.reports[0].data.rows;

                    }

                    //Age
                    var httpWebRequestForAge = (HttpWebRequest)WebRequest.Create("https://analyticsreporting.googleapis.com/v4/reports:batchGet");
                    httpWebRequestForAge.ContentType = "application/json; CHARSET=UTF-8";
                    httpWebRequestForAge.Method = "POST";
                    httpWebRequestForAge.Headers.Add("Authorization", "Bearer " + socialNetwork.AccessToken);
                    using (var streamWriter = new StreamWriter(httpWebRequestForAge.GetRequestStream()))
                    {
                        string json = "{" +
                    "\"reportRequests\": [{" +
                        "\"viewId\":'" + socialNetwork.ViewScreen + "'," +
                        "\"dateRanges\" : [{\"" +
                         "startDate\" :'" + startDate + "' ," +
                         "\"endDate\" : '" + endDate + "'" +
                    "}]," +
                    "\"metrics\":[" +
                    "{\"expression\":'ga:users'}," +
                    "{\"expression\":'ga:newusers'}" +
                      "]," +
                       "\"dimensions\":[" +
                         "{\"name\":'ga:userAgeBracket'}" +
                          "]" +
                        "}]}";
                        streamWriter.Write(json);
                    }
                    var httpResponseForAge = (HttpWebResponse)httpWebRequestForAge.GetResponse();

                    using (var streamReader = new StreamReader(httpResponseForAge.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var googleModel = JsonConvert.DeserializeObject<GoogleDataModel>(result);
                        usersAges = googleModel.reports[0].data.rows;

                    }

                    //Device Category
                    var httpWebRequestForDeviceCategory = (HttpWebRequest)WebRequest.Create("https://analyticsreporting.googleapis.com/v4/reports:batchGet");
                    httpWebRequestForDeviceCategory.ContentType = "application/json; CHARSET=UTF-8";
                    httpWebRequestForDeviceCategory.Method = "POST";
                    httpWebRequestForDeviceCategory.Headers.Add("Authorization", "Bearer " + socialNetwork.AccessToken);
                    using (var streamWriter = new StreamWriter(httpWebRequestForDeviceCategory.GetRequestStream()))
                    {
                        string json = "{" +
                    "\"reportRequests\": [{" +
                        "\"viewId\":'" + socialNetwork.ViewScreen + "'," +
                        "\"dateRanges\" : [{\"" +
                         "startDate\" :'" + startDate + "' ," +
                         "\"endDate\" : '" + endDate + "'" +
                    "}]," +
                    "\"metrics\":[" +
                    "{\"expression\":'ga:users'}," +
                    "{\"expression\":'ga:newusers'}" +
                      "]," +
                       "\"dimensions\":[" +
                         "{\"name\":'ga:deviceCategory'}" +
                          "]" +
                        "}]}";
                        streamWriter.Write(json);
                    }
                    var httpResponseForDeviceCategory = (HttpWebResponse)httpWebRequestForDeviceCategory.GetResponse();

                    using (var streamReader = new StreamReader(httpResponseForDeviceCategory.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var googleModel = JsonConvert.DeserializeObject<GoogleDataModel>(result);
                        usersDeviceCategory = googleModel.reports[0].data.rows;

                    }
                    if (usersDeviceCategory != null)
                    {
                        UsersFromDesktop = usersDeviceCategory[0].metrics[0].values[0];
                        NewUsersFromDesktop = usersDeviceCategory[0].metrics[0].values[1];
                        UsersFromMobile = usersDeviceCategory[1].metrics[0].values[0];
                        NewUsersFromMobile = usersDeviceCategory[1].metrics[0].values[1];
                    }

                    //Acquisition Traffic Channel 
                    var httpWebRequestForAcquisitionTrafficChannel = (HttpWebRequest)WebRequest.Create("https://analyticsreporting.googleapis.com/v4/reports:batchGet");
                    httpWebRequestForAcquisitionTrafficChannel.ContentType = "application/json; CHARSET=UTF-8";
                    httpWebRequestForAcquisitionTrafficChannel.Method = "POST";
                    httpWebRequestForAcquisitionTrafficChannel.Headers.Add("Authorization", "Bearer " + socialNetwork.AccessToken);
                    using (var streamWriter = new StreamWriter(httpWebRequestForAcquisitionTrafficChannel.GetRequestStream()))
                    {
                        string json = "{" +
                    "\"reportRequests\": [{" +
                        "\"viewId\":'" + socialNetwork.ViewScreen + "'," +
                        "\"dateRanges\" : [{\"" +
                         "startDate\" :'" + startDate + "' ," +
                         "\"endDate\" : '" + endDate + "'" +
                    "}]," +
                       "\"dimensions\":[" +
                         "{\"name\":'ga:acquisitionTrafficChannel'}" +
                          "]" +
                        "}]}";
                        streamWriter.Write(json);
                    }
                    var httpResponseForAcquisitionTrafficChannel = (HttpWebResponse)httpWebRequestForAcquisitionTrafficChannel.GetResponse();

                    using (var streamReader = new StreamReader(httpResponseForAcquisitionTrafficChannel.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var googleModel = JsonConvert.DeserializeObject<GoogleDataModel>(result);
                        acquisitionTrafficChannel = googleModel.reports[0].data.rows;

                    }
                    if (acquisitionTrafficChannel != null)
                    {
                        if (acquisitionTrafficChannel.Length > 0)
                        {
                            directAcquisitionTrafficChannel = acquisitionTrafficChannel[0].metrics[0].values[0];
                        }
                        if (acquisitionTrafficChannel.Length > 1)
                        {
                            organicSearchAcquisitionTrafficChannel = acquisitionTrafficChannel[1].metrics[0].values[0];
                        }
                        if (acquisitionTrafficChannel.Length > 2)
                        {
                            referralAcquisitionTrafficChannel = acquisitionTrafficChannel[2].metrics[0].values[0];
                        }
                        if (acquisitionTrafficChannel.Length > 3)
                        {
                            socialAcquisitionTrafficChannel = acquisitionTrafficChannel[3].metrics[0].values[0];
                        }

                    }

                    //Insert in data
                    using (var scope = new UnitOfWork())
                    {
                        var googleData = new GoogleData()
                        {
                            SocialNetworkId = socialNetwork.Id,
                            Users = Convert.ToInt32(users),
                            NewUsers = Convert.ToInt32(newusers),
                            PageViews = Convert.ToInt32(pageviews),
                            Sessions = Convert.ToInt32(sessions),
                            SessionsPerUser = Convert.ToDouble(sessionsPerUser),
                            PageViewPerSession = Convert.ToDouble(pageviewsPerSession),
                            AvgSessionDuration = Convert.ToDouble(avgSessionDuration),
                            BounceRate = Convert.ToDouble(bounceRate),
                            UsersFromDesktop = Convert.ToInt32(UsersFromDesktop),
                            NewUsersFromDesktop = Convert.ToInt32(NewUsersFromDesktop),
                            UsersFromMobile = Convert.ToInt32(UsersFromMobile),
                            NewUsersFromMobile = Convert.ToInt32(NewUsersFromMobile),
                            DirectAcquisitionTrafficChannel = Convert.ToInt32(directAcquisitionTrafficChannel),
                            OrganicSearchAcquisitionTrafficChannel = Convert.ToInt32(organicSearchAcquisitionTrafficChannel),
                            ReferralAcquisitionTrafficChannel = Convert.ToInt32(referralAcquisitionTrafficChannel),
                            SocialAcquisitionTrafficChannel = Convert.ToInt32(socialAcquisitionTrafficChannel),
                            CreatedOn = DateTime.Now,
                            ModifiedOn = DateTime.Now

                        };
                        await _googleDataRepository.InsertAsync(googleData);

                        if (usersCountries != null)
                        {
                            foreach (var usersCountry in usersCountries)
                            {
                                var googleDataCountry = new GoogleDataCountry()
                                {
                                    GoogleDataId = googleData.Id,
                                    Country = usersCountry.dimensions[0],
                                    Users = Convert.ToInt32(usersCountry.metrics[0].values[0]),
                                    NewUsers = Convert.ToInt32(usersCountry.metrics[0].values[1]),
                                };
                                await _googleDataCountryRepository.InsertAsync(googleDataCountry);
                            }
                        }

                        if (usersCities != null)
                        {
                            foreach (var usersCity in usersCities)
                            {
                                var googleDataCity = new GoogleDataCity()
                                {
                                    GoogleDataId = googleData.Id,
                                    City = usersCity.dimensions[0],
                                    Users = Convert.ToInt32(usersCity.metrics[0].values[0]),
                                    NewUsers = Convert.ToInt32(usersCity.metrics[0].values[1]),
                                };
                                await _googleDataCityRepository.InsertAsync(googleDataCity);
                            }
                        }

                        if (usersGenders != null)
                        {
                            foreach (var usersGender in usersGenders)
                            {
                                var googleDataGender = new GoogleDataGender()
                                {
                                    GoogleDataId = googleData.Id,
                                    Gender = usersGender.dimensions[0],
                                    Users = Convert.ToInt32(usersGender.metrics[0].values[0]),
                                    NewUsers = Convert.ToInt32(usersGender.metrics[0].values[1]),
                                };
                                await _googleDataGenderRepository.InsertAsync(googleDataGender);
                            }
                        }

                        if (usersAges != null)
                        {
                            foreach (var usersAge in usersAges)
                            {
                                var googleDataAge = new GoogleDataAge()
                                {
                                    GoogleDataId = googleData.Id,
                                    Age = usersAge.dimensions[0],
                                    Users = Convert.ToInt32(usersAge.metrics[0].values[0]),
                                    NewUsers = Convert.ToInt32(usersAge.metrics[0].values[1]),
                                };
                                await _googleDataAgeRepository.InsertAsync(googleDataAge);
                            }
                        }
                        scope.Commit();
                    }

                    //Logger Commnad

                    var loggerCommandCompleted = new CreateLoggerCommand()
                    {
                        LogLevel = LogLevel.Information.ToString(),
                        ShortMessage = ErrorModel.ShortMessageGoogleCompletedProcessing,
                        ExceptionMessage = string.Empty,
                        CustomerId = companyId,
                        CreatedOn = DateTime.Now

                    };
                    await this._eventBus.SendCommand(loggerCommandCompleted);

                }
                catch (Exception exception)
                {
                    var loggerCommandError = new CreateLoggerCommand()
                    {
                        LogLevel = LogLevel.Error.ToString(),
                        ShortMessage = ErrorModel.ShortMessageGoogle,
                        ExceptionMessage = exception.ToString(),
                        CustomerId = companyId,
                        CreatedOn = DateTime.Now

                    };
                    await this._eventBus.SendCommand(loggerCommandError);
                }
            }
        }


        public async Task ProcessingYoutubeApi()
        {
            var companyId = string.Empty;
            var socialNetworks = await _socialNetworkRepository.FindAsync(x => x.NetworkType == (int)SocialNetworkEnum.Youtube);
            foreach (var socialNetwork in socialNetworks)
            {
                try
                {
                    var youtubeDataModel = new YoutubeDataModel();
                    string url = "https://youtubeanalytics.googleapis.com/v2/reports?metrics=views,likes,comments,subscribersGained&ids=channel==MINE&startDate=2021-08-03&endDate=2021-08-21&&key=" + socialNetwork.AppKey + "&access_token=" + socialNetwork.AccessToken + "";

                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpWebRequest.Method = "GET";

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        youtubeDataModel = JsonConvert.DeserializeObject<YoutubeDataModel>(result);
                    }

                    using (var scope = new UnitOfWork())
                    {
                        var youtubeData = new YoutubeData()
                        {
                            SocialNetworkId = socialNetwork.Id,
                            Views = youtubeDataModel.rows[0][0],
                            Likes = youtubeDataModel.rows[0][1],
                            Comments = youtubeDataModel.rows[0][2],
                            Subscribers = youtubeDataModel.rows[0][3],
                            CreatedOn = DateTime.Now,
                            ModifiedOn = DateTime.Now

                        };
                        await _youtubeDataRepository.InsertAsync(youtubeData);

                        scope.Commit();
                    }

                    var loggerCommandCompleted = new CreateLoggerCommand()
                    {
                        LogLevel = LogLevel.Information.ToString(),
                        ShortMessage = ErrorModel.ShortMessageYoutubeCompletedProcessing,
                        ExceptionMessage = string.Empty,
                        CustomerId = socialNetwork.ClientId,
                        CreatedOn = DateTime.Now

                    };
                    await this._eventBus.SendCommand(loggerCommandCompleted);
                }
                catch (Exception exception)
                {
                    var loggerCommandError = new CreateLoggerCommand()
                    {
                        LogLevel = LogLevel.Error.ToString(),
                        ShortMessage = ErrorModel.ShortMessageYoutube,
                        ExceptionMessage = exception.ToString(),
                        CustomerId = companyId,
                        CreatedOn = DateTime.Now

                    };
                    await this._eventBus.SendCommand(loggerCommandError);

                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// GetAccessTokenMgmt
        /// </summary>
        /// <returns></returns>
        private async Task<AuthTokenModel> GetAccessTokenMgmt()
        {
            AuthTokenModel authTokenModel;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://development.ad-pointer.com/mgmt-api/v1/auth/token");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"email\":\"" + _configuration["MGMT:username"] + "\"," + "\"password\":\"" + _configuration["MGMT:password"] + "\"}";

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                authTokenModel = JsonConvert.DeserializeObject<AuthTokenModel>(result);
            }

            return authTokenModel;
        }

        /// <summary>
        /// GetAccessTokenEdge
        /// </summary>
        /// <returns></returns>
        private async Task<AuthTokenModel> GetAccessTokenEdge()
        {
            string result = string.Empty;
            AuthTokenModel authTokenModel;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://development.ad-pointer.com/edge-api/v1/auth/token");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"email\":\"" + _configuration["EDGE:username"] + "\"," + "\"password\":\"" + _configuration["EDGE:password"] + "\"}";

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
                authTokenModel = JsonConvert.DeserializeObject<AuthTokenModel>(result);
            }


            return authTokenModel;
        }

        /// <summary>
        /// GetAdPointerIndustry
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task GetAdPointerIndustry()
        {
            var tokenModel = await this.GetAccessTokenMgmt();
            if (string.IsNullOrEmpty(tokenModel.accessToken))
            {
                throw new ApplicationException("The Token from MGMT end point can not be null or empty");
            }

            var industryModels = new List<IndustryModel>();
            var requestUrl = "https://development.ad-pointer.com/mgmt-api/v1/industry/all";


            // Create Web Request 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Headers.Add("Authorization", string.Format("{0} {1}", tokenModel.tokenType, tokenModel.accessToken));
            request.Method = "Get";

            // Get Web Response
            WebResponse response = request.GetResponse();

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var result = reader.ReadToEnd();
                industryModels = JsonConvert.DeserializeObject<List<IndustryModel>>(result);
            }

            using (var scope = new UnitOfWork())
            {
                var industries = _industryRepository.GetAllAsync().Result.ToList();
                foreach (var industry in industries)
                {
                    await _industryRepository.DeleteAsync(industry);
                }
                foreach (var industryModel in industryModels)
                {
                    var industry = new Industry()
                    {
                        Id = industryModel.id,
                        Name = industryModel.name,
                        Version = industryModel.version,
                        CreatedOn = Convert.ToDateTime(industryModel.createdOn),
                        ModifiedOn = Convert.ToDateTime(industryModel.modifiedOn)
                    };
                    await _industryRepository.InsertAsync(industry);
                }

                scope.Commit();
            }
            var loggerCommandCompleted = new CreateLoggerCommand()
            {
                LogLevel = LogLevel.Information.ToString(),
                ShortMessage = ErrorModel.MessageAdPointerIndustryCompleted,
                ExceptionMessage = string.Empty,
                CustomerId = null,
                CreatedOn = DateTime.Now

            };
            await this._eventBus.SendCommand(loggerCommandCompleted);

        }

        private async Task GetAdPointerBrands()
        {
            var tokenModel = await this.GetAccessTokenMgmt();
            if (string.IsNullOrEmpty(tokenModel.accessToken))
            {
                throw new ApplicationException("The Token from MGMT end point can not be null or empty");
            }

            var brandModels = new List<BrandModel>();
            var requestUrl = "https://development.ad-pointer.com/mgmt-api/v1/brand/list";


            // Create Web Request 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Headers.Add("Authorization", string.Format("{0} {1}", tokenModel.tokenType, tokenModel.accessToken));
            request.Method = "Get";

            // Get Web Response
            WebResponse response = request.GetResponse();

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var result = reader.ReadToEnd();
                brandModels = JsonConvert.DeserializeObject<List<BrandModel>>(result);
            }

            using (var scope = new UnitOfWork())
            {
                var brands = _brandRepository.GetAllAsync().Result.ToList();
                foreach (var brand in brands)
                {
                    await _brandRepository.DeleteAsync(brand);
                }
                foreach (var brandModel in brandModels)
                {
                    var brand = new Brand()
                    {
                        Id = brandModel.id,
                        Name = brandModel.name,
                        Version = brandModel.version,
                        CompanyId = brandModel.companyId,
                        CreatedOn = Convert.ToDateTime(brandModel.createdOn),
                        ModifiedOn = Convert.ToDateTime(brandModel.modifiedOn)
                    };
                    await _brandRepository.InsertAsync(brand);
                }

                scope.Commit();
            }
            var loggerCommandCompleted = new CreateLoggerCommand()
            {
                LogLevel = LogLevel.Information.ToString(),
                ShortMessage = ErrorModel.MessageAdPointerBrandCompleted,
                ExceptionMessage = string.Empty,
                CustomerId = null,
                CreatedOn = DateTime.Now

            };
            await this._eventBus.SendCommand(loggerCommandCompleted);

        }

        /// <summary>
        /// GetAdPointerCompany
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task GetAdPointerCompany()
        {
            var tokenModel = await this.GetAccessTokenMgmt();
            if (string.IsNullOrEmpty(tokenModel.accessToken))
            {
                throw new ApplicationException("The Token from MGMT end point can not be null or empty");
            }
            var companyModels = new List<CompanyModel>();
            var requestUrl = "https://development.ad-pointer.com/mgmt-api/v1/company/all";


            // Create Web Request 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Headers.Add("Authorization", string.Format("{0} {1}", tokenModel.tokenType, tokenModel.accessToken));
            request.Method = "Get";

            // Get Web Response
            WebResponse response = request.GetResponse();

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var result = reader.ReadToEnd();
                companyModels = JsonConvert.DeserializeObject<List<CompanyModel>>(result);
            }

            using (var scope = new UnitOfWork())
            {
                var companies = _companyRepository.GetAllAsync().Result.ToList();
                foreach (var company in companies)
                {
                    await _companyRepository.DeleteAsync(company);
                }
                foreach (var companyModel in companyModels)
                {
                    var company = new Company()
                    {
                        Id = companyModel.id,
                        Name = companyModel.name,
                        Version = companyModel.version,
                        CreatedOn = Convert.ToDateTime(companyModel.createdOn),
                        ModifiedOn = Convert.ToDateTime(companyModel.modifiedOn),
                        IndustryId = companyModel.industryId
                    };
                    await _companyRepository.InsertAsync(company);
                }

                scope.Commit();
            }
            var loggerCommandCompleted = new CreateLoggerCommand()
            {
                LogLevel = LogLevel.Information.ToString(),
                ShortMessage = ErrorModel.MessageAdPointerCompanyCompleted,
                ExceptionMessage = string.Empty,
                CustomerId = null,
                CreatedOn = DateTime.Now

            };
            await this._eventBus.SendCommand(loggerCommandCompleted);

        }

        /// <summary>
        /// GetAdPointerChannel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task GetAdPointerChannel()
        {
            var tokenModel = await this.GetAccessTokenMgmt();
            var tokenModelEdge = await this.GetAccessTokenEdge();
            if (string.IsNullOrEmpty(tokenModel.accessToken))
            {
                throw new ApplicationException("The Token from MGMT end point can not be null or empty");
            }
            var channelModels = new List<ChannelModel>();
            var requestUrl = "https://development.ad-pointer.com/mgmt-api/v1/channel/list";


            // Create Web Request 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Headers.Add("Authorization", string.Format("{0} {1}", tokenModel.tokenType, tokenModel.accessToken));
            request.Method = "Get";

            // Get Web Response
            WebResponse response = request.GetResponse();

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var result = reader.ReadToEnd();
                channelModels = JsonConvert.DeserializeObject<List<ChannelModel>>(result);
            }

            using (var scope = new UnitOfWork())
            {
                var channels = _channelRepository.GetAllAsync().Result.ToList();
                foreach (var channel in channels)
                {
                    await _channelRepository.DeleteAsync(channel);
                }
                foreach (var channelModel in channelModels)
                {
                    var channel = new Channel()
                    {
                        Id = channelModel.id,
                        Name = channelModel.name,
                        Version = channelModel.version,
                        CreatedOn = Convert.ToDateTime(channelModel.createdOn),
                        ModifiedOn = Convert.ToDateTime(channelModel.modifiedOn),
                        ExternalId = channelModel.externalId,
                        LogoPath = channelModel.logoPath
                    };
                    await _channelRepository.InsertAsync(channel);
                }

                scope.Commit();
            }
            var loggerCommandCompleted = new CreateLoggerCommand()
            {
                LogLevel = LogLevel.Information.ToString(),
                ShortMessage = ErrorModel.MessageAdPointerChannelCompleted,
                ExceptionMessage = string.Empty,
                CustomerId = null,
                CreatedOn = DateTime.Now

            };
            await this._eventBus.SendCommand(loggerCommandCompleted);

        }

        /// <summary>
        /// GetAdPointerAd
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task GetAdPointerAd(int page)
        {
            var tokenModel = await this.GetAccessTokenMgmt();
            if (string.IsNullOrEmpty(tokenModel.accessToken))
            {
                throw new ApplicationException("The Token from MGMT end point can not be null or empty");
            }
            var adModel = new AdModel();
            var requestUrl = "https://development.ad-pointer.com/mgmt-api/v1/ad/list/defined?page=" + page + "&size=500";


            // Create Web Request 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Headers.Add("Authorization", string.Format("{0} {1}", tokenModel.tokenType, tokenModel.accessToken));
            request.Method = "Get";

            // Get Web Response
            WebResponse response = request.GetResponse();

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var result = reader.ReadToEnd();
                adModel = JsonConvert.DeserializeObject<AdModel>(result);
            }

            using (var scope = new UnitOfWork())
            {
                var ads = _adRepository.GetAllAsync().Result.ToList();
                await _adRepository.DeleteAllAsync(ads);
                scope.Commit();
            }

            while (adModel.totalPages > page)
            {
                await GetAdPointerAdByPage(page);
                page = page + 1;
            }

            var loggerCommandCompleted = new CreateLoggerCommand()
            {
                LogLevel = LogLevel.Information.ToString(),
                ShortMessage = ErrorModel.MessageAdPointerAdCompleted,
                ExceptionMessage = string.Empty,
                CustomerId = null,
                CreatedOn = DateTime.Now

            };
            await this._eventBus.SendCommand(loggerCommandCompleted);

        }

        /// <summary>
        /// Gets the ad pointer ad by page.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="page">The page.</param>
        private async Task GetAdPointerAdByPage(int page)
        {
            var tokenModel = await this.GetAccessTokenMgmt();

            if (string.IsNullOrEmpty(tokenModel.accessToken))
            {
                throw new ApplicationException("The Token from MGMT end point can not be null or empty");
            }
            var adModel = new AdModel();
            var requestUrl = "https://development.ad-pointer.com/mgmt-api/v1/ad/list/defined?page=" + page + "&size=500";


            // Create Web Request 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Headers.Add("Authorization", string.Format("{0} {1}", tokenModel.tokenType, tokenModel.accessToken));
            request.Method = "Get";

            // Get Web Response
            WebResponse response = request.GetResponse();

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var result = reader.ReadToEnd();
                adModel = JsonConvert.DeserializeObject<AdModel>(result);
            }

            using (var scope = new UnitOfWork())
            {
                foreach (var adContent in adModel.content)
                {
                    var newAd = new Ad()
                    {
                        Id = adContent.id,
                        Name = adContent.name,
                        IsDefined = adContent.isDefined,
                        BrandId = adContent.brand.id,
                        CompanyId = adContent.company.id,
                        IndustryId = adContent.industry.id,
                        ExternalId = adContent.externalId,
                        VideoURl = adContent.videoUrl,
                        VideoImgUrl = adContent.videoImgUrl

                    };
                    await _adRepository.InsertAsync(newAd);
                }

                scope.Commit();
            }

        }

        /// <summary>
        /// GetAdPointerAdEvent
        /// </summary>
        /// <param name="model"></param>
        /// <param name="page"></param>
        /// <param name="isForDelete"></param>
        /// <returns></returns>
        private async Task GetAdPointerAdEvent(int page)
        {
            var tokenModel = await this.GetAccessTokenMgmt();
            if (string.IsNullOrEmpty(tokenModel.accessToken))
            {
                throw new ApplicationException("The Token from MGMT end point can not be null or empty");
            }

            var adEventModel = new AdEventModel();
            var requestUrl = "https://development.ad-pointer.com/mgmt-api/v1/adevent/list?page=" + page + "&size=500";


            // Create Web Request 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Headers.Add("Authorization", string.Format("{0} {1}", tokenModel.tokenType, tokenModel.accessToken));
            request.Method = "Get";

            // Get Web Response
            WebResponse response = request.GetResponse();

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var result = reader.ReadToEnd();
                adEventModel = JsonConvert.DeserializeObject<AdEventModel>(result);
            }
            using (var scope = new UnitOfWork())
            {

                var adEvents = _adEventRepository.GetAllAsync().Result.ToList();
                await _adEventRepository.DeleteAllAsync(adEvents);

                scope.Commit();
            }


            while (adEventModel.totalPages > page)
            {
                await GetAdPointerAdEventByPage(page);
                page = page + 1;
            }

            var loggerCommandCompleted = new CreateLoggerCommand()
            {
                LogLevel = LogLevel.Information.ToString(),
                ShortMessage = ErrorModel.MessageAdPointerAdEventCompleted,
                ExceptionMessage = string.Empty,
                CustomerId = null,
                CreatedOn = DateTime.Now

            };
            await this._eventBus.SendCommand(loggerCommandCompleted);
        }

        private async Task GetAdPointerAdEventByPage(int page)
        {

            var tokenModel = await this.GetAccessTokenMgmt();

            if (string.IsNullOrEmpty(tokenModel.accessToken))
            {
                throw new ApplicationException("The Token from MGMT end point can not be null or empty");
            }

            var adEventModel = new AdEventModel();
            var requestUrl = "https://development.ad-pointer.com/mgmt-api/v1/adevent/list?page=" + page + "&size=500";


            // Create Web Request 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Headers.Add("Authorization", string.Format("{0} {1}", tokenModel.tokenType, tokenModel.accessToken));
            request.Method = "Get";

            // Get Web Response
            WebResponse response = request.GetResponse();

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var result = reader.ReadToEnd();
                adEventModel = JsonConvert.DeserializeObject<AdEventModel>(result);
            }
            using (var scope = new UnitOfWork())
            {
                foreach (var adEventContent in adEventModel.content)
                {
                    var newAdEvent = new AdEvent()
                    {
                        Id = adEventContent.id,
                        AdId = adEventContent.ad.id,
                        ChannelId = adEventContent.channel.id,
                        AdEventTime = Convert.ToDateTime(adEventContent.adEventTime),
                        AdStartEventTime = Convert.ToDateTime(adEventContent.adStartEventTime),
                        AdEndEventTime = Convert.ToDateTime(adEventContent.adEndEventTime),
                        ProgramId = adEventContent.programId,
                        ProgramName = adEventContent.programName,
                        ProgramGenre = adEventContent.programGenre,
                        ProgramPrice = adEventContent.programPrice,
                        ProgramImpressions = adEventContent.programImpressions,

                    };
                    await _adEventRepository.InsertAsync(newAdEvent);
                }
                scope.Commit();
            }
        }

        private async Task GetAdPointerClient(int page)
        {
            var tokenModel = await this.GetAccessTokenMgmt();
            if (string.IsNullOrEmpty(tokenModel.accessToken))
            {
                throw new ApplicationException("The Token from MGMT end point can not be null or empty");
            }
            var clientModel = new ClientModel();
            var requestUrl = "https://development.ad-pointer.com/mgmt-api/v1/user/list?page=" + page + "&size=500";


            // Create Web Request 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Headers.Add("Authorization", string.Format("{0} {1}", tokenModel.tokenType, tokenModel.accessToken));
            request.Method = "Get";

            // Get Web Response
            WebResponse response = request.GetResponse();

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var result = reader.ReadToEnd();
                clientModel = JsonConvert.DeserializeObject<ClientModel>(result);
            }

            using (var scope = new UnitOfWork())
            {
                var clients = _clientRepository.GetAllAsync().Result.ToList();
                await _clientRepository.DeleteAllAsync(clients);

                scope.Commit();
            }

            while (clientModel.totalPages > page)
            {
                await GetAdPointerClientByPage(page);
                page = page + 1;
            }

            var loggerCommandCompleted = new CreateLoggerCommand()
            {
                LogLevel = LogLevel.Information.ToString(),
                ShortMessage = ErrorModel.MessageAdPointerClientCompleted,
                ExceptionMessage = string.Empty,
                CustomerId = null,
                CreatedOn = DateTime.Now

            };
            await this._eventBus.SendCommand(loggerCommandCompleted);

        }

        private async Task GetAdPointerClientByPage(int page)
        {
            var tokenModel = await this.GetAccessTokenMgmt();
            if (string.IsNullOrEmpty(tokenModel.accessToken))
            {
                throw new ApplicationException("The Token from MGMT end point can not be null or empty");
            }

            var clientModel = new ClientModel();
            var requestUrl = "https://development.ad-pointer.com/mgmt-api/v1/user/list?page=" + page + "&size=500";


            // Create Web Request 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Headers.Add("Authorization", string.Format("{0} {1}", tokenModel.tokenType, tokenModel.accessToken));
            request.Method = "Get";

            // Get Web Response
            WebResponse response = request.GetResponse();

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var result = reader.ReadToEnd();
                clientModel = JsonConvert.DeserializeObject<ClientModel>(result);
            }

            using (var scope = new UnitOfWork())
            {
                foreach (var clientContent in clientModel.content)
                {
                    var newClient = new Client()
                    {
                        Id = clientContent.id,
                        Email = clientContent.email,
                        Role = GetEnumFromDescription(clientContent.role, typeof(RoleEnum)),
                        Enabled = clientContent.enabled,
                        Name = clientContent.name,
                        Phone = clientContent.phone,
                        CreatedOn = Convert.ToDateTime(clientContent.createdOn),
                        ModifiedOn = Convert.ToDateTime(clientContent.modifiedOn)

                    };
                    await _clientRepository.InsertAsync(newClient);
                }

                scope.Commit();
            }

        }

        private async Task GetAdPointerProgram()
        {
            var tokenModel = await this.GetAccessTokenMgmt();
            if (string.IsNullOrEmpty(tokenModel.accessToken))
            {
                throw new ApplicationException("The Token from MGMT end point can not be null or empty");
            }

            var channelIds = _channelRepository.GetAllAsync().Result.Select(x => x.ExternalId).ToArray();
            string channelIdStrings = string.Empty;
            foreach (var id in channelIds)
            {
                channelIdStrings = channelIdStrings + "," + id;
            }

            var startDateTime = (DateTime.Now.Date).ToString("yyyy-MM-dd HH:mm:ss");
            var endDateTime = (DateTime.Now.Date.AddDays(15)).ToString("yyyy-MM-dd HH:mm:ss");
            var programModels = new List<ProgramModel>();
            var requestUrl = "https://development.ad-pointer.com/mgmt-api/v1/programs?startDateTime=" + startDateTime + "&endDateTime=" + endDateTime + "&channelIds=" + channelIdStrings;


            // Create Web Request 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Headers.Add("Authorization", string.Format("{0} {1}", tokenModel.tokenType, tokenModel.accessToken));
            request.Method = "Get";

            // Get Web Response
            WebResponse response = request.GetResponse();

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var result = reader.ReadToEnd();
                programModels = JsonConvert.DeserializeObject<List<ProgramModel>>(result);
            }

            using (var scope = new UnitOfWork())
            {
                var programs = _programRepository.GetAllAsync().Result.Where(x => x.StartDateTime > DateTime.Now.Date).ToList();
                await _programRepository.DeleteAllAsync(programs);
                scope.Commit();
            }

            using (var scope = new UnitOfWork())
            {


                foreach (var programModel in programModels)
                {
                    var newProgram = new Program()
                    {
                        Id = programModel.id,
                        Title = programModel.title,
                        Genre = programModel.genre,
                        Price = programModel.price,
                        Impressions = programModel.impressions,
                        Version = programModel.version,
                        CreatedOn = Convert.ToDateTime(programModel.createdOn),
                        ModifiedOn = Convert.ToDateTime(programModel.modifiedOn),
                        Description = programModel.description,
                        StartDateTime = Convert.ToDateTime(programModel.startDateTime),
                        EndDateTime = Convert.ToDateTime(programModel.endDateTime),
                        Duration = programModel.duration,
                        Language = programModel.language,
                        ProgramImagePath = programModel.programImagePath,
                        SeriesId = programModel.seriesId,
                        CastDirector = programModel.castDirector,
                        CastActors = programModel.castActors,
                        CategoryId = programModel.categoryId,
                        ChannelId = programModel.channelId

                    };
                    await _programRepository.InsertAsync(newProgram);
                }

                scope.Commit();
            }
            var loggerCommandCompleted = new CreateLoggerCommand()
            {
                LogLevel = LogLevel.Information.ToString(),
                ShortMessage = ErrorModel.MessageAdPointerClientCompleted,
                ExceptionMessage = string.Empty,
                CustomerId = null,
                CreatedOn = DateTime.Now

            };
            await this._eventBus.SendCommand(loggerCommandCompleted);

        }

        /// <summary>
        /// GetAdPointerSearchFilter
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task GetAdPointerSearchFilter()
        {

            var tokenModelEdge = await this.GetAccessTokenEdge();
            if (string.IsNullOrEmpty(tokenModelEdge.accessToken))
            {
                throw new ApplicationException("The Token from EDGE end point can not be null or empty");
            }
            var searchFilterListModel = new List<SearchFilterListModel>();
            var requestUrl = "https://development.ad-pointer.com/edge-api/v1/searchFilters";


            // Create Web Request 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Headers.Add("Authorization", string.Format("{0} {1}", tokenModelEdge.tokenType, tokenModelEdge.accessToken));
            request.Method = "Get";

            // Get Web Response
            WebResponse response = request.GetResponse();

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var result = reader.ReadToEnd();
                searchFilterListModel = JsonConvert.DeserializeObject<List<SearchFilterListModel>>(result);
            }


            using (var scope = new UnitOfWork())
            {
                var searchFilters = _searchFilterRepository.GetAllAsync().Result.ToList();
                var searchFilteAds = _searchFilterAdsRepository.GetAllAsync().Result.ToList();
                var searchFilteBrands = _searchFilterBrandsRepository.GetAllAsync().Result.ToList();
                var searchFilteIndustrys = _searchFilterIndustrysRepository.GetAllAsync().Result.ToList();
                var searchFilterCompanies = _searchFilterCompaniesRepository.GetAllAsync().Result.ToList();

                await _searchFilterCompaniesRepository.DeleteAllAsync(searchFilterCompanies);
                await _searchFilterIndustrysRepository.DeleteAllAsync(searchFilteIndustrys);
                await _searchFilterBrandsRepository.DeleteAllAsync(searchFilteBrands);
                await _searchFilterAdsRepository.DeleteAllAsync(searchFilteAds);
                await _searchFilterRepository.DeleteAllAsync(searchFilters);
                scope.Commit();
            }
            foreach (var newSearchFilter in searchFilterListModel)
            {
                await GetAdPointerSearchFilterById(newSearchFilter.id);
            }
            var loggerCommandCompleted = new CreateLoggerCommand()
            {
                LogLevel = LogLevel.Information.ToString(),
                ShortMessage = ErrorModel.MessageAdPointerSearchFilterCompleted,
                ExceptionMessage = string.Empty,
                CustomerId = null,
                CreatedOn = DateTime.Now

            };
            await this._eventBus.SendCommand(loggerCommandCompleted);

        }

        /// <summary>
        /// GetAdPointerSearchFilterById
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task GetAdPointerSearchFilterById(string id)
        {

            var tokenModelEdge = await this.GetAccessTokenEdge();
            if (string.IsNullOrEmpty(tokenModelEdge.accessToken))
            {
                throw new ApplicationException("The Token from EDGE end point can not be null or empty");
            }
            var searchFilterModel = new SearchFilterModel();
            var requestUrl = "https://development.ad-pointer.com/edge-api/v1/searchFilters/get?searchFilterId=" + id;

            // Create Web Request 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Headers.Add("Authorization", string.Format("{0} {1}", tokenModelEdge.tokenType, tokenModelEdge.accessToken));
            request.Method = "Get";

            // Get Web Response
            WebResponse response = request.GetResponse();

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var result = reader.ReadToEnd();
                searchFilterModel = JsonConvert.DeserializeObject<SearchFilterModel>(result);
            }

            using (var scope = new UnitOfWork())
            {
                var searchFilter = new SearchFilter()
                {
                    Id = searchFilterModel.id,
                    Version = searchFilterModel.version,
                    Name = searchFilterModel.name,
                    UserId = searchFilterModel.userId,
                    CreatedOn = Convert.ToDateTime(searchFilterModel.createdOn),
                    ModifiedOn = Convert.ToDateTime(searchFilterModel.modifiedOn)

                };
                await _searchFilterRepository.InsertAsync(searchFilter);

                foreach (var searchFilterAdId in searchFilterModel.adIds)
                {
                    var searchFilterAd = new SearchFilterAds()
                    {
                        SearchFilterId = searchFilter.Id,
                        AdId = searchFilterAdId
                    };
                    await _searchFilterAdsRepository.InsertAsync(searchFilterAd);
                }

                foreach (var searchFilterIndustryId in searchFilterModel.industryIds)
                {
                    var searchFilterIndustry = new SearchFilterIndustrys()
                    {
                        SearchFilterId = searchFilter.Id,
                        IndustryId = searchFilterIndustryId
                    };
                    await _searchFilterIndustrysRepository.InsertAsync(searchFilterIndustry);
                }

                foreach (var searchFilterBrandId in searchFilterModel.brandIds)
                {
                    var searchFilterBrand = new SearchFilterBrands()
                    {
                        SearchFilterId = searchFilter.Id,
                        BrandId = searchFilterBrandId
                    };
                    await _searchFilterBrandsRepository.InsertAsync(searchFilterBrand);
                }

                foreach (var searchFilterCompanyId in searchFilterModel.companyIds)
                {
                    var searchFilterCompany= new SearchFilterCompanies()
                    {
                        SearchFilterId = searchFilter.Id,
                        CompanyId = searchFilterCompanyId
                    };
                    await _searchFilterCompaniesRepository.InsertAsync(searchFilterCompany);
                }

                scope.Commit();
            }

        }
        #endregion


        #region EnumHelper

        /// <summary>
        /// GetEnumFromDescription
        /// </summary>
        /// <param name="description"></param>
        /// <param name="enumType"></param>
        /// <returns></returns>
        private static int GetEnumFromDescription(string description, Type enumType)
        {
            foreach (var field in enumType.GetFields())
            {
                DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute == null)
                    continue;
                if (attribute.Description == description)
                {
                    return (int)field.GetValue(null);
                }
            }
            return 0;
        }
        #endregion

    }
}
