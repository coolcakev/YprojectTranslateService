using MassTransit;
using y_nuget.RabbitMq;
using YprojectTranslateService.Database;
using YprojectTranslateService.TranslationFolder.Entity;

namespace YprojectTranslateService.RabbitMq;

public class TranslateConsumerService : IConsumer<TranslationItemsMessage>
{
    private readonly ApplicationDbContext _dbContext;

    public TranslateConsumerService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<TranslationItemsMessage> context)
    {
        var messageWrapper = context.Message.Items;
        var namespaceName = context.Message.Namespace;
        
        Console.WriteLine("[X] Message:");
        Console.WriteLine($"TranslationItemsMessage [{namespaceName}]:");
        
        foreach (var entry in messageWrapper)
        {
            Console.WriteLine(
                $"Key: {entry.LocalizationKey}, " +
                $"Translations: {string.Join("; ", entry.Translations.Select(t => $"{t.Key}={t.Value}"))}"
            );
        }
        
        var translations = new List<Translation>();
        
        foreach (var entry in messageWrapper)
        {
            foreach (var (langKey, translationText) in entry.Translations)
            {
                translations.Add(new Translation
                {
                    LocalizationKey = entry.LocalizationKey,
                    Namespace = namespaceName,
                    Language = langKey,
                    TranslationText = translationText
                });
            }
        }
        
        _dbContext.Translations.AddRange(translations);
        await _dbContext.SaveChangesAsync();
        Console.WriteLine("[*] Data range saved successfully.");
    }
}