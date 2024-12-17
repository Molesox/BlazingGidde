using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazingGidde.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialDbCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Person");

            migrationBuilder.EnsureSchema(
                name: "FlowCheck");

            migrationBuilder.EnsureSchema(
                name: "Patois");

            migrationBuilder.CreateSequence(
                name: "TemplateSequence");

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
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DictionaryEntry",
                schema: "Patois",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Parent = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    FrenchWord = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DialectWord = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FrenchExample = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DialectExample = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AudioId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedVisa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedVisa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictionaryEntry", x => x.Id);
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
                name: "TemplateType",
                schema: "FlowCheck",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateUser = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UpdateUser = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlowUser",
                schema: "FlowCheck",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateUser = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UpdateUser = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowUser_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemplateKind",
                schema: "FlowCheck",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultItemCount = table.Column<int>(type: "int", nullable: false),
                    TemplateTypeId = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateUser = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UpdateUser = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateKind", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplateKind_TemplateType_TemplateTypeId",
                        column: x => x.TemplateTypeId,
                        principalSchema: "FlowCheck",
                        principalTable: "TemplateType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    VatNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AnnualRevenue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PersonTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonID);
                    table.ForeignKey(
                        name: "FK_Person_FlowUser_Id",
                        column: x => x.Id,
                        principalSchema: "FlowCheck",
                        principalTable: "FlowUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Person_PersonType_PersonTypeID",
                        column: x => x.PersonTypeID,
                        principalSchema: "Person",
                        principalTable: "PersonType",
                        principalColumn: "PersonTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomTemplateItem",
                schema: "FlowCheck",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemplateKindId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomTemplateItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomTemplateItem_TemplateKind_TemplateKindId",
                        column: x => x.TemplateKindId,
                        principalSchema: "FlowCheck",
                        principalTable: "TemplateKind",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Template",
                schema: "FlowCheck",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [TemplateSequence]"),
                    FlowUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TemplateKindId = table.Column<int>(type: "int", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateUser = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UpdateUser = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Template_FlowUser_FlowUserId",
                        column: x => x.FlowUserId,
                        principalSchema: "FlowCheck",
                        principalTable: "FlowUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Template_TemplateKind_TemplateKindId",
                        column: x => x.TemplateKindId,
                        principalSchema: "FlowCheck",
                        principalTable: "TemplateKind",
                        principalColumn: "Id");
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
                    Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "TemplateItem",
                schema: "FlowCheck",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Success = table.Column<bool>(type: "bit", nullable: true),
                    Traceability = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Line = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateUser = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UpdateUser = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplateItem_Template_TemplateId",
                        column: x => x.TemplateId,
                        principalSchema: "FlowCheck",
                        principalTable: "Template",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BreakeableItem",
                schema: "FlowCheck",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreakeableItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BreakeableItem_TemplateItem_Id",
                        column: x => x.Id,
                        principalSchema: "FlowCheck",
                        principalTable: "TemplateItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GazItem",
                schema: "FlowCheck",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    O2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    N2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CO2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsGasOk = table.Column<bool>(type: "bit", nullable: true),
                    IsSealedOk = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GazItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GazItem_TemplateItem_Id",
                        column: x => x.Id,
                        principalSchema: "FlowCheck",
                        principalTable: "TemplateItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incidency",
                schema: "FlowCheck",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    IsQualityAdvised = table.Column<bool>(type: "bit", nullable: true),
                    IsMaintenancAdvised = table.Column<bool>(type: "bit", nullable: true),
                    Signature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectiveActions = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidency", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incidency_TemplateItem_Id",
                        column: x => x.Id,
                        principalSchema: "FlowCheck",
                        principalTable: "TemplateItem",
                        principalColumn: "Id",
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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CustomTemplateItem_TemplateKindId",
                schema: "FlowCheck",
                table: "CustomTemplateItem",
                column: "TemplateKindId");

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
                name: "IX_Person_Id",
                schema: "Person",
                table: "Person",
                column: "Id",
                unique: true,
                filter: "[Id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Person_PersonTypeID",
                schema: "Person",
                table: "Person",
                column: "PersonTypeID");

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

            migrationBuilder.CreateIndex(
                name: "IX_Template_FlowUserId",
                schema: "FlowCheck",
                table: "Template",
                column: "FlowUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Template_TemplateKindId",
                schema: "FlowCheck",
                table: "Template",
                column: "TemplateKindId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateItem_TemplateId",
                schema: "FlowCheck",
                table: "TemplateItem",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateKind_TemplateTypeId",
                schema: "FlowCheck",
                table: "TemplateKind",
                column: "TemplateTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BreakeableItem",
                schema: "FlowCheck");

            migrationBuilder.DropTable(
                name: "CustomTemplateItem",
                schema: "FlowCheck");

            migrationBuilder.DropTable(
                name: "DictionaryEntry",
                schema: "Patois");

            migrationBuilder.DropTable(
                name: "Email",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "GazItem",
                schema: "FlowCheck");

            migrationBuilder.DropTable(
                name: "Incidency",
                schema: "FlowCheck");

            migrationBuilder.DropTable(
                name: "PersonProfile",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "Phone",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "AddressType",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "EmailType",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "TemplateItem",
                schema: "FlowCheck");

            migrationBuilder.DropTable(
                name: "Person",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "PhoneType",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "Template",
                schema: "FlowCheck");

            migrationBuilder.DropTable(
                name: "PersonType",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "FlowUser",
                schema: "FlowCheck");

            migrationBuilder.DropTable(
                name: "TemplateKind",
                schema: "FlowCheck");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "TemplateType",
                schema: "FlowCheck");

            migrationBuilder.DropSequence(
                name: "TemplateSequence");
        }
    }
}
