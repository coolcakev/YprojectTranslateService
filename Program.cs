using y_nuget;
using y_nuget.Endpoints;
using YprojectTranslateService.Configurations;
using YprojectTranslateService.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddConfiguration(builder);

var app = builder.Build();

app.MapEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.AddAutoMigration();
}

app.UseHttpsRedirection();

app.Run();