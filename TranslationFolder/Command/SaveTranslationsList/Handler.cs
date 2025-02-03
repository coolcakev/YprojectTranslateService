using MediatR;
using Microsoft.AspNetCore.Mvc;
using y_nuget.Endpoints;
using YprojectTranslateService.Database;
using YprojectTranslateService.TranslationFolder.Entity;

namespace YprojectTranslateService.TranslationFolder.Command.SaveTranslationsList;

public record TranslationItemDto(
    string Key,
    string Namespace,
    string Ua,
    string En,
    string Ru
);
public record SaveTranslationsListRequest([FromBody] List<TranslationItemDto> Items) : IHttpRequest<bool>;

//TODO навіщо існує цей ендпоінт
public class Handler : IRequestHandler<SaveTranslationsListRequest, Response<bool>>
{
    private readonly ApplicationDbContext _dbContext;

    public Handler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Response<bool>> Handle(SaveTranslationsListRequest request, CancellationToken cancellationToken)
    {
        var newTranslations = new List<Translation>();

        foreach (var item in request.Items)
        {
            var languageTexts = new Dictionary<Language, string?>
            {
                { Language.ua, item.Ua },
                { Language.en, item.En },
                { Language.ru, item.Ru }
            };

            foreach (var (language, text) in languageTexts)
            {
                if (!string.IsNullOrWhiteSpace(text))
                {
                    newTranslations.Add(new Translation
                    {
                        Id = Guid.NewGuid(),
                        Namespace = item.Namespace,
                        LocalizationKey = item.Key,
                        Language = language,
                        TranslationText = text
                    });
                }
            }
        }

        if (newTranslations.Any())
        {
            _dbContext.Translations.AddRange(newTranslations);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return SuccessResponses.Ok(true);
    }
}