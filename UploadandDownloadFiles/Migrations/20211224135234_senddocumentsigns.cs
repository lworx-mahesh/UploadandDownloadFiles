using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UploadandDownloadFiles.Migrations
{
    public partial class senddocumentsigns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.CreateTable(
            ////     name: "AspNetRoles",
            //     columns: table => new
            //     {
            //         Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //         Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //         NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //         ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //    //     table.PrimaryKey("PK_AspNetRoles", x => x.Id);
            //     });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUsers",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false),
            //        Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
            //        PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
            //        TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
            //        LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
            //        LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
            //        AccessFailedCount = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUsers", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "SenddocumentForSigns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Filepath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createddate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmailTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniqueId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Issign = table.Column<bool>(type: "bit", nullable: true),
                    Isdeleted = table.Column<bool>(type: "bit", nullable: true),
                    Signdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SignpdfFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderNumber = table.Column<int>(type: "int", nullable: true),
                    TotalSigner = table.Column<int>(type: "int", nullable: true),
                    DocumentExpireDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorUniqueId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SenddocumentForSign", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SignerColourList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SignerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Filepath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignerOrder = table.Column<int>(type: "int", nullable: true),
                    DocumentUniqueId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignerUniqueID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColorCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignerColourListModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignerColourList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SignerColourList_SignerColourList_SignerColourListModelId",
                        column: x => x.SignerColourListModelId,
                        principalTable: "SignerColourList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            //migrationBuilder.CreateTable(
            //    name: "StoreSignerInfoes",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        SignerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Filepath = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SignerOrder = table.Column<int>(type: "int", nullable: true),
            //        DocumentUniqueId = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SignerUniqueId = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Days = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SignerType = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Message = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_StoreSignerInfoes", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetRoleClaims",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
            //        //table.ForeignKey(
            //        //    name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
            //        //    column: x => x.RoleId,
            //        //    principalTable: "AspNetRoles",
            //        //    principalColumn: "Id",
            //        //    onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUserClaims",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    }
            //    //constraints: table =>
            //    //{
            //    //    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
            //    //    table.ForeignKey(
            //    //        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
            //    //        column: x => x.UserId,
            //    //        principalTable: "AspNetUsers",
            //    //        principalColumn: "Id",
            //    //        onDelete: ReferentialAction.Cascade);
            //    //}
            //    );

            //migrationBuilder.CreateTable(
            //    name: "AspNetUserLogins",
            //    columns: table => new
            //    {
            //        LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            //    }
            //    //constraints: table =>
            //    //{
            //    //    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
            //    //    table.ForeignKey(
            //    //        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
            //    //        column: x => x.UserId,
            //    //        principalTable: "AspNetUsers",
            //    //        principalColumn: "Id",
            //    //        onDelete: ReferentialAction.Cascade);
            //    //}
            //    );

            //migrationBuilder.CreateTable(
            //    name: "AspNetUserRoles",
            //    columns: table => new
            //    {
            //        UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
            //        //table.ForeignKey(
            //        ////    name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
            //        //    column: x => x.RoleId,
            //        ////    principalTable: "AspNetRoles",
            //        //    principalColumn: "Id",
            //        //    onDelete: ReferentialAction.Cascade);
            //        //table.ForeignKey(
            //        //    name: "FK_AspNetUserRoles_AspNetUsers_UserId",
            //        //    column: x => x.UserId,
            //        //    principalTable: "AspNetUsers",
            //        //    principalColumn: "Id",
            //        //    onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUserTokens",
            //    columns: table => new
            //    {
            //        UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
            //        //table.ForeignKey(
            //        //    name: "FK_AspNetUserTokens_AspNetUsers_UserId",
            //        //    column: x => x.UserId,
            //        //    principalTable: "AspNetUsers",
            //        //    principalColumn: "Id",
            //        //    onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetRoleClaims_RoleId",
            //    table: "AspNetRoleClaims",
            //    column: "RoleId");

           // migrationBuilder.CreateIndex(
           //     name: "RoleNameIndex",
           ////     table: "AspNetRoles",
           //     column: "NormalizedName",
           //     unique: true,
           //     filter: "[NormalizedName] IS NOT NULL");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserClaims_UserId",
            //    table: "AspNetUserClaims",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserLogins_UserId",
            //    table: "AspNetUserLogins",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserRoles_RoleId",
            //    table: "AspNetUserRoles",
            //    column: "RoleId");

            //migrationBuilder.CreateIndex(
            //    name: "EmailIndex",
            //    table: "AspNetUsers",
            //    column: "NormalizedEmail");

            //migrationBuilder.CreateIndex(
            //    name: "UserNameIndex",
            //    table: "AspNetUsers",
            //    column: "NormalizedUserName",
            //    unique: true,
            //    filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SignerColourList_SignerColourListModelId",
                table: "SignerColourList",
                column: "SignerColourListModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "AspNetRoleClaims");

            //migrationBuilder.DropTable(
            //    name: "AspNetUserClaims");

            //migrationBuilder.DropTable(
            //    name: "AspNetUserLogins");

            //migrationBuilder.DropTable(
            //    name: "AspNetUserRoles");

            //migrationBuilder.DropTable(
            //    name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "SenddocumentForSigns");

            migrationBuilder.DropTable(
                name: "SignerColourList");

            //migrationBuilder.DropTable(
            //    name: "StoreSignerInfoes");

            //migrationBuilder.DropTable(
            //    name: "AspNetRoles");

            //migrationBuilder.DropTable(
            //    name: "AspNetUsers");
        }
    }
}
