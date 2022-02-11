using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Domain.Models.AdPointer.SocialNetworkReach
{
   public class TwitterDataReachModel
    {
        public string name { get; set; }
        public List<TwitterDataSeriesModel> series { get; set; }
    }

   public class TwitterDataSeriesModel
    {
       public string name { get; set; }
       public int value { get; set; }
   }
}
