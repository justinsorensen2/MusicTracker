using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MusicTracker.Migrations
{
    public partial class FreshDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BandName = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    NumberOfMembers = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    Style = table.Column<string>(nullable: true),
                    IsSigned = table.Column<bool>(nullable: false),
                    Contact = table.Column<string>(nullable: true),
                    ContactPhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(nullable: true),
                    IsExplicit = table.Column<bool>(nullable: false),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    BandId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Albums_Bands_BandId",
                        column: x => x.BandId,
                        principalTable: "Bands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SongTitle = table.Column<string>(nullable: true),
                    Lyrics = table.Column<string>(nullable: true),
                    Length = table.Column<string>(nullable: true),
                    Genre = table.Column<string>(nullable: true),
                    BandId = table.Column<int>(nullable: false),
                    AlbumId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Songs_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Songs_Bands_BandId",
                        column: x => x.BandId,
                        principalTable: "Bands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_BandId",
                table: "Albums",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_AlbumId",
                table: "Songs",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_BandId",
                table: "Songs",
                column: "BandId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Songs");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "Bands");
        }
    }
}
