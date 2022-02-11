using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Analytics.Domain.Enums.SocialNetworkEnum
{
    public enum SocialNetworkEnum
    {
        [Description("Facebook")]
        Facebook = 1,
        [Description("Instagram")]
        Instagram = 2,
        [Description("GoogleAnalytic")]
        GoogleAnalytic = 3,
        [Description("Youtube")]
        Youtube = 4,
        [Description("Twitter")]
        Twitter = 5,
        [Description("LinkedIn")]
        LinkedIn = 6
    }
}
