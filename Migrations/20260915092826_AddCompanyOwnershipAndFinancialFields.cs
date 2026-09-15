using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyLookupApi.Migrations
{
    /// <inheritdoc />
    public partial class AddCompanyOwnershipAndFinancialFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AnnualTurnover",
                table: "Companies",
                type: "numeric(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BoardOfDirectors",
                table: "Companies",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalId",
                table: "Companies",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "NetProfit",
                table: "Companies",
                type: "numeric(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerName",
                table: "Companies",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OwnershipPercentage",
                table: "Companies",
                type: "numeric(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ShareholdersEquity",
                table: "Companies",
                type: "numeric(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignatoryAuthority",
                table: "Companies",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAssets",
                table: "Companies",
                type: "numeric(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalLiabilities",
                table: "Companies",
                type: "numeric(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WorkingCapital",
                table: "Companies",
                type: "numeric(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "YtdTurnover",
                table: "Companies",
                type: "numeric(18,2)",
                nullable: true);

            migrationBuilder.Sql("""
                UPDATE "Companies"
                SET "OwnerName" = 'Ahmed Hassan',
                    "OwnershipPercentage" = 55.50,
                    "NationalId" = '29801011234567',
                    "BoardOfDirectors" = 'Ahmed Hassan; Sara Ali',
                    "SignatoryAuthority" = 'Ahmed Hassan',
                    "AnnualTurnover" = 12500000.00,
                    "WorkingCapital" = 3200000.00,
                    "YtdTurnover" = 8400000.00,
                    "NetProfit" = 1750000.00,
                    "TotalAssets" = 25000000.00,
                    "TotalLiabilities" = 9000000.00,
                    "ShareholdersEquity" = 16000000.00;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnnualTurnover",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "BoardOfDirectors",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "NationalId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "NetProfit",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "OwnerName",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "OwnershipPercentage",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ShareholdersEquity",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "SignatoryAuthority",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "TotalAssets",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "TotalLiabilities",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "WorkingCapital",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "YtdTurnover",
                table: "Companies");
        }
    }
}
