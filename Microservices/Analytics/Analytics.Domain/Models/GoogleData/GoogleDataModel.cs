using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Domain.Models.GoogleData
{
    public class GoogleDataModel
    {
        public Report[] reports { get; set; }
    }
    public class Report
    {
        public ColumnHeader columnHeader { get; set; }
        public Data data { get; set; }
    }

    public class ColumnHeader
    {
        public MetricHeader metricHeader { get; set; }

        public string[] dimensions { get; set; }
    }  
    public class MetricHeader
    {
        public  MetricHeaderEntries[] metricHeaderEntries { get; set; }
    }
    public class MetricHeaderEntries
    {
        public string name { get; set; }
        public string type { get; set; }
    }
    public class Data
    {
        public  Row[] rows { get; set; }
        public Total[] totals { get; set; }
        public int rowCount { get; set; }
        public List<MinimumAndMaximum> minimums { get; set; }
        public List<MinimumAndMaximum> maximums { get; set; }
        public bool isDataGolden { get; set; }
    }
    public class Row
    {
        public string[] dimensions { get; set; }
        public Metric[] metrics { get; set; }
    }
    public class Metric
    {
        public string[] values { get; set; }
    }
    public class Total
    {
        public string[] values { get; set; }
    }
    public class MinimumAndMaximum
    {
        public string[] values { get; set; }
    }
}
