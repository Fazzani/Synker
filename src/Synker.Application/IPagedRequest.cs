namespace Synker.Application
{
    public interface IPagedRequest
    {
        int Page { get; set; }

        int PageSize { get; set; }
    }
}
