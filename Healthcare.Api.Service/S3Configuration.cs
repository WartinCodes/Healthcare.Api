namespace Healthcare.Api.Service
{
    public class S3Configuration
    {
        public string AwsRegion { get; set; }
        public string AwsAccessKey { get; set; }
        public string AwsSecretAccessKey { get; set; }
        public string BucketName { get; set; }
        public string AmazonS3Service { get; set; }
    }
}
