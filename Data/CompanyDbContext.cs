using CompanyLookupApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyLookupApi.Data;

public sealed class CompanyDbContext(DbContextOptions<CompanyDbContext> options) : DbContext(options)
{
    public DbSet<Company> Companies => Set<Company>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.ToTable("Companies");
            entity.HasKey(company => company.Id);
            entity.HasIndex(company => company.Cif).IsUnique();
            entity.Property(company => company.Cif).HasMaxLength(50).IsRequired();
            entity.Property(company => company.CompanyName).HasMaxLength(255).IsRequired();
            entity.Property(company => company.TradingName).HasMaxLength(255);
            entity.Property(company => company.Segment).HasMaxLength(100);
            entity.Property(company => company.LegalEntityName).HasMaxLength(255);
            entity.Property(company => company.Country).HasMaxLength(100);
            entity.Property(company => company.RegistrationNumber).HasMaxLength(100);
            entity.Property(company => company.LegalForm).HasMaxLength(100);
            entity.Property(company => company.TaxIdVat).HasMaxLength(100);
            entity.Property(company => company.Address).HasMaxLength(500);
            entity.Property(company => company.Industry).HasMaxLength(150);
            entity.Property(company => company.OwnerName).HasMaxLength(255);
            entity.Property(company => company.OwnershipPercentage).HasColumnType("numeric(5,2)");
            entity.Property(company => company.NationalId).HasMaxLength(50);
            entity.Property(company => company.BoardOfDirectors).HasMaxLength(1000);
            entity.Property(company => company.SignatoryAuthority).HasMaxLength(500);
            entity.Property(company => company.AnnualTurnover).HasColumnType("numeric(18,2)");
            entity.Property(company => company.WorkingCapital).HasColumnType("numeric(18,2)");
            entity.Property(company => company.YtdTurnover).HasColumnType("numeric(18,2)");
            entity.Property(company => company.NetProfit).HasColumnType("numeric(18,2)");
            entity.Property(company => company.TotalAssets).HasColumnType("numeric(18,2)");
            entity.Property(company => company.TotalLiabilities).HasColumnType("numeric(18,2)");
            entity.Property(company => company.ShareholdersEquity).HasColumnType("numeric(18,2)");
        });
    }
}