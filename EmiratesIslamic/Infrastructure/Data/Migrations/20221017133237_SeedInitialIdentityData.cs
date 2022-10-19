using Microsoft.EntityFrameworkCore.Migrations;

namespace EmiratesIslamic.Infrastructure.Data.Migrations;

public partial class SeedInitialIdentityData : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "AspNetRoles",
            columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            values: new object[,]
            {
                { 1, "3266ad79-95bf-440d-b98e-dc000478da9b", "Admin", "ADMIN" },
                { 2, "0f5afe8d-bacb-4926-9e9d-193f71f22e6b", "Sales Supervisor", "SALES SUPERVISOR" },
                { 3, "f6014079-d224-4434-83d2-8634c6972ba0", "Services Supervisor", "SERVICES SUPERVISOR" }
            });

        migrationBuilder.InsertData(
            table: "AspNetUsers",
            columns: new[] { "Id", "AccessFailedCount", "Email", "EmailConfirmed", "LockoutEnabled",
                "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumberConfirmed",
                "UserName", "TwoFactorEnabled", "SecurityStamp", "ConcurrencyStamp",
                "FullName" },
            values: new object[] { 1, 0,
                "MohammadHammad1796@gmail.com",
                true, false, "MOHAMMADHAMMAD1796@GMAIL.COM", "MOHAMMADHAMMAD1796@GMAIL.COM",
                "AQAAAAEAACcQGtp0Cqz1YT1G62YlvAAAAEChLudze8aHzpsDJEop1+EubZLjweYyJ2YKndadz4KznvIfgw==",
                false,"MohammadHammad1796@gmail.com", false , "sadasdas+A54", "we58eqw84+*SOf",
                "Mohammad Hammad"});

        migrationBuilder.InsertData(
            table: "AspNetUserRoles",
            columns: new[] { "UserId", "RoleId" },
            values: new object[,]
            {
                { 1, 1 }
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("DELETE FROM AspNetUserRoles");

        migrationBuilder.Sql("DELETE FROM AspNetUsers");

        migrationBuilder.Sql("DELETE FROM AspNetRoles");
    }
}