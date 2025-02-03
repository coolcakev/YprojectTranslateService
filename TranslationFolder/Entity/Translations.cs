namespace YprojectTranslateService.TranslationFolder.Entity;

//TODO винести енамку в пакет 
public enum Language
{
    en,
    ua,
    ru,
}

public class Translation
{
    public Guid Id { get; set; }
    public string LocalizationKey { get; set; }
    public string Namespace { get; set; }
    public Language Language { get; set; }
    public string TranslationText { get; set; }
}