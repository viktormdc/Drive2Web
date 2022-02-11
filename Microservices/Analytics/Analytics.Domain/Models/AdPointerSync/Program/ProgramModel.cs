using System;

namespace Analytics.Domain.Models.AdPointerSync.Program
{
    public class ProgramModel
    {
        public string id { get; set; }
        public int version { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime modifiedOn { get; set; }
        public string programId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime startDateTime { get; set; }
        public DateTime endDateTime { get; set; }
        public int duration { get; set; }
        public string language { get; set; }
        public string programImagePath { get; set; }
        public string seriesId { get; set; }
        public string castDirector { get; set; }
        public string castActors { get; set; }
        public string categoryId { get; set; }
        public string channelId { get; set; }
        public string genre { get; set; }
        public double price { get; set; }
        public int impressions { get; set; }
    }
}
