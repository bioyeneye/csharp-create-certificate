namespace IdentityServerCertificate
{
    public class AppSettings
    {
        public string BucketName { get; set; }
        public string AwsAccessKeyId { get; set; }    
        public string AwsSecretAccessKey { get; set; }
        public string CertificateFileName { get; set; }
        public string FileDestination { get; set; }
    }
}