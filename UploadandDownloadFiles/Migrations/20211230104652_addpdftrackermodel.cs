using Microsoft.EntityFrameworkCore.Migrations;

namespace UploadandDownloadFiles.Migrations
{
    public partial class addpdftrackermodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        //    migrationBuilder.DropTable(
        //        name: "SavePdfBtnValue");

        //    migrationBuilder.CreateTable(
        //        name: "SavePdfBtnValues",
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
        //            table.PrimaryKey("PK_SavePdfBtnValues", x => x.Id);
        //        });
        //}

        //protected override void Down(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.DropTable(
        //        name: "SavePdfBtnValues");

        //    migrationBuilder.CreateTable(
        //        name: "SavePdfBtnValue",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            ButtonHeight = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            ButtonOffsetLeft = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            ButtonOffsetTop = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            ButtonValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            ButtonWidth = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            ColorCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            ControlFieldId = table.Column<int>(type: "int", nullable: false),
        //            ControllerUniqueId = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            DocumentUniqueId = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            UserOrderId = table.Column<int>(type: "int", nullable: false),
        //            controllerid = table.Column<string>(type: "nvarchar(max)", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_SavePdfBtnValue", x => x.Id);
        //        });
        }
    }
}
