using CompanyLookupApi.Data;
using CompanyLookupApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyLookupApi.Controllers;

[ApiController]
[Route("api/companies")]
public sealed class CompaniesController(CompanyDbContext dbContext) : ControllerBase
{
    [HttpGet("{cif}")]
    [ProducesResponseType(typeof(CompanyResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CompanyResponse>> GetByCif(string cif, CancellationToken cancellationToken)
    {
        var company = await dbContext.Companies
            .AsNoTracking()
            .Where(item => item.Cif == cif)
            .Select(item => new CompanyResponse(
                item.Cif,
                item.CompanyName,
                item.TradingName,
                item.Segment,
                item.LegalEntityName,
                item.Country,
                item.RegistrationNumber,
                item.LegalForm,
                item.DateOfIncorporation,
                item.TaxIdVat,
                item.Address,
                item.NumberOfEmployees,
                item.Industry))
            .SingleOrDefaultAsync(cancellationToken);

        return company is null ? NotFound() : company;
    }
}