using Infrastructure.V1.FilterBase.Model;
using System.Linq.Dynamic.Core;

namespace Infrastructure.V1.FilterBase
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ToPaggingView<T>(
    this IQueryable<T> query, FilterRequest filter)
        {
            // filter
            query = Filter(query, filter);
            // sort
            if (filter.Sort == null)
            {
                filter.Sort = Enumerable.Empty<SortModel>();
            }

            query = Sort(query, filter.Sort);
            // EF does not apply skip and take without order.
            query = Limit(query, filter.PageSize, filter.PageNumber);

            // return the final query
            return query;
        }

        public static IQueryable<T> ToExportView<T>(
           this IQueryable<T> query, FilterRequest filter)
        {
            // filter
            query = Filter(query, filter);
            // sort
            if (filter.Sort != null)
            {
                query = Sort(query, filter.Sort);
            }
            // return the final query
            return query;
        }

        public static IQueryable<T> ToFilterTotalView<T>(
          this IQueryable<T> query, FilterRequest filter)
        {
            // filter
            query = Filter(query, filter);
            // sort
            return query;
        }

        private static IQueryable<T> Filter<T>(
            IQueryable<T> queryable, FilterRequest filter)
        {
            var query = "";
            List<string> values = new List<string>();
            if (!string.IsNullOrEmpty(filter.GlobalFilter) && filter.GlobalFilterFields != null && filter.GlobalFilterFields.Count() > 0)
            {
                var filterResponse = GlobalFilter(filter.GlobalFilterFields, query, filter.GlobalFilter, values);
                query = filterResponse.Item1;
                values = filterResponse.Item2;
            }

            if (filter.Filters != null && filter.Filters.Count() > 0)
            {
                var filterResponse = IndividualFilter(filter.Filters, query, values);
                query = filterResponse.Item1;
                values = filterResponse.Item2;
            }

            if (!string.IsNullOrEmpty(query))
            {
                string[] queryValues = values.ToArray();

                queryable = queryable.AsQueryable().Where(query, queryValues);
            }

            return queryable;
        }


        private static IQueryable<T> Sort<T>(
            IQueryable<T> queryable, IEnumerable<SortModel> sort)
        {
            if (sort != null && sort.Any())
            {
                var ordering = string.Join(",",
                  sort.Select(s => $"{s.Field} {s.Direction}"));
                return queryable.OrderBy(ordering);
            }
            return queryable;
        }

        private static IQueryable<T> Limit<T>(
          IQueryable<T> queryable, int limit, int offset)
        {
            // set default values
            limit = limit <= 0 ? 20 : limit;
            offset = offset <= 0 ? 1 : offset;
            return queryable.Skip((offset - 1) * limit).Take(limit);
        }

        private static readonly IDictionary<string, string>
        Operators = new Dictionary<string, string>
        {
        {"eq", "="},
        {"in", "Contains"},
        {"neq", "!="},
        //Add New
        {"nnl", "IsAvailable"},
        {"nl", "NotAvailable"},
       // {"between", "Between"},

        {"lt", "<"},
        {"lte", "<="},
        {"gt", ">"},
        {"gte", ">="},
        {"ltgt", "="},
        {"ltegte", "="},
        {"sw", "StartsWith"},
        {"ew", "EndsWith"},
        {"nin", "Contains"},
        };


        public static Tuple<string, List<string>> GlobalFilter(IList<FilterModel> fields, string query, string value, List<string> values)
        {
            List<string> queries = new List<string>();
            foreach (var field in fields)
            {
                field.Operator = "in";
                field.Value = value;
                var tranform = Transform(field, values);
                queries.Add(tranform.Item1);
                // values = values.Concat(tranform.Item2).ToList();
            }
            query += string.Join("||", queries.ToArray());
            return new Tuple<string, List<string>>(query, values);
        }

        public static Tuple<string, List<string>> IndividualFilter(IList<FilterModel> fields, string query, List<string> values)
        {
            List<string> queries = new List<string>();
            foreach (var field in fields)
            {
                var tranform = Transform(field, values);
                queries.Add(tranform.Item1);
                // values = values.Concat(tranform.Item2).ToList();
            }
            // add and tag if 
            if (!string.IsNullOrEmpty(query))
            {
                query += " && ";
            }
            query += string.Join("&&", queries.ToArray());
            return new Tuple<string, List<string>>(query, values);
        }

        public static Tuple<string, List<string>> Transform(FilterModel filter, List<string> values)
        {
            var comparison = Operators[filter.Operator];
            var query = "";
            switch (filter.Type)
            {
                case "dateoptional":
                    if (filter.Operator == "in" || filter.Operator == "sw" || filter.Operator == "ew")
                    {
                        query = String.Format("({0} != null && {0}.Value.Date.ToString().ToLower().{1}(@{2}))", filter.Field, comparison, values.Count());
                        values.Add(filter.Value);
                    }
                    else if (filter.Operator == "doesnotcontain")
                    {
                        query = String.Format("({0} != null && !{0}.Value.Date.ToString().ToLower().{1}(@{2}))", filter.Field, comparison, values.Count());
                        values.Add(filter.Value);
                    }
                    else
                    {
                        query = String.Format("({0} != null && {0}.Value.Date {1} Convert.ToDateTime(@{2}).Date)", filter.Field, comparison, values.Count());
                        values.Add(filter.DateValue.ToString());
                    }
                    break;
                case "date":
                    if (filter.Operator == "in" || filter.Operator == "sw" || filter.Operator == "ew")
                    {
                        query = String.Format("({0} != null && {0}.Date.ToString().ToLower().{1}(@{2}.ToLower()))", filter.Field, comparison, values.Count());
                        values.Add(filter.Value);
                    }
                    else if (filter.Operator == "nin")
                    {
                        query = String.Format("({0} != null && !{0}.Date.ToString().ToLower().{1}(@{2}.ToLower())", filter.Field, comparison, values.Count());
                        values.Add(filter.Value);
                    }
                    else if (filter.Operator == "nnl")
                    {
                        query = String.Format("({0} != null)", filter.Field);
                    }
                    else if (filter.Operator == "nl")
                    {
                        query = String.Format("({0} == null)", filter.Field);
                    }
                    else
                    {
                        query = String.Format("({0} != null && {0}.Date {1} Convert.ToDateTime(@{2}).ToUniversalTime().Date)", filter.Field, comparison, values.Count());
                        values.Add(filter.DateValue.ToString());
                    }
                    break;
                case "daterange":
                    if (filter.Operator == "ltgt")
                    {
                        query = String.Format("({0} != null && {0}.Date > Convert.ToDateTime(@{1}).ToUniversalTime() && {0}.Date < Convert.ToDateTime(@{2}).ToUniversalTime())", filter.Field, values.Count(), values.Count() + 1);
                        values.Add(filter.DateRangeValue.MinDate.ToString());
                        values.Add(filter.DateRangeValue.MaxDate.ToString());
                    }
                    else if (filter.Operator == "ltegte")
                    {
                        query = String.Format("({0} != null && {0}.Date >= Convert.ToDateTime(@{1}).ToUniversalTime() && {0}.Date <= Convert.ToDateTime(@{2}).ToUniversalTime())", filter.Field, values.Count(), values.Count() + 1);
                        values.Add(Convert.ToDateTime(filter.DateRangeValue.MinDate).ToString());
                        values.Add(Convert.ToDateTime(filter.DateRangeValue.MaxDate).ToString());
                    }
                    break;
                case "string":
                    if (filter.Operator == "nin")
                    {
                        query = String.Format("({0} != null && !{0}.ToLower().{1}(@{2}.ToLower()))", filter.Field, comparison, values.Count());
                    }
                    else if (filter.Operator == "in" || filter.Operator == "sw" || filter.Operator == "ew" || filter.Operator == "")
                    {
                        query = String.Format("({0} != null && {0}.ToLower().{1}(@{2}.ToLower()))", filter.Field, comparison, values.Count());
                    }
                    else if (filter.Operator == "nnl")
                    {
                        query = String.Format("({0} != null)", filter.Field);
                    }
                    else if (filter.Operator == "nl")
                    {
                        query = String.Format("({0} = null)", filter.Field);
                    }
                    else
                    {
                        query = String.Format("({0}.ToLower() {1} @{2}.ToLower())", filter.Field, comparison, values.Count());
                    }
                    values.Add(filter.Value);
                    break;
                case "number":
                    if (filter.Operator == "nin")
                    {
                        query = String.Format("({0} != null && !{0}.ToString().ToLower().{1}(@{2}))", filter.Field, comparison, values.Count());
                    }
                    else if (filter.Operator == "in" || filter.Operator == "sw" || filter.Operator == "ew" || filter.Operator == "")
                    {
                        query = String.Format("({0} != null && {0}.ToString().ToLower().{1}(@{2}))", filter.Field, comparison, values.Count());
                    }
                    else if (filter.Operator == "between")
                    {
                        query = String.Format("({0} != null && {0} >= @{1} && {0} <= @{2})", filter.Field, values.Count(), values.Count() + 1);
                        values.Add(filter.NumRangeValue.MinNum.ToString());
                        values.Add(filter.NumRangeValue.MaxNum.ToString());
                    }
                    else if (filter.Operator == "nnl")
                    {
                        query = String.Format("({0} != null)", filter.Field);
                    }
                    else if (filter.Operator == "nl")
                    {
                        query = String.Format("({0} = null)", filter.Field);
                    }
                    else
                    {
                        query = String.Format("({0} {1} @{2})", filter.Field, comparison, values.Count());
                    }
                    values.Add(filter.Value);
                    break;
                case "numrange":
                    if (filter.Operator == "ltgt")
                    {
                        query = String.Format("({0} != null && {0} > @{1} && {0} < @{2})", filter.Field, values.Count(), values.Count() + 1);
                        values.Add(filter.NumRangeValue.MinNum.ToString());
                        values.Add(filter.NumRangeValue.MaxNum.ToString());
                    }
                    else if (filter.Operator == "ltegte")
                    {
                        query = String.Format("({0} != null && {0} >= @{1} && {0} <= @{2})", filter.Field, values.Count(), values.Count() + 1);
                        values.Add(filter.NumRangeValue.MinNum.ToString());
                        values.Add(filter.NumRangeValue.MaxNum.ToString());
                    }
                    break;
                case "array":
                    var queries = new List<string>();
                    if (filter.SubType == "string")
                    {
                        foreach (var item in filter.ArrayValue)
                        {
                            queries.Add(String.Format("({0} = @{1})", filter.Field, values.Count()));
                            values.Add(item);
                        }
                    }
                    else
                    {
                        foreach (var item in filter.ArrayValue)
                        {
                            queries.Add(String.Format("({0}.ToString() = @{1})", filter.Field, values.Count()));
                            values.Add(item);
                        }
                    }
                    query = string.Join("||", queries.ToArray());
                    break;
                default:
                    if (filter.Operator == "nin")
                    {
                        query = String.Format("({0} != null && !{0}.ToString().ToLower().{1}(@{2}.ToLower()))", filter.Field, comparison, values.Count());
                    }
                    else if (filter.Operator == "in" || filter.Operator == "sw" || filter.Operator == "ew")
                    {
                        query = String.Format("({0} != null && {0}.ToString().ToLower().{1}(@{2}.ToLower()))", filter.Field, comparison, values.Count());
                    }
                    else
                    {
                        query = String.Format("({0} {1} @{2})", filter.Field, comparison, values.Count());
                    }
                    values.Add(filter.Value);
                    break;
            }
            return new Tuple<string, List<string>>(query, values);
        }

    }

}
