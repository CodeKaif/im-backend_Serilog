
namespace Infrastructure.V1.FilterBase.Model
{
    public class FilterRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<SortModel> Sort { get; set; }
        public List<FilterModel> Filters { get; set; }
        public string GlobalFilter { get; set; }
        public List<FilterModel> GlobalFilterFields { get; set; }
    }

    public class SortModel
    {
        public string Field { get; set; }
        public string Direction { get; set; }
    }

    public class FilterModel
    {
        public string Field { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public string Operator { get; set; }
        public string Logic { get; set; }
        public string Value { get; set; }
        public DateTime DateValue { get; set; }
        public string[] ArrayValue { get; set; }
        public DateFilterModel DateRangeValue { get; set; }
        public NumFilterModel NumRangeValue { get; set; }
    }

    public class DateFilterModel
    {
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
    }
    public class NumFilterModel
    {
        public int MinNum { get; set; }
        public int MaxNum { get; set; }
    }
}
