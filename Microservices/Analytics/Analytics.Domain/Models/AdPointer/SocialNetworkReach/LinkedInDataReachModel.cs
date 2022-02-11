using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Domain.Models.AdPointer.SocialNetworkReach
{
  public class LinkedInDataReachModel
    {
        public string name { get; set; }
        public List<LinkedInDataSeriesModel> series { get; set; }
    }

  public class LinkedInDataSeriesModel
    {
      public string name { get; set; }
      public int value { get; set; }
  }
}
