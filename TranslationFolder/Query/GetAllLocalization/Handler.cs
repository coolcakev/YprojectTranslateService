using MediatR;
using y_nuget.Endpoints;
using YprojectTranslateService.Database;
using Microsoft.EntityFrameworkCore;

namespace YprojectTranslateService.TranslationFolder.Query.GetAllLocalization;

public record GetAllLocalizationRequest() : IHttpRequest<GetAllLocalizationResponse>;

// респонс для зручності щоб не переписувати багато разів цю структуру
public class GetAllLocalizationResponse
{
    public Dictionary<string, Dictionary<string, Dictionary<string, string>>> Data { get; set; }
}

public class Handler : IRequestHandler<GetAllLocalizationRequest, Response<GetAllLocalizationResponse>> 
{
    private readonly ApplicationDbContext _dbContext;

    public Handler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response<GetAllLocalizationResponse>> Handle(GetAllLocalizationRequest request, CancellationToken cancellationToken)
    {
        var translations = await _dbContext.Translations
            .ToListAsync(cancellationToken);
        
        var result = translations
            .GroupBy(t => t.Language.ToString())
            .ToDictionary(
                langGroup => langGroup.Key,
                langGroup => langGroup
                    .GroupBy(t => t.Namespace)
                    .ToDictionary(
                        nsGroup => nsGroup.Key,
                        nsGroup => nsGroup.ToDictionary(
                            t => t.LocalizationKey,
                            t => t.TranslationText
                        )
                    )
            );

        return SuccessResponses.Ok(new GetAllLocalizationResponse
        {
            Data = result
        });
    }
}