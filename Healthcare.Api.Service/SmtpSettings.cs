namespace Healthcare.Api.Service
{
    public class SmtpSettings
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FromName { get; set; }
        public string FromAddress { get; set; }
    }
}
