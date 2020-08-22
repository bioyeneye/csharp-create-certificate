using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using CertificateManager;
using CertificateManager.Models;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServerCertificate
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("About to create certificate!");
            var sp = new ServiceCollection()
                .AddCertificateManager()
                .BuildServiceProvider();

            _cc = sp.GetService<CreateCertificates>();
            // var rsaCert = CreateRsaCertificate("localhost", 10);
            // var ecdsaCert = CreateECDsaCertificate("localhost", 10);
            //
            // string password = "1234";
            // var iec = sp.GetService<ImportExportCertificate>();
            //
            // var rsaCertPfxBytes = iec.ExportSelfSignedCertificatePfx(password, rsaCert);
            // File.WriteAllBytes("cert_rsa512.pfx", rsaCertPfxBytes);
            //
            // var ecdsaCertPfxBytes = iec.ExportSelfSignedCertificatePfx(password, ecdsaCert);
            // File.WriteAllBytes("cert_ecdsa384.pfx", ecdsaCertPfxBytes);

            try
            {
                var certificate = Convert.FromBase64String("JvwMRJTS9ZQ844F+pIT9ysXno2Zow5lkwcyBhKiXdna/MiFc3Fg8KuEtQ2G7InxVOjvnGgdLKB/eAgm/An+h2Q==");
                var _signingCertificate = new X509Certificate2(certificate);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("created");
        }
    }
}