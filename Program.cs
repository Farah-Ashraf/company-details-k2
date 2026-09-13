using CompanyLookupApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

var databaseUrl = builder.Configuration["DATABASE_URL"]
    ?? builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("DATABASE_URL or DefaultConnection must be configured.");

builder.Services.AddDbContext<CompanyDbContext>(options =>
    options.UseNpgsql(ToNpgsqlConnectionString(databaseUrl)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
    options.PreSerializeFilters.Add((swaggerDoc, httpRequest) =>
    {
        swaggerDoc.Servers = new List<OpenApiServer>
        {
            new() { Url = $"{httpRequest.Scheme}://{httpRequest.Host.Value}" }
        };
    });
});
app.UseSwaggerUI();


app.MapGet("/", () => "Company Lookup API is running.");

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

static string ToNpgsqlConnectionString(string value)
{
    if (!value.StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase) &&
        !value.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase))
    {
        return value;
    }

    var databaseUri = new Uri(value);
    var userInfo = databaseUri.UserInfo.Split(':', 2);
    var connectionString = new NpgsqlConnectionStringBuilder
    {
        Host = databaseUri.Host,
        Port = databaseUri.Port > 0 ? databaseUri.Port : 5432,
        Database = databaseUri.AbsolutePath.Trim('/'),
        Username = Uri.UnescapeDataString(userInfo[0]),
        Password = userInfo.Length > 1 ? Uri.UnescapeDataString(userInfo[1]) : string.Empty,
        SslMode = SslMode.Prefer
    };

    var query = databaseUri.Query.TrimStart('?');
    if (query.Contains("schema=", StringComparison.OrdinalIgnoreCase))
    {
        connectionString.SearchPath = "public";
    }

    return connectionString.ConnectionString;
}