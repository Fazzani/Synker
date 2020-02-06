using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Synker.Persistence.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "transaction_timestamp()"),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "transaction_timestamp()"),
                    Email = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlaylistDataSources",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    State = table.Column<byte>(nullable: false),
                    UserId = table.Column<long>(nullable: true),
                    PlaylistDataSourceFormat = table.Column<short>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Uri_Url = table.Column<string>(nullable: true),
                    Server_Url = table.Column<string>(nullable: true),
                    Authentication_User = table.Column<string>(maxLength: 255, nullable: true),
                    Authentication_Password = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistDataSources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaylistDataSources_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "transaction_timestamp()"),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "transaction_timestamp()"),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    State = table.Column<byte>(nullable: false),
                    UserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Playlists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "transaction_timestamp()"),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "transaction_timestamp()"),
                    Url_Url = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    Position = table.Column<int>(nullable: false),
                    PlaylistId = table.Column<long>(nullable: true),
                    Labels = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Media_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Media_DisplayName",
                table: "Media",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_Media_PlaylistId",
                table: "Media",
                column: "PlaylistId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistDataSources_UserId",
                table: "PlaylistDataSources",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_Name",
                table: "Playlists",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_UserId",
                table: "Playlists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "PlaylistDataSources");

            migrationBuilder.DropTable(
                name: "Playlists");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
