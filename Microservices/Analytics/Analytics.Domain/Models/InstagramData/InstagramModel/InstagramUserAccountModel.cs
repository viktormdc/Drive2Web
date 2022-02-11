using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Domain.Models.InstagramData.InstagramModel
{
    public class InstagramUserAccountModel
    {
        public DataUser[] data { get; set; }
        public Paging paging { get; set; }
    }
    public class DataUser
    {
        public string access_token { get; set; }
        public string category { get; set; }
        public Category_list[] category_list { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public List<string> tasks { get; set; }
    }
    public class Category_list
    {
        public string id { get; set; }
        public string name { get; set; }
    }
    public class Paging
    {
        public Cursors cursors { get; set; }
    }
    public class Cursors
    {
        public string before { get; set; }
        public string after { get; set; }
    }
}
