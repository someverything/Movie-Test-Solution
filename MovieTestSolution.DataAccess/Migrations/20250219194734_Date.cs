using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTestSolution.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Date : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_ReleaseDate_ReleaseDateId",
                table: "Movies");

            migrationBuilder.DropTable(
                name: "ReleaseDate");

            migrationBuilder.DropIndex(
                name: "IX_Movies_ReleaseDateId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "DateId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ReleaseDateId",
                table: "Movies");

            migrationBuilder.AddColumn<int>(
                name: "Date",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Movies");

            migrationBuilder.AddColumn<Guid>(
                name: "DateId",
                table: "Movies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ReleaseDateId",
                table: "Movies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ReleaseDate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseDate", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_ReleaseDateId",
                table: "Movies",
                column: "ReleaseDateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_ReleaseDate_ReleaseDateId",
                table: "Movies",
                column: "ReleaseDateId",
                principalTable: "ReleaseDate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
