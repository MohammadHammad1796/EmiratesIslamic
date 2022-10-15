using Microsoft.EntityFrameworkCore.Migrations;

namespace EmiratesIslamic.Infrastructure.Data.Migrations;

public partial class AddOffersTable : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Offers",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Title = table.Column<string>(maxLength: 100, nullable: false),
                Text = table.Column<string>(maxLength: 250, nullable: false),
                ImagePath = table.Column<string>(maxLength: 250, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Offers", x => x.Id);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Offers");
    }
}