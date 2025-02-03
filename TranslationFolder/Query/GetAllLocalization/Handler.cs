using MediatR;
using y_nuget.Endpoints;
using YprojectTranslateService.Database;
using Microsoft.EntityFrameworkCore;

namespace YprojectTranslateService.TranslationFolder.Query.GetAllLocalization;

public record GetAllLocalizationRequest() : IHttpRequest<GetAllLocalizationResponse>;

//TODO навіщо цей респонс
public class GetAllLocalizationResponse
{
    public Dictionary<string, Dictionary<string, Dictionary<string, string>>> Data { get; set; }

    //TODO а без конструктора не можна?
    public GetAllLocalizationResponse(Dictionary<string, Dictionary<string, Dictionary<string, string>>> data)
    {
        Data = data;
    }
}

public class Handler : IRequestHandler<GetAllLocalizationRequest, Response<GetAllLocalizationResponse>> 
{
    private readonly ApplicationDbContext _dbContext;

    public Handler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //TODO do lazy loading client side send you language and namespace
    public async Task<Response<GetAllLocalizationResponse>> Handle(GetAllLocalizationRequest request, CancellationToken cancellationToken)
    {
        var translations = await _dbContext.Translations
            //TODO навіщо тут есноутрекінг
            .AsNoTracking()
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

        return SuccessResponses.Ok(new GetAllLocalizationResponse(result));
    }
}