using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Domain.Errors
{
    public static class ErrorModel
    {
        #region FacebookApi

        public const string ShortMessageFacebook = "Error occurred when processing FacebookAPI data";
        public const string ShortMessageFacebookCompletedProcessing = "Successfully completed processing FacebookAPI data ";

        #endregion

        #region InstagramApi
        public const string ShortMessageInstagram = "Error occurred when processing InstagramAPI data";
        public const string ShortMessageInstagramCompletedProcessing = "Successfully completed processing InstagramAPI data ";
        #endregion

        #region TwitterApi
        public const string ShortMessageTwitter = "Error occurred when processing TwitterAPI data";
        public const string ShortMessageTwitterCompletedProcessing = "Successfully completed processing TwitterAPI data ";
        #endregion

        #region LinkedInApi
        public const string ShortMessageLinkedInStartProcessing = "Successfully started processing LinkedInApi data ";
        public const string ShortMessageLinkeInCompletedProcessing = "Successfully completed processing LinkedInAPI data ";

        #endregion

        #region GoogleApi
        public const string ShortMessageGoogle = "Error occurred when processing GoogleAPI data";
        public const string ShortMessageGoogleCompletedProcessing = "Successfully completed processing GoogleAPI data ";

        #endregion

        #region YoutubeApi
        public const string ShortMessageYoutube = "Error occurred when processing YoutubeAPI data";
        public const string ShortMessageYoutubeCompletedProcessing = "Successfully completed processing YoutubeAPI data ";

        #endregion

        #region AdPointer Sync
        public const string ErrorMessageAdPointer = "Error occurred when processing Ad Pointer";
        public const string MessageAdPointerIndustryCompleted = "Successfully completed processing Ad Pointer Industry data";
        public const string MessageAdPointerBrandCompleted = "Successfully completed processing Ad Pointer Brand data";
        public const string MessageAdPointerCompanyCompleted = "Successfully completed processing Ad Pointer Company data";
        public const string MessageAdPointerChannelCompleted = "Successfully completed processing Ad Pointer Channel data";
        public const string MessageAdPointerAdCompleted = "Successfully completed processing Ad Pointer Ad data";
        public const string MessageAdPointerAdEventCompleted = "Successfully completed processing Ad Pointer AdEvent data";
        public const string MessageAdPointerClientCompleted = "Successfully completed processing Ad Pointer Client data";
        public const string MessageAdPointerSearchFilterCompleted = "Successfully completed processing Ad Pointer SearchFilter data";
        #endregion

        #region WebToDrive
        public const string TheUserIsNullOrEmpty = "analytics.user.is.null.or.empty";
        public const string StartDateRequiredField = "analytics.startdate.required.field";
        public const string EndDateRequiredField = "analytics.enddate.required.field";
        public const string RequiredField = "analytics.required.field";
        #endregion



    }
}
