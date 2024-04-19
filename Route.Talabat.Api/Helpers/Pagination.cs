namespace Route.Talabat.Api.Helpers
{
	public class Pagination<T>
	{
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }
        public Pagination(int pageindex,int pagesize ,int count, IReadOnlyList<T> values)
        {
            PageIndex = pageindex;
            PageSize =pagesize;
            Count = count;
            Data = values;
        }
    }
}
