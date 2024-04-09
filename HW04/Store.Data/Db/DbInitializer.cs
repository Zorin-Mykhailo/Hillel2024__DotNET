using Microsoft.EntityFrameworkCore;
using Store.Data.Entities;

namespace Store.Data.Db;

internal class DbInitializer(ModelBuilder modelBuilder)
{
    public void Seed()
    {
        string result = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") switch
        {
            "Development" => SeedForDevelopment(),
            "Staging" => SeedForStaging(),
            "Production" => SeedForProduction(),
            _ => "Unknown ASPNETCORE_ENVIRONMENT value"
        };
        Console.WriteLine(result);        
    }

    private string SeedForDevelopment()
    {
        SeedCustomers();
        SeedCategories();
        SeedProducts();
        SeedOrders();
        //SeedOrderLines();
        return "Seeding for development environment finished";
    }

    private string SeedForStaging() => "Seeding for staging environment not supported";

    private string SeedForProduction() => "Seeding for development environment not supported";

    private void SeedCustomers()
    {
        modelBuilder.Entity<Customer>(e =>
        {
            e.HasData(new Customer
            {
                Id = 1,
                Name = "Олійник Іван Петрови",
                Description = "",
            });
            e.HasData(new Customer
            {
                Id = 2,
                Name = "Левченко Марія Григорівна",
                Description = "",
            });
            e.HasData(new Customer
            {
                Id = 3,
                Name = "Поліщук Микола Вікторович",
                Description = "",
            });
            e.HasData(new Customer
            {
                Id = 4,
                Name = "Омельяненко Григорій Пилипович",
                Description = "",
            });
            e.HasData(new Customer
            {
                Id = 5,
                Name = "Стеценко Вікторія Федорівна",
                Description = "",
            });
            e.HasData(new Customer
            {
                Id = 6,
                Name = "Налийборщ Омельян Францевич",
                Description = "",
            });
            e.HasData(new Customer
            {
                Id = 7,
                Name = "Неїжхліб Олена Юхимівна",
                Description = "",
            });
            e.HasData(new Customer
            {
                Id = 8,
                Name = "Майборода Віталій Семенович",
                Description = "",
            });
            e.HasData(new Customer
            {
                Id = 9,
                Name = "Черезтинногозадерищенко Ульяна Юстимівна",
                Description = "",
            });
            e.HasData(new Customer
            {
                Id = 10,
                Name = "Дубовий Віктор Олегович",
                Description = "",
            });
            e.HasData(new Customer
            {
                Id = 11,
                Name = "Підгорний Вадим Сергійович",
                Description = "",
            });
            e.HasData(new Customer
            {
                Id = 12,
                Name = "Озерний Сергій Іванович",
                Description = "",
            });
            e.HasData(new Customer
            {
                Id = 13,
                Name = "Нечипоренко Ульяна Омельянівна",
                Description = "",
            });
            e.HasData(new Customer
            {
                Id = 14,
                Name = "Задорожній Олег Олександрович",
                Description = "",
            });
            e.HasData(new Customer
            {
                Id = 15,
                Name = "Миколаєнко Олександра Вікторівна",
                Description = "",
            });
        });
    }

    private void SeedCategories()
    {
        modelBuilder.Entity<Category>(e =>
        {
            e.HasData(new Category
            {
                Id = 1,
                Name = "Ноутбуки та комп'ютери",
                Description = "Ноутбуки; Комп'ютери, неттопи, моноблоки; Монітори, Gaming, Планшети, ..."
            });
            e.HasData(new Category
            {
                Id = 2,
                Name = "Смартфони, ТВ та електроніка",
                Description = ""
            });
            e.HasData(new Category
            {
                Id = 3,
                Name = "Побутова технік",
                Description = "Пральні маниши; Холодильники; Праски; тощо"
            });
        });
    }

    private void SeedProducts()
    {
        modelBuilder.Entity<Product>(e =>
        {
            e.HasData(new Product
            {
                Id = 1,
                CategoryId = 1,
                Name = "Ноутбук Lenovo IdeaPad Slim 5 16IAH8 (83BG001ARA) Cloud Grey / 16\" IPS WUXGA / Intel Core i5-12450H / RAM 16 ГБ / SSD 512 ГБ / Підсвічування клавіатури / Зарядка через Type-C",
                Description = "Екран 16\" IPS (1920x1200) WUXGA, матовий / Intel Core i5-12450H (2.0 - 4.4 ГГц) / RAM 16 ГБ / SSD 512 ГБ / Intel UHD Graphics / без ОД / Wi-Fi / Bluetooth / веб-камера / без ОС / 1.89 кг / сірий",
                CurrentPricePerUnit = 25999.00
            });
            e.HasData(new Product
            {
                Id = 2,
                CategoryId = 1,
                Name = "Ноутбук Acer Aspire 7 A715-76G-560W (NH.QMMEU.002) Charcoal Black / Intel Core i5-12450H / RAM 16 ГБ / SSD 512 ГБ / nVidia GeForce RTX 3050, 4 ГБ / Підсвітка клавіатури",
                Description = "Екран 15.6\" IPS (1920x1080) Full HD, матовий / Intel Core i5-12450H (2.0 - 4.4 ГГц) / RAM 16 ГБ / SSD 512 ГБ / nVidia GeForce RTX 3050, 4 ГБ / без ОД / LAN / Wi-Fi / Bluetooth / веб-камера / без ОС / 2.1 кг / чорний",
                CurrentPricePerUnit = 32999.00
            });
            e.HasData(new Product
            {
                Id = 3,
                CategoryId = 1,
                Name = "Ноутбук ASUS TUF Gaming F15 (2022) FX507ZC4-HN087 (90NR0GW1-M00HJ0) Mecha Gray / 15.6\" IPS Full HD 144 Гц / Intel Core i5-12500H / RAM 16 ГБ / SSD 512 ГБ / nVidia GeForce RTX 3050",
                Description = "Екран 15.6\" IPS (1920x1080) Full HD 144 Гц, матовий / Intel Core i5-12500H (2.5 - 4.5 ГГц) / RAM 16 ГБ / SSD 512 ГБ / nVidia GeForce RTX 3050, 4 ГБ / без ОД / LAN / Wi-Fi / Bluetooth / вебкамера / без ОС / 2.2 кг / сірий",
                CurrentPricePerUnit = 35499.00
            });
            e.HasData(new Product
            {
                Id = 4,
                CategoryId = 1,
                Name = "Ноутбук Apple MacBook Air 13\" M1 8/256GB 2020 (MGN63) Space Gray",
                Description = "Екран 13.3\" Retina (2560x1600) WQXGA, глянсовий / Apple M1 / RAM 8 ГБ / SSD 256 ГБ / Apple M1 Graphics / Wi-Fi / Bluetooth / macOS Big Sur / 1.29 кг / сірий",
                CurrentPricePerUnit = 43499.00
            });
            e.HasData(new Product
            {
                Id = 5,
                CategoryId = 1,
                Name = "Ноутбук HP Laptop 15s-fq5032ua (8F322EA) Spruce Blue / Intel Core i5-1235U / RAM 16 ГБ / SSD 512 ГБ",
                Description = "Экран 15.6\" IPS (1920x1080) Full HD, матовый / Intel Core i5-1235U (0.9 - 4.4 ГГц) / RAM 16 ГБ / SSD 512 ГБ / Intel Iris Xe Graphics / без ОД / Wi-Fi / Bluetooth / веб-камера / DOS / 1.69 кг / синий с серебристым",
                CurrentPricePerUnit = 22999.00
            });
            e.HasData(new Product
            {
                Id = 6,
                CategoryId = 2,
                Name = "Мобільний телефон Samsung Galaxy A24 6/128GB Black (SM-A245FZKVSEK)",
                Description = "Екран (6.5\", Super AMOLED, 2340x1080) / Mediatek Helio G99 (2 x 2.6 ГГц + 6 x 2.0 ГГц) / основна потрійна камера: 50 Мп + 5 Мп + 2 Мп, фронтальна камера: 13 Мп / RAM 6 ГБ / 128 ГБ вбудованої пам'яті + microSD (до 1 ТБ) / 3G / LTE / GPS / ГЛОНАСС / BDS / підтримка 2х SIM-карток (Nano-SIM) / Android 13 / 5000 мА*год",
                CurrentPricePerUnit = 7599.00
            });
            e.HasData(new Product
            {
                Id = 7,
                CategoryId = 2,
                Name = "Телевізор LG 50UR81006LJ",
                Description = "Екран (50\", 3840x2160) / WebOS",
                CurrentPricePerUnit = 20999.00
            });
            e.HasData(new Product
            {
                Id = 8,
                CategoryId = 2,
                Name = "Планшет Lenovo Tab M10 Plus (3rd Gen) 4/64 Wi-Fi Storm Grey (ZAAM0190UA) + чохол у комплекті!",
                Description = "Екран 10.61\" IPS (2000x1200), MultiTouch / Qualcomm Snapdragon 680 (2.4 ГГц + 1.9 ГГц) / RAM 4 ГБ / 64 ГБ вбудованої пам'яті + microSD / Wi-Fi / Bluetooth 5.1 / основная камера 8 Мп + фронтальна - 8 Мп / GPS / ГЛОНАСС / Android 12 / 465 г / сірий",
                CurrentPricePerUnit = 6999.00
            });
            e.HasData(new Product
            {
                Id = 9,
                CategoryId = 2,
                Name = "Зарядна станція EcoFlow DELTA 2 (ZMR330-EU)",
                Description = "1.024 кВт/год / LiFePO4/ Швидке заряджання батареї",
                CurrentPricePerUnit = 38999.00
            });
            e.HasData(new Product
            {
                Id = 10,
                CategoryId = 2,
                Name = "Смарт-годинник Amazfit GTR 4 Superspeed Black (955544)",
                Description = "Екран (1.43\", AMOLED) / 475 мА·год",
                CurrentPricePerUnit = 6999.00
            });
            e.HasData(new Product
            {
                Id = 11,
                CategoryId = 3,
                Name = "Пральна машина вузька LG F2J3WS2W",
                Description = "6.5 кг / 85 х 60 х 44",
                CurrentPricePerUnit = 16499.00
            });
            e.HasData(new Product
            {
                Id = 12,
                CategoryId = 3,
                Name = "Робот-пилосос Xiaomi Robot Vacuum X10+ EU",
                Description = "Сухе та вологе прибирання, док станція",
                CurrentPricePerUnit = 35999.00
            });
            e.HasData(new Product
            {
                Id = 13,
                CategoryId = 3,
                Name = "Двокамерний холодильник Whirlpool W7X 82O OX H",
                Description = "378 л + 248 л / Двокамерний, інверторний / No Frost / 191.2 х 59.6 х 67.8 см",
                CurrentPricePerUnit = 20555.00
            });
        });
    }

    private void SeedOrders()
    {
        modelBuilder.Entity<Order>(e =>
        {
            e.HasData(new Order
            {
                Id = 1,
                CustomerId = 1,
                Notes = "Для роботи"
            });
            e.HasData(new Order
            {
                Id = 2,
                CustomerId = 2,
                Notes = "Подарунки на день народження"
            });
            e.HasData(new Order
            {
                Id = 3,
                CustomerId = 3,
                Notes = "Дружині"
            });
            e.HasData(new Order
            {
                Id = 4,
                CustomerId = 4,
                Notes = "Щоб було"
            });
            e.HasData(new Order
            {
                Id = 5,
                CustomerId = 5,
                Notes = "Для нової оселі"
            });
            e.HasData(new Order
            {
                Id = 6,
                CustomerId = 6,
                Notes = ""
            });
            e.HasData(new Order
            {
                Id = 7,
                CustomerId = 7,
                Notes = ""
            });
            e.HasData(new Order
            {
                Id = 8,
                CustomerId = 8,
                Notes = ""
            });
            e.HasData(new Order
            {
                Id = 9,
                CustomerId = 9,
                Notes = ""
            });
            e.HasData(new Order
            {
                Id = 10,
                CustomerId = 10,
                Notes = ""
            });
            e.HasData(new Order
            {
                Id = 11,
                CustomerId = 11,
                Notes = ""
            });
            e.HasData(new Order
            {
                Id = 12,
                CustomerId = 12,
                Notes = ""
            });
            e.HasData(new Order
            {
                Id = 13,
                CustomerId = 13,
                Notes = ""
            });
            e.HasData(new Order
            {
                Id = 14,
                CustomerId = 14,
                Notes = ""
            });
            e.HasData(new Order
            {
                Id = 15,
                CustomerId = 15,
                Notes = ""
            });
            e.HasData(new Order
            {
                Id = 16,
                CustomerId = 6,
                Notes = ""
            });
            e.HasData(new Order
            {
                Id = 17,
                CustomerId = 7,
                Notes = ""
            });
            e.HasData(new Order
            {
                Id = 18,
                CustomerId = 8,
                Notes = ""
            });
            e.HasData(new Order
            {
                Id = 19,
                CustomerId = 9,
                Notes = ""
            });
        });
    }

    private void SeedOrderLines()
    {
        //TODO SeedOrderLines
        //modelBuilder.Entity<OrderLine>(e =>
        //{
        //    e.HasData(new OrderLine
        //    {

        //    });
        //});
    }
}