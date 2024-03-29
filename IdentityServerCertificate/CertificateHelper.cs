using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using CertificateManager;
using CertificateManager.Models;

namespace IdentityServerCertificate
{
    public class CertificateHelper
    {
        
        static CreateCertificates _cc;
        private static string _fileaName;
        public static X509Certificate2 CreateRsaCertificate(CreateCertificates _cc, string dnsName, int validityPeriodInYears)
        {
            var basicConstraints = new BasicConstraints
            {
                CertificateAuthority = false,
                HasPathLengthConstraint = false,
                PathLengthConstraint = 0,
                Critical = false
            };

            var subjectAlternativeName = new SubjectAlternativeName
            {
                DnsName = new List<string>
                {
                    dnsName,
                }
            };

            var x509KeyUsageFlags = X509KeyUsageFlags.DigitalSignature;

            // only if certification authentication is used
            var enhancedKeyUsages = new OidCollection
            {
                OidLookup.ClientAuthentication,
                OidLookup.ServerAuthentication 
                // OidLookup.CodeSigning,
                // OidLookup.SecureEmail,
                // OidLookup.TimeStamping  
            };

            var certificate = _cc.NewRsaSelfSignedCertificate(
                new DistinguishedName { CommonName = dnsName },
                basicConstraints,
                new ValidityPeriod
                {
                    ValidFrom = DateTimeOffset.UtcNow,
                    ValidTo = DateTimeOffset.UtcNow.AddYears(validityPeriodInYears)
                },
                subjectAlternativeName,
                enhancedKeyUsages,
                x509KeyUsageFlags,
                new RsaConfiguration
                { 
                    KeySize = 2048,
                    HashAlgorithmName = HashAlgorithmName.SHA512
                });

            return certificate;
        }
        public static X509Certificate2 CreateECDsaCertificate(CreateCertificates _cc, string dnsName, int validityPeriodInYears)
        {
            var basicConstraints = new BasicConstraints
            {
                CertificateAuthority = false,
                HasPathLengthConstraint = false,
                PathLengthConstraint = 0,
                Critical = false
            };

            var subjectAlternativeName = new SubjectAlternativeName
            {
                DnsName = new List<string>
                {
                    dnsName,
                }
            };

            var x509KeyUsageFlags = X509KeyUsageFlags.DigitalSignature;

            // only if certification authentication is used
            var enhancedKeyUsages = new OidCollection {
                OidLookup.ClientAuthentication,
                OidLookup.ServerAuthentication 
                // OidLookup.CodeSigning,
                // OidLookup.SecureEmail,
                // OidLookup.TimeStamping 
            };

            var certificate = _cc.NewECDsaSelfSignedCertificate(
                new DistinguishedName { CommonName = dnsName },
                basicConstraints,
                new ValidityPeriod
                {
                    ValidFrom = DateTimeOffset.UtcNow,
                    ValidTo = DateTimeOffset.UtcNow.AddYears(validityPeriodInYears)
                },
                subjectAlternativeName,
                enhancedKeyUsages,
                x509KeyUsageFlags,
                new ECDsaConfiguration
                {
                    KeySize = 384,
                    HashAlgorithmName = HashAlgorithmName.SHA384
                });

            return certificate;
        }

        public async Task UploadCertificateToAWSS3()
        {
            IAmazonS3 client = new AmazonS3Client("AKI...access-key...", "+8Bo...secrey-key...", RegionEndpoint.APSoutheast2);

            var fileName = "cert_rsa512.pfx";
            FileInfo file = new FileInfo(fileName);  
            string destPath = $"folder/sub-folder/{fileName}"; // <-- low-level s3 path uses /
            var bucketName = "my-bucket-name";
            PutObjectRequest request = new PutObjectRequest()  
            {  
                InputStream = file.OpenRead(),  
                BucketName = bucketName,  
                Key = destPath // <-- in S3 key represents a path  
            };  
  
            PutObjectResponse response = await  client.PutObjectAsync(request); 
        }
    }
}