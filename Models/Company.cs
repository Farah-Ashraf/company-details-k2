namespace CompanyLookupApi.Models;

public sealed class Company
{
    public int Id { get; set; }
    public string Cif { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string? TradingName { get; set; }
    public string? Segment { get; set; }
    public string? LegalEntityName { get; set; }
    public string? Country { get; set; }
    public string? RegistrationNumber { get; set; }
    public string? LegalForm { get; set; }
    public DateTime? DateOfIncorporation { get; set; }
    public string? TaxIdVat { get; set; }
    public string? Address { get; set; }
    public int? NumberOfEmployees { get; set; }
    public string? Industry { get; set; }
}