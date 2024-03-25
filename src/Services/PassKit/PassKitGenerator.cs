using Cloudinteractive.PassKitGenerator.Services.Template;
using Passbook.Generator;
using System.Security.Cryptography.X509Certificates;
using Passbook.Generator.Fields;
using System.Reflection;

namespace Cloudinteractive.PassKitGenerator.Services.PassKit
{
    public static class PassKitGenerator
    {
        private const string _certLocation = "/config/certs/";
        private const string _intermediateCertName = "AppleWWDRCAG4.cer";
        private const string _passTypeIdCertName = "passbook.pfx";
        private const string _passTypeIdCertPasswordFileName = "passbook_password.txt";

        private static object _dictionaryLock = new object();
        private static Dictionary<string, byte[]> _passDictionary = new Dictionary<string, byte[]>();

        private static X509Certificate2? _appleWWDRCACert;
        private static X509Certificate2? _passTypeIdCert;
        private static PassGenerator _passGenerator;

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
            _passGenerator = new PassGenerator();
        }

        public static string MakePass(Models.IssueModel model)
        {
            var template = model.Template;
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

            //Thumbnail image
            if (template.UsingThumbnail)
            {
                using var stream = model.ThumbnailImage.OpenReadStream();
                var image = Util.StreamToByteArray(stream);
                request.Images.Add(PassbookImage.Thumbnail, image);
                request.Images.Add(PassbookImage.Thumbnail2X, image);
                request.Images.Add(PassbookImage.Thumbnail3X, image);
            }

            //Icon images
            request.Images.Add(PassbookImage.Icon, template.IconImage);
            request.Images.Add(PassbookImage.Icon2X, template.IconImage);
            request.Images.Add(PassbookImage.Icon3X, template.IconImage);

            //Logo images
            request.Images.Add(PassbookImage.Logo, template.LogoImage);
            request.Images.Add(PassbookImage.Logo2X, template.LogoImage);
            request.Images.Add(PassbookImage.Logo3X, template.LogoImage);

            //Primary field
            request.AddPrimaryField(new StandardField(template.PrimaryFieldKey, template.PrimaryFieldLabel,
                model.PrimaryFieldValue));

            //Secondary field
            if (template.UsingSecondaryField)
                request.AddSecondaryField(new StandardField(template.SecondaryFieldKey, template.SecondaryFieldLabel, model.SecondaryFieldValue));

            //First auxiliary field
            if (template.UsingFirstAuxiliaryField)
                request.AddAuxiliaryField(new StandardField(template.FirstAuxiliaryFieldKey,
                    template.SecondAuxiliaryFieldLabel, model.FirstAuxiliaryFieldValue));

            //Second auxiliary field
            if (template.UsingSecondAuxiliaryField)
                request.AddAuxiliaryField(new StandardField(template.SecondAuxiliaryFieldKey, template.SecondAuxiliaryFieldLabel, model.SecondAuxiliaryFieldValue));

            //Back fields
            if (template.BackFields is not null)
            {
                foreach (var field in template.BackFields)
                {
                    request.AddBackField(new StandardField(field.Key, field.Label, field.Value));
                }
            }

            //Barcode
            if (template.UsingBarcode)
            {
                request.AddBarcode(BarcodeType.PKBarcodeFormatCode128, model.BarcodeValue, template.BarcodeEncoding);
            }

            string passId = Util.GeneratePassID();
            request.AppleWWDRCACertificate = _appleWWDRCACert;
            request.PassbookCertificate = _passTypeIdCert;

            byte[] pass = _passGenerator.Generate(request);

            lock (_dictionaryLock)
            {
                _passDictionary.Add(passId, pass);
            }

            _logger.Log(LogLevel.Information, $"Create new pass #{passId} (template: {model.TemplateKey}).");
            return passId;

        }

        public static bool CheckPass(string passId)
        {
            lock (_dictionaryLock)
            {
                return _passDictionary.ContainsKey(passId);
            }
        }

        public static bool GetPass(string passId, out byte[] pass)
        {
            lock (_dictionaryLock)
            {
                 if(!_passDictionary.TryGetValue(passId, out pass)) return false;
                 _passDictionary.Remove(passId);
                _logger.Log(LogLevel.Information, $"Pass destroyed #{passId}.");
                return true;
            }
        }
    }
}
