using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class dataSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplaySeqNo", "IsFeatured", "ParentCategoryID", "PictureID", "SanitizedName" },
                values: new object[] { 1, 1, false, null, null, "telefon" });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "IconCode", "IsDefault", "IsEnabled", "Name", "ResourceFileName", "ShortCode" },
                values: new object[] { 1, "az.png", false, true, "Azərbaijan", null, "az" });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "IconCode", "IsDefault", "IsEnabled", "Name", "ResourceFileName", "ShortCode" },
                values: new object[] { 2, "ru.png", false, true, "Russian", null, "ru" });

            migrationBuilder.InsertData(
                table: "CategoryRecords",
                columns: new[] { "Id", "CategoryID", "Description", "LanguageID", "Name", "Summary" },
                values: new object[] { 1, 1, "Telefon Desc", 0, "Telefon", "TEl Sum" });

            migrationBuilder.InsertData(
                table: "CategoryRecords",
                columns: new[] { "Id", "CategoryID", "Description", "LanguageID", "Name", "Summary" },
                values: new object[] { 2, 1, "Телефон Desc", 0, "Телефон", "Телефон Sum" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CoverPhotoId", "Discount", "InStock", "IsDay", "IsDeleted", "IsFeatured", "IsSlider", "ModifiedOn", "Price", "PublishDate", "Rating" },
                values: new object[] { 1, 1, 1, 1900m, 10, false, false, true, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2000m, new DateTime(2022, 3, 2, 4, 7, 44, 83, DateTimeKind.Local).AddTicks(6187), 4.5m });

            migrationBuilder.InsertData(
                table: "ProductRecords",
                columns: new[] { "Id", "Description", "LanguageId", "Name", "ProductId", "Summary" },
                values: new object[] { 1, "Diqqət mərkəzində olmağı xoşlayırsınızsa yeni SAMSUNG GALAXY Z FOLD 3 smartfonu sizin üçündür", 1, "Samsung Galaxy Z Fold 3 12/256GB Green (SM-F926B)", 1, "Gözəl insan" });

            migrationBuilder.InsertData(
                table: "ProductRecords",
                columns: new[] { "Id", "Description", "LanguageId", "Name", "ProductId", "Summary" },
                values: new object[] { 2, "Новинка, если вы любите быть в центре вниманияСмартфон SAMSUNG GALAXY Z FOLD 3 для вас", 2, "RU Samsung Galaxy Z Fold 3 12/256GB Green (SM-F926B)", 1, "красивый человек" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CategoryRecords",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CategoryRecords",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductRecords",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductRecords",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
