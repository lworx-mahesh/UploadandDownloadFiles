using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UploadandDownloadFiles.Migrations
{
    public partial class saveasixrecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //    migrationBuilder.CreateTable(
            //        name: "RecipientSignerForDocument",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(type: "int", nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            SignerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            Filepath = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            SignerOrder = table.Column<int>(type: "int", nullable: true),
            //            Signdate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //            DocumentUniqueId = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            IsSign = table.Column<bool>(type: "bit", nullable: true),
            //            Ipaddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            ParentsentId = table.Column<int>(type: "int", nullable: true),
            //            Createdate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //            Isdeleted = table.Column<bool>(type: "bit", nullable: true),
            //            IsMailSend = table.Column<bool>(type: "bit", nullable: true),
            //            MailSendDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //            SignPdfFilepath = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            SingerUniqueId = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            CreatorUniqueId = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_RecipientSignerForDocument", x => x.Id);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "SaveControlAxis",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(type: "int", nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            ButtonHeight = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            ButtonOffsetLeft = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            ButtonOffsetTop = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            ButtonWidth = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            controllerid = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            DocumentUniqueId = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            ControllerUniqueId = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            ControlFieldId = table.Column<int>(type: "int", nullable: false),
            //            ColorCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            UserOrderId = table.Column<int>(type: "int", nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_SaveControlAxis", x => x.Id);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "SavePdfBtnValue",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(type: "int", nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            ButtonHeight = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            ButtonOffsetLeft = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            ButtonOffsetTop = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            ButtonWidth = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            controllerid = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            DocumentUniqueId = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            ControllerUniqueId = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            ControlFieldId = table.Column<int>(type: "int", nullable: false),
            //            ColorCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            UserOrderId = table.Column<int>(type: "int", nullable: false),
            //            ButtonValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_SavePdfBtnValue", x => x.Id);
            //        });
            //}

            //protected override void Down(MigrationBuilder migrationBuilder)
            //{
            //    migrationBuilder.DropTable(
            //        name: "RecipientSignerForDocument");

            //    migrationBuilder.DropTable(
            //        name: "SaveControlAxis");

            //    migrationBuilder.DropTable(
            //        name: "SavePdfBtnValue");
            //}
             }
        }
}