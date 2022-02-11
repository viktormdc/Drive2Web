using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Domain.Models.YoutubeData
{
    public class YoutubeDataModel
    {
        public string kind { get; set; }
        public ColumnHeader[] columnHeaders { get; set; }
        public List<List<int>>  rows { get; set; }
    }


    public class ColumnHeader
    {
        public string name { get; set; }
        public string columnType { get; set; }
        public string dataType { get; set; }
    }
  
}
