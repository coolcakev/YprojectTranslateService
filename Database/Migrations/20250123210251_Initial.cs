using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YprojectTranslateService.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LocalizationKey = table.Column<string>(type: "text", nullable: false),
                    Namespace = table.Column<string>(type: "text", nullable: false),
                    Language = table.Column<int>(type: "integer", nullable: false),
                    TranslationText = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Translations");
        }
    }
}
