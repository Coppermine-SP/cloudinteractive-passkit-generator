using Cloudinteractive.PassKitGenerator.Services.Template;
using Passbook.Generator;
using System.Security.Cryptography.X509Certificates;

namespace Cloudinteractive.PassKitGenerator.Services.PassKit
{
    public static class PassKitGenerator
    {
        private const string _certLocation = "/config/certs/";
        private const string _intermediateCertName = "AppleWWDRCAG4.cer";
        private const string _passTypeIdCertName = "passbook.pfx";
        private const string _passTypeIdCertPasswordFileName = "passbook_password.txt";

        private static X509Certificate2? _appleWWDRCACert;
        private static X509Certificate2? _passTypeIdCert;

        private static ILogger? _logger;
        public static void Init()
        {
            _logger = Logging.CreateLogger("PassKitGenerator");

            _logger.Log(LogLevel.Information, "Load certificates from file..");
            _appleWWDRCACert = new X509Certificate2(_certLocation + _intermediateCertName);

            string? certPassword = Util.FileToString(_certLocation + _passTypeIdCertPasswordFileName);
            if (certPassword is null)
            {
                _logger.Log(LogLevel.Warning, "Cannot load PassTypeIdCert password! aborting.");
                throw new ApplicationException("certPassword was null.");
            }

            _passTypeIdCert = new X509Certificate2(_certLocation + _passTypeIdCertName, certPassword);
        }

        public static PassGeneratorRequest MakeRequest(Template.Template template)
        {
            var request = new PassGeneratorRequest();

            request.PassTypeIdentifier = template.PassTypeIdentifier;
            request.TeamIdentifier = template.TeamIdentifier;
            request.SerialNumber = Guid.NewGuid().ToString();
            request.Description = template.Description;
            request.OrganizationName = template.OrganizationName;
            request.LogoText = template.LogoText;
            request.SharingProhibited = true;

            request.BackgroundColor = template.BackgroundColor;
            request.LabelColor = template.LabelColor;
            request.ForegroundColor = template.ForegroundColor;

            request.Style = PassStyle.Generic;

            return request;
        }
    }
}
