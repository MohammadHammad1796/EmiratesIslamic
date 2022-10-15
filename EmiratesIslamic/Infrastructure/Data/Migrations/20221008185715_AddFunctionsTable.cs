using Microsoft.EntityFrameworkCore.Migrations;

namespace EmiratesIslamic.Infrastructure.Data.Migrations;

public partial class AddFunctionsTable : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Functions",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                ImagePath = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Functions", x => x.Id);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Functions");
    }
}