namespace Healthcare.Api.Contracts.Responses
{
    public class ODataResponse<T>
    {
        public int Total { get; set; }
        public List<T> Values { get; set; }
    }
}
