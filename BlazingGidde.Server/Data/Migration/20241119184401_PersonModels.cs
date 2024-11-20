using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazingGidde.Server.Data.Migration
{
    /// <inheritdoc />
    public partial class PersonModels :  Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Person");

            migrationBuilder.CreateTable(
                name: "AddressType",
                schema: "Person",
                columns: table => new
                {
                    AddressTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SortKey = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressType", x => x.AddressTypeID);
                });

            migrationBuilder.CreateTable(
                name: "EmailType",
                schema: "Person",
                columns: table => new
                {
                    EmailTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SortKey = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailType", x => x.EmailTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                schema: "Person",
                columns: table => new
                {
                    PersonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Culture = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    VatNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AnnualRevenue = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonID);
                });

            migrationBuilder.CreateTable(
                name: "PersonType",
                schema: "Person",
                columns: table => new
                {
                    PersonTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SortKey = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonType", x => x.PersonTypeID);
                });

            migrationBuilder.CreateTable(
                name: "PhoneType",
                schema: "Person",
                columns: table => new
                {
                    PhoneTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SortKey = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneType", x => x.PhoneTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                schema: "Person",
                columns: table => new
                {
                    AddressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<int>(type: "int", nullable: false),
                    AddressTypeID = table.Column<int>(type: "int", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    AddressLine3 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SortKey = table.Column<int>(type: "int", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressID);
                    table.ForeignKey(
                        name: "FK_Address_AddressType_AddressTypeID",
                        column: x => x.AddressTypeID,
                        principalSchema: "Person",
                        principalTable: "AddressType",
                        principalColumn: "AddressTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Address_Person_PersonID",
                        column: x => x.PersonID,
                        principalSchema: "Person",
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Email",
                schema: "Person",
                columns: table => new
                {
                    EmailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<int>(type: "int", nullable: false),
                    EmailTypeID = table.Column<int>(type: "int", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SortKey = table.Column<int>(type: "int", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Email", x => x.EmailID);
                    table.ForeignKey(
                        name: "FK_Email_EmailType_EmailTypeID",
                        column: x => x.EmailTypeID,
                        principalSchema: "Person",
                        principalTable: "EmailType",
                        principalColumn: "EmailTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Email_Person_PersonID",
                        column: x => x.PersonID,
                        principalSchema: "Person",
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonProfile",
                schema: "Person",
                columns: table => new
                {
                    PersonProfileID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<int>(type: "int", nullable: false),
                    Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonProfile", x => x.PersonProfileID);
                    table.ForeignKey(
                        name: "FK_PersonProfile_Person_PersonID",
                        column: x => x.PersonID,
                        principalSchema: "Person",
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phone",
                schema: "Person",
                columns: table => new
                {
                    PhoneID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<int>(type: "int", nullable: false),
                    PhoneTypeID = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SortKey = table.Column<int>(type: "int", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phone", x => x.PhoneID);
                    table.ForeignKey(
                        name: "FK_Phone_Person_PersonID",
                        column: x => x.PersonID,
                        principalSchema: "Person",
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Phone_PhoneType_PhoneTypeID",
                        column: x => x.PhoneTypeID,
                        principalSchema: "Person",
                        principalTable: "PhoneType",
                        principalColumn: "PhoneTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_AddressTypeID",
                schema: "Person",
                table: "Address",
                column: "AddressTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Address_PersonID",
                schema: "Person",
                table: "Address",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Email_EmailTypeID",
                schema: "Person",
                table: "Email",
                column: "EmailTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Email_PersonID",
                schema: "Person",
                table: "Email",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonProfile_PersonID",
                schema: "Person",
                table: "PersonProfile",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Phone_PersonID",
                schema: "Person",
                table: "Phone",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Phone_PhoneTypeID",
                schema: "Person",
                table: "Phone",
                column: "PhoneTypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "Email",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "PersonProfile",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "PersonType",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "Phone",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "AddressType",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "EmailType",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "Person",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "PhoneType",
                schema: "Person");
        }
    }
}
