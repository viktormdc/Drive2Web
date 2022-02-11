using System;
using System.Collections.Generic;
using System.Text;
using Analytics.Domain.Models.AdPointer.SocialNetworkReach;

namespace Analytics.Domain.Models.Views.AdPointer.SocialNetworkRech
{
    public class SocialNetworkReachViewModel
    {
        public List<GoogleDataReachModel> GoogleDataReachModels { get; set; }
        public List<FacebookDataReachModel> FacebookDataPageViewsModels { get; set; }
        public List<InstagramDataReachModel> InstagramDataProfileViewsModels { get; set; }
        public List<YoutubeDataReachModel> YoutubeDataViewsModels { get; set; }
        public List<LinkedInDataReachModel> LinkedInDataReachModels { get; set; }
        public List<TwitterDataReachModel> TwitterDataReachModels { get; set; }
    }
}
