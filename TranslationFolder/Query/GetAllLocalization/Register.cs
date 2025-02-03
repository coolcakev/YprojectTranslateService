using y_nuget.Endpoints;

namespace YprojectTranslateService.TranslationFolder.Query.GetAllLocalization;

public class Register : IEndpoint
{
    public void MapEndpoint(RouteGroupBuilder app)
    {
        app.MediateGet<GetAllLocalizationRequest, GetAllLocalizationResponse>("translation/get-all");
    }
}