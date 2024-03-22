using System.Collections.ObjectModel;
using Newtonsoft.Json;
namespace Cloudinteractive.PassKitGenerator.Services.Template
{
    public static class TemplateManager
    {
        public static ReadOnlyDictionary<string, Template> TemplateDictionary;
        private const string TemplateLocation = "/config/templates/";
        private static ILogger? _logger;
        public static void Init()
        {
            _logger = Logging.CreateLogger("TemplateManager");

            _logger.Log(LogLevel.Information, $"Getting TemplateBase from {TemplateLocation}..");
            var templateDir = new DirectoryInfo(TemplateLocation);
            var templates = templateDir.GetDirectories();
            var dict = new Dictionary<string, Template>();

            if (!templateDir.Exists || templates.Length == 0)
            {
                _logger.LogCritical("Cannot find any templates! aborting.");
                throw new ApplicationException();
            }

            foreach (var template in templates)
            {
                string templateName = template.Name;
                string location = template.FullName;
                var metadataFile = new FileInfo( location+ "/metadata.json");

                _logger.Log(LogLevel.Information, $"Directory found: {templateName}");

                if (!metadataFile.Exists)
                {
                    _logger.Log(LogLevel.Warning, $"{templateName}: metadata not found!");
                    return;
                }

                try
                {
                    var metadata = File.ReadAllText(metadataFile.FullName);
                    var templateObject = JsonConvert.DeserializeObject<Template>(metadata);

                    var iconImg = Util.FileToByteArray(location + "/img/icon.png");
                    var logoImg = Util.FileToByteArray(location + "/img/logo.png");

                    if (iconImg is null)
                    {
                        _logger.Log(LogLevel.Warning, $"{templateName}: cannot found icon image!");
                        continue;
                    }

                    if (logoImg is null)
                    {
                        _logger.Log(LogLevel.Warning, $"{templateName}: cannot found logo image!");
                        continue;
                    }

                    templateObject.SetImage(iconImg, logoImg);
                    dict.Add(templateName, templateObject);
                    _logger.Log(LogLevel.Information, $"Template loaded: {templateName}");
                }
                catch (Exception e)
                {
                    _logger.Log(LogLevel.Warning, $"{templateName}: exception occurred while loading.");
                    continue;
                }
            }

            _logger.Log(LogLevel.Information, $"Loaded total {dict.Count} templates in dict.");
            TemplateDictionary = dict.AsReadOnly();
        }
    }
}
