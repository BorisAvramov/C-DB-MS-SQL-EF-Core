using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P02_FootballBetting.Data.Migrations
{
    public partial class correctionNamesOfDbSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bets_USers_UserId",
                table: "Bets");

            migrationBuilder.DropForeignKey(
                name: "FK_Towns_Countryies_CountryId",
                table: "Towns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_USers",
                table: "USers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countryies",
                table: "Countryies");

            migrationBuilder.RenameTable(
                name: "USers",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Countryies",
                newName: "Countries");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bets_Users_UserId",
                table: "Bets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Towns_Countries_CountryId",
                table: "Towns",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bets_Users_UserId",
                table: "Bets");

            migrationBuilder.DropForeignKey(
                name: "FK_Towns_Countries_CountryId",
                table: "Towns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "USers");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "Countryies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_USers",
                table: "USers",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countryies",
                table: "Countryies",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bets_USers_UserId",
                table: "Bets",
                column: "UserId",
                principalTable: "USers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Towns_Countryies_CountryId",
                table: "Towns",
                column: "CountryId",
                principalTable: "Countryies",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
