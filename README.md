# Company Lookup API

This ASP.NET Core 8 API retrieves a company by its CIF from PostgreSQL.

## Endpoint

`GET /api/companies/{cif}`

- `200 OK`: returns the company data.
- `404 Not Found`: no company exists with that CIF.

The response includes company name, trading name, segment, legal entity name, country, registration number, legal form, date of incorporation, tax ID/VAT, address, number of employees, and industry.

## Database setup

1. Install the .NET 8 SDK and PostgreSQL.
2. Set the PostgreSQL connection URL before starting the API. Use a user secret or environment variable so the password is not committed:

```powershell
$env:DATABASE_URL = "postgresql://postgres:your-password@localhost:5432/los_system?schema=public"
```

3. Create a migration and database:

```powershell
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate
dotnet ef database update
```

4. Start the API:

```powershell
dotnet run
```

The `Companies` table is mapped by `CompanyDbContext`. CIF is required and unique.
