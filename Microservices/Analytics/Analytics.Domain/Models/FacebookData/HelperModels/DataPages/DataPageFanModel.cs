using Analytics.Domain.Models.FacebookData.HelperModels.Values;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Domain.Models.FacebookData.HelperModels.DataPages
{
    public class DataPageFanModel
    {
        public string name { get; set; }
        public string period { get; set; }
        public ValuesModel[] values { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string id { get; set; }
    }
}
