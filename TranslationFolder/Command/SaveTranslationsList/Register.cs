using y_nuget.Endpoints;

namespace YprojectTranslateService.TranslationFolder.Command.SaveTranslationsList;

public class Register: IEndpoint
{
    public void MapEndpoint(RouteGroupBuilder app)
    {
        app.MediatePost<SaveTranslationsListRequest, bool>("translation/save-list");
    }
}