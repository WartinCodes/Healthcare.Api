using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Contracts.Responses
{
    public class UltrasoundImageResponse
    {
        public int Id { get; set; }
        public string LocationS3 { get; set; }
    }
}
