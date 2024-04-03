using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Store.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 4, 3, 17, 1, 32, 731, DateTimeKind.Utc).AddTicks(6682)),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 4, 3, 17, 1, 32, 731, DateTimeKind.Utc).AddTicks(7553)),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 4, 3, 17, 1, 32, 732, DateTimeKind.Utc).AddTicks(728)),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 4, 3, 17, 1, 32, 732, DateTimeKind.Utc).AddTicks(1129)),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentPricePerUnit = table.Column<double>(type: "float", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 4, 3, 17, 1, 32, 733, DateTimeKind.Utc).AddTicks(3551)),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 4, 3, 17, 1, 32, 733, DateTimeKind.Utc).AddTicks(4063)),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    TotalSum = table.Column<double>(type: "float", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 4, 3, 17, 1, 32, 732, DateTimeKind.Utc).AddTicks(3092)),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 4, 3, 17, 1, 32, 732, DateTimeKind.Utc).AddTicks(3540))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderLines",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductAmount = table.Column<double>(type: "float", nullable: false),
                    PricePerUnit = table.Column<double>(type: "float", nullable: false),
                    TotalSum = table.Column<double>(type: "float", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 4, 3, 17, 1, 32, 732, DateTimeKind.Utc).AddTicks(6700)),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 4, 3, 17, 1, 32, 732, DateTimeKind.Utc).AddTicks(7194))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLines", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderLines_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderLines_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Ноутбуки; Комп'ютери, неттопи, моноблоки; Монітори, Gaming, Планшети, ...", "Ноутбуки та комп'ютери" },
                    { 2, "", "Смартфони, ТВ та електроніка" },
                    { 3, "Пральні маниши; Холодильники; Праски; тощо", "Побутова технік" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "", "Олійник Іван Петрови" },
                    { 2, "", "Левченко Марія Григорівна" },
                    { 3, "", "Поліщук Микола Вікторович" },
                    { 4, "", "Омельяненко Григорій Пилипович" },
                    { 5, "", "Стеценко Вікторія Федорівна" },
                    { 6, "", "Налийборщ Омельян Францевич" },
                    { 7, "", "Неїжхліб Олена Юхимівна" },
                    { 8, "", "Майборода Віталій Семенович" },
                    { 9, "", "Черезтинногозадерищенко Ульяна Юстимівна" },
                    { 10, "", "Дубовий Віктор Олегович" },
                    { 11, "", "Підгорний Вадим Сергійович" },
                    { 12, "", "Озерний Сергій Іванович" },
                    { 13, "", "Нечипоренко Ульяна Омельянівна" },
                    { 14, "", "Задорожній Олег Олександрович" },
                    { 15, "", "Миколаєнко Олександра Вікторівна" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "Notes", "TotalSum" },
                values: new object[,]
                {
                    { 1, 1, "Для роботи", 0.0 },
                    { 2, 2, "Подарунки на день народження", 0.0 },
                    { 3, 3, "Дружині", 0.0 },
                    { 4, 4, "Щоб було", 0.0 },
                    { 5, 5, "Для нової оселі", 0.0 },
                    { 6, 6, "", 0.0 },
                    { 7, 7, "", 0.0 },
                    { 8, 8, "", 0.0 },
                    { 9, 9, "", 0.0 },
                    { 10, 10, "", 0.0 },
                    { 11, 11, "", 0.0 },
                    { 12, 12, "", 0.0 },
                    { 13, 13, "", 0.0 },
                    { 14, 14, "", 0.0 },
                    { 15, 15, "", 0.0 },
                    { 16, 6, "", 0.0 },
                    { 17, 7, "", 0.0 },
                    { 18, 8, "", 0.0 },
                    { 19, 9, "", 0.0 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CurrentPricePerUnit", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 1, 25999.0, "Екран 16\" IPS (1920x1200) WUXGA, матовий / Intel Core i5-12450H (2.0 - 4.4 ГГц) / RAM 16 ГБ / SSD 512 ГБ / Intel UHD Graphics / без ОД / Wi-Fi / Bluetooth / веб-камера / без ОС / 1.89 кг / сірий", "Ноутбук Lenovo IdeaPad Slim 5 16IAH8 (83BG001ARA) Cloud Grey / 16\" IPS WUXGA / Intel Core i5-12450H / RAM 16 ГБ / SSD 512 ГБ / Підсвічування клавіатури / Зарядка через Type-C" },
                    { 2, 1, 32999.0, "Екран 15.6\" IPS (1920x1080) Full HD, матовий / Intel Core i5-12450H (2.0 - 4.4 ГГц) / RAM 16 ГБ / SSD 512 ГБ / nVidia GeForce RTX 3050, 4 ГБ / без ОД / LAN / Wi-Fi / Bluetooth / веб-камера / без ОС / 2.1 кг / чорний", "Ноутбук Acer Aspire 7 A715-76G-560W (NH.QMMEU.002) Charcoal Black / Intel Core i5-12450H / RAM 16 ГБ / SSD 512 ГБ / nVidia GeForce RTX 3050, 4 ГБ / Підсвітка клавіатури" },
                    { 3, 1, 35499.0, "Екран 15.6\" IPS (1920x1080) Full HD 144 Гц, матовий / Intel Core i5-12500H (2.5 - 4.5 ГГц) / RAM 16 ГБ / SSD 512 ГБ / nVidia GeForce RTX 3050, 4 ГБ / без ОД / LAN / Wi-Fi / Bluetooth / вебкамера / без ОС / 2.2 кг / сірий", "Ноутбук ASUS TUF Gaming F15 (2022) FX507ZC4-HN087 (90NR0GW1-M00HJ0) Mecha Gray / 15.6\" IPS Full HD 144 Гц / Intel Core i5-12500H / RAM 16 ГБ / SSD 512 ГБ / nVidia GeForce RTX 3050" },
                    { 4, 1, 43499.0, "Екран 13.3\" Retina (2560x1600) WQXGA, глянсовий / Apple M1 / RAM 8 ГБ / SSD 256 ГБ / Apple M1 Graphics / Wi-Fi / Bluetooth / macOS Big Sur / 1.29 кг / сірий", "Ноутбук Apple MacBook Air 13\" M1 8/256GB 2020 (MGN63) Space Gray" },
                    { 5, 1, 22999.0, "Экран 15.6\" IPS (1920x1080) Full HD, матовый / Intel Core i5-1235U (0.9 - 4.4 ГГц) / RAM 16 ГБ / SSD 512 ГБ / Intel Iris Xe Graphics / без ОД / Wi-Fi / Bluetooth / веб-камера / DOS / 1.69 кг / синий с серебристым", "Ноутбук HP Laptop 15s-fq5032ua (8F322EA) Spruce Blue / Intel Core i5-1235U / RAM 16 ГБ / SSD 512 ГБ" },
                    { 6, 2, 7599.0, "Екран (6.5\", Super AMOLED, 2340x1080) / Mediatek Helio G99 (2 x 2.6 ГГц + 6 x 2.0 ГГц) / основна потрійна камера: 50 Мп + 5 Мп + 2 Мп, фронтальна камера: 13 Мп / RAM 6 ГБ / 128 ГБ вбудованої пам'яті + microSD (до 1 ТБ) / 3G / LTE / GPS / ГЛОНАСС / BDS / підтримка 2х SIM-карток (Nano-SIM) / Android 13 / 5000 мА*год", "Мобільний телефон Samsung Galaxy A24 6/128GB Black (SM-A245FZKVSEK)" },
                    { 7, 2, 20999.0, "Екран (50\", 3840x2160) / WebOS", "Телевізор LG 50UR81006LJ" },
                    { 8, 2, 6999.0, "Екран 10.61\" IPS (2000x1200), MultiTouch / Qualcomm Snapdragon 680 (2.4 ГГц + 1.9 ГГц) / RAM 4 ГБ / 64 ГБ вбудованої пам'яті + microSD / Wi-Fi / Bluetooth 5.1 / основная камера 8 Мп + фронтальна - 8 Мп / GPS / ГЛОНАСС / Android 12 / 465 г / сірий", "Планшет Lenovo Tab M10 Plus (3rd Gen) 4/64 Wi-Fi Storm Grey (ZAAM0190UA) + чохол у комплекті!" },
                    { 9, 2, 38999.0, "1.024 кВт/год / LiFePO4/ Швидке заряджання батареї", "Зарядна станція EcoFlow DELTA 2 (ZMR330-EU)" },
                    { 10, 2, 6999.0, "Екран (1.43\", AMOLED) / 475 мА·год", "Смарт-годинник Amazfit GTR 4 Superspeed Black (955544)" },
                    { 11, 3, 16499.0, "6.5 кг / 85 х 60 х 44", "Пральна машина вузька LG F2J3WS2W" },
                    { 12, 3, 35999.0, "Сухе та вологе прибирання, док станція", "Робот-пилосос Xiaomi Robot Vacuum X10+ EU" },
                    { 13, 3, 20555.0, "378 л + 248 л / Двокамерний, інверторний / No Frost / 191.2 х 59.6 х 67.8 см", "Двокамерний холодильник Whirlpool W7X 82O OX H" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_ProductId",
                table: "OrderLines",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderLines");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
