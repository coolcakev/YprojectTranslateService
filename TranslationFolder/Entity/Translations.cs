using y_nuget.RabbitMq;
using y_nuget.Sources;

namespace YprojectTranslateService.TranslationFolder.Entity;

public class Translation: BaseEntity
{
    public string LocalizationKey { get; set; }
    public string Namespace { get; set; }
    public Language Language { get; set; }
    public string TranslationText { get; set; }
}