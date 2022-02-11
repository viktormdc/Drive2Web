using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Domain.Models.AdPointer.SocialNetworkReach
{
  public class GoogleDataReachModel
    {
        public string name { get; set; }
        public List<GoogleDataReachSeriesModel> series { get; set; }
    }

  public class GoogleDataReachSeriesModel
  {
      public string name { get; set; }
      public int value { get; set; }
  }
}
