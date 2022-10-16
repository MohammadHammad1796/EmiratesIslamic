using Microsoft.EntityFrameworkCore.Migrations;

namespace EmiratesIslamic.Infrastructure.Data.Migrations;

public partial class AddCurrenciesTable : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Currencies",
            columns: table => new
            {
                Code = table.Column<string>(maxLength: 5, nullable: false),
                Name = table.Column<string>(maxLength: 100, nullable: false),
                Buy = table.Column<float>(nullable: false),
                Sell = table.Column<float>(nullable: false),
                IsAvailable = table.Column<bool>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Currencies", x => x.Code);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Currencies");
    }
}