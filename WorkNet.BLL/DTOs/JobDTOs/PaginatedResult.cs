namespace WorkNet.BLL.DTOs.JobDTOs
{
    public class PaginatedResult<T>
    {
        public T? Data { get; set; }
        public int TotalRecords { get; set; }
    }
}
