using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazingGidde.Server.Data.Migration
{
    /// <inheritdoc />
    public partial class Patois : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Patois");

            migrationBuilder.AlterColumn<string>(
                name: "VatNumber",
                schema: "Person",
                table: "Person",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                schema: "Person",
                table: "Person",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateTable(
                name: "DictionaryEntry",
                schema: "Patois",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Parent = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    FrenchWord = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DialectWord = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FrenchExample = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DialectExample = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AudioId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedVisa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedVisa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictionaryEntry", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DictionaryEntry",
                schema: "Patois");

            migrationBuilder.AlterColumn<string>(
                name: "VatNumber",
                schema: "Person",
                table: "Person",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                schema: "Person",
                table: "Person",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);
        }
    }
}
