using MassTransit;
using y_nuget.RabbitMq;
using YprojectTranslateService.Database;
using YprojectTranslateService.TranslationFolder.Entity;

namespace YprojectTranslateService.Configurations;

//TODO its not configuration file 
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
        foreach (var item in messageWrapper)
        {
            Console.WriteLine($"Key: {item.Key}, Ua: {item.Ua}, En: {item.En}, Ru: {item.Ru}");
        }
        
        var translations = new List<Translation>();
        
        foreach (var item in messageWrapper)
        {
            
            //TODO в модельці TranslationItemsMessage забрати 3 проперті з мовами і зробити масив з такого обєкта (мова(енамка) і валюе(стрінг))
            translations.Add(new Translation
            {
                Id = Guid.NewGuid(),
                LocalizationKey = item.Key,
                Namespace = namespaceName,
                Language = Language.ua,
                TranslationText = item.Ua
            });

            translations.Add(new Translation
            {
                Id = Guid.NewGuid(),
                LocalizationKey = item.Key,
                Namespace = namespaceName,
                Language = Language.en,
                TranslationText = item.En
            });

            translations.Add(new Translation
            {
                Id = Guid.NewGuid(),
                LocalizationKey = item.Key,
                Namespace = namespaceName,
                Language = Language.ru,
                TranslationText = item.Ru
            });
        }

        _dbContext.Translations.AddRange(translations);
        await _dbContext.SaveChangesAsync();
        Console.WriteLine("[*] Data range saved successfully.");
    }
}