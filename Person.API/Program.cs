using Person.API.Model;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc();
builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<PersonContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(
        options => options.WithOrigins("https://localhost:44412")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
    );
var supportedCultures = new[]
{
 new CultureInfo("en-US"),
 new CultureInfo("pt-BR")
};
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("pt-BR"),
    // Formatting numbers, dates, etc.
    SupportedCultures = supportedCultures,
    // UI strings that we have localized.
    SupportedUICultures = supportedCultures
});


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
