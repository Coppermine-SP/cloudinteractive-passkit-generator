using Cloudinteractive.PassKitGenerator.Services.Template;

namespace Cloudinteractive.PassKitGenerator.Models
{
    public class IssueModel
    {
        public string? TemplateKey { get; set; }
        public Template? Template { get; private set; }

        public string? PrimaryFieldValue { get; set; }

        public bool TemplateKeyValidation()
        {
            Template template;
            if (!TemplateManager.TemplateDictionary.TryGetValue(TemplateKey, out template)) return false;

            Template = template;
            return true;
        }
    }
}
