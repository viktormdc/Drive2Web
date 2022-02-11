using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Domain.Programs
{
    public class Program
    {
        public string Id { get; set; }
        public int Version { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int Duration { get; set; }
        public string Language { get; set; }
        public string ProgramImagePath { get; set; }
        public string SeriesId { get; set; }
        public string CastDirector { get; set; }
        public string CastActors { get; set; }
        public string CategoryId { get; set; }
        public string ChannelId { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
        public int Impressions { get; set; }
    }
}
