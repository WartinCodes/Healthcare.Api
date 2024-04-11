using Newtonsoft.Json;

namespace Healthcare.Api.Contracts.Requests
{
    public class SupportRequest
    {
        public string UserId { get; set; }
        public string Priority { get; set; }
        public string Module { get; set; }
        public string Description { get; set; }
    }
}
