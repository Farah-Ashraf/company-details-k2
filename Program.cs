using CompanyLookupApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Npgsql;
using System.Text.Json.Nodes;

var builder = WebApplication.CreateBuilder(args);

var databaseUrl = builder.Configuration["DATABASE_URL"]
    ?? builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("DATABASE_URL or DefaultConnection must be configured.");

builder.Services.AddDbContext<CompanyDbContext>(options =>
    options.UseNpgsql(ToNpgsqlConnectionString(databaseUrl)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.EnableAnnotations());

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
app.MapGet("/api-docs/k2/swagger.json", (HttpRequest request) =>
    Results.Json(CreateK2SwaggerDocument(request)));

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

static JsonObject CreateK2SwaggerDocument(HttpRequest request)
{
    return new JsonObject
    {
        ["swagger"] = "2.0",
        ["info"] = new JsonObject
        {
            ["title"] = "CompanyLookupApi",
            ["version"] = "1.0"
        },
        ["host"] = request.Host.Value,
        ["schemes"] = new JsonArray(request.Scheme),
        ["paths"] = new JsonObject
        {
            ["/api/companies/{cif}"] = new JsonObject
            {
                ["get"] = new JsonObject
                {
                    ["tags"] = new JsonArray("CompanyResponse"),
                    ["operationId"] = "GetCompanyByCif",
                    ["produces"] = new JsonArray("application/json"),
                    ["parameters"] = new JsonArray
                    {
                        new JsonObject
                        {
                            ["name"] = "cif",
                            ["in"] = "path",
                            ["required"] = true,
                            ["type"] = "string"
                        }
                    },
                    ["responses"] = new JsonObject
                    {
                        ["200"] = new JsonObject
                        {
                            ["description"] = "OK",
                            ["schema"] = new JsonObject
                            {
                                ["$ref"] = "#/definitions/CompanyResponse"
                            }
                        }
                    }
                }
            }
        },
        ["definitions"] = new JsonObject
        {
            ["CompanyResponse"] = new JsonObject
            {
                ["type"] = "object",
                ["properties"] = new JsonObject
                {
                    ["cif"] = StringProperty(),
                    ["companyName"] = StringProperty(),
                    ["tradingName"] = StringProperty(),
                    ["segment"] = StringProperty(),
                    ["legalEntityName"] = StringProperty(),
                    ["country"] = StringProperty(),
                    ["registrationNumber"] = StringProperty(),
                    ["legalForm"] = StringProperty(),
                    ["dateOfIncorporation"] = new JsonObject
                    {
                        ["type"] = "string",
                        ["format"] = "date-time"
                    },
                    ["taxIdVat"] = StringProperty(),
                    ["address"] = StringProperty(),
                    ["numberOfEmployees"] = new JsonObject
                    {
                        ["type"] = "integer",
                        ["format"] = "int32"
                    },
                    ["industry"] = StringProperty()
                }
            }
        }
    };

    static JsonObject StringProperty() => new() { ["type"] = "string" };
}