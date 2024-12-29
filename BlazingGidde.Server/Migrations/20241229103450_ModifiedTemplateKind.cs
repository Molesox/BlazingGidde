using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazingGidde.Server.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedTemplateKind : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TemplateCode",
                schema: "FlowCheck",
                table: "CustomTemplateItem");

            migrationBuilder.AddColumn<string>(
                name: "ProgramDescription",
                schema: "FlowCheck",
                table: "TemplateKind",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Section",
                schema: "FlowCheck",
                table: "TemplateKind",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProgramDescription",
                schema: "FlowCheck",
                table: "TemplateKind");

            migrationBuilder.DropColumn(
                name: "Section",
                schema: "FlowCheck",
                table: "TemplateKind");

            migrationBuilder.AddColumn<int>(
                name: "TemplateCode",
                schema: "FlowCheck",
                table: "CustomTemplateItem",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
