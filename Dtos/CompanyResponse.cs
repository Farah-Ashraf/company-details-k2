namespace CompanyLookupApi.Dtos;

public sealed record CompanyResponse(
    string Cif,
    string CompanyName,
    string? TradingName,
    string? Segment,
    string? LegalEntityName,
    string? Country,
    string? RegistrationNumber,
    string? LegalForm,
    DateTime? DateOfIncorporation,
    string? TaxIdVat,
    string? Address,
    int? NumberOfEmployees,
    string? Industry);