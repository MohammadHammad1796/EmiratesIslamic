using Microsoft.EntityFrameworkCore.Migrations;

namespace EmiratesIslamic.Infrastructure.Data.Migrations;

public partial class UpdateAdminPassword : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // password is Te$t4You
        migrationBuilder.UpdateData("AspNetUsers", "Id", 1, "PasswordHash",
            "AQAAAAEAACcQAAAAEHbbLf3EfIbRVyCHVBtqn1WXBrFa1EJcTIWhds7PDDl7k2w66+71GA1ow1lavr3F1Q==");
        migrationBuilder.UpdateData("AspNetUsers", "Id", 1, "SecurityStamp",
            "BEIWUPUXYVSNISIHCIJVH7IJBTOYEA4T");
        migrationBuilder.UpdateData("AspNetUsers", "Id", 1, "ConcurrencyStamp",
            "1e6a4f38-1387-4472-82ef-1fa8ab6e99f2");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpdateData("AspNetUsers", "Id", 1, "PasswordHash",
            "AQAAAAEAACcQGtp0Cqz1YT1G62YlvAAAAEChLudze8aHzpsDJEop1+EubZLjweYyJ2YKndadz4KznvIfgw==");
        migrationBuilder.UpdateData("AspNetUsers", "Id", 1, "SecurityStamp",
            "sadasdas+A54");
        migrationBuilder.UpdateData("AspNetUsers", "Id", 1, "ConcurrencyStamp",
            "we58eqw84+*SOf");
    }
}