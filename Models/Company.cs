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
    public string? OwnerName { get; set; }
    public decimal? OwnershipPercentage { get; set; }
    public string? NationalId { get; set; }
    public string? BoardOfDirectors { get; set; }
    public string? SignatoryAuthority { get; set; }
    public decimal? AnnualTurnover { get; set; }
    public decimal? WorkingCapital { get; set; }
    public decimal? YtdTurnover { get; set; }
    public decimal? NetProfit { get; set; }
    public decimal? TotalAssets { get; set; }
    public decimal? TotalLiabilities { get; set; }
    public decimal? ShareholdersEquity { get; set; }
}