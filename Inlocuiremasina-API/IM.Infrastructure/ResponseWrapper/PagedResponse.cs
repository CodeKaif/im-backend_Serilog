namespace IM.Infrastructure.ResponseWrapper
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public object TotalCount { get; set; }

        public PagedResponse(T data, object totalCount = null, int pageNumber = 0, int pageSize = 0)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
            this.TotalCount = totalCount == null ? 0 : totalCount;
        }
    }
}
