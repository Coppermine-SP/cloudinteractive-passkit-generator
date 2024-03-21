namespace Cloudinteractive.PassKitGenerator.Services.Template
{
    public class Template
    {
        public string TemplateName { get; set; }

        //Common info
        public string PassTypeIdentifier { get; set; }
        public string TeamIdentifier { get; set; } 
        public string Description { get; set; }
        public string OrganizationName { get; set; }
        public string LogoText { get; set; }
        public bool UsingThumbnail { get; set; }

        //Color
        public string BackgroundColor { get; set; }
        public string LabelColor { get; set; }
        public string ForegroundColor { get; set; }

        //PrimaryField
        public string PrimaryFieldLabel { get; set; }

        //SecondaryField
        public bool UsingSecondaryField { get; set; }
        public string? SecondaryFieldLabel { get; set; }

        //AuxiliaryField
        public bool UsingFirstAuxiliaryField { get; set; }
        public string? FirstAuxiliaryFieldLabel { get; set; }

        public bool UsingSecondAuxiliaryField { get; set; }
        public string? SecondAuxiliaryFieldLabel { get; set; }

        //BackField
        public Field[]? BackFields { get; set; }

        //Barcode
        public bool UsingBarcode { get; set; }
        public string? BarcodeEncoding { get; set; }

        //Images
        public byte[]? IconImage { get; private set; }
        public byte[]? LogoImage { get; private set; }

        public void SetImage(byte[] icon, byte[] logo)
        {
            if (IconImage is not null || LogoImage is not null) return;

            IconImage = icon;
            LogoImage = logo;
        }
    }
}
