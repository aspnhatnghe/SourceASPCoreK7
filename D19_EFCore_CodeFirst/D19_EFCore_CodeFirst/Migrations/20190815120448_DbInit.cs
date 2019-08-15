using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace D19_EFCore_CodeFirst.Migrations
{
    public partial class DbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Loai",
                columns: table => new
                {
                    MaLoai = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenLoai = table.Column<string>(maxLength: 50, nullable: false),
                    MoTa = table.Column<string>(nullable: true),
                    Hinh = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loai", x => x.MaLoai);
                });

            migrationBuilder.CreateTable(
                name: "HangHoa",
                columns: table => new
                {
                    MaHH = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenHH = table.Column<string>(maxLength: 50, nullable: false),
                    Hinh = table.Column<string>(nullable: true),
                    MoTa = table.Column<string>(nullable: true),
                    DonGia = table.Column<double>(nullable: false),
                    SoLuong = table.Column<int>(nullable: false),
                    MaLoai = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangHoa", x => x.MaHH);
                    table.ForeignKey(
                        name: "FK_HangHoa_Loai_MaLoai",
                        column: x => x.MaLoai,
                        principalTable: "Loai",
                        principalColumn: "MaLoai",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HangHoa_MaLoai",
                table: "HangHoa",
                column: "MaLoai");

            //tạo dữ liệu mẫu
            migrationBuilder.InsertData(
                //table
                "Loai",
                //columns
                new[] { "TenLoai", "MoTa" },
                //values
                new object[] {
                    "Điện thoại", "Các loại điện thoại"
                });
            migrationBuilder.InsertData(
                //table
                "Loai",
                //columns
                new[] { "TenLoai", "MoTa" },
                //values
                new object[] {
                    "Máy tính bảng", "Các máy tính bảng"
                });
            migrationBuilder.InsertData(
                //table
                "Loai",
                //columns
                new[] { "TenLoai", "MoTa" },
                //values
                new object[] {
                    "Laptop", "Các laptop"
                });
        }//end Up()

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HangHoa");

            migrationBuilder.DropTable(
                name: "Loai");
        }
    }
}
