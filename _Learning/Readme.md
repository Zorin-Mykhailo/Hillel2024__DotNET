# [📗 Lesson 01 • Вступ в платформу .NET](./Materials/01/README.md)
# [📗 Lesson 02](./Materials/02/README.md)
# [📗 Lesson 03](./Materials/03/README.md)
# [📗 Lesson 04](./Materials/04/README.md)
# [📗 Lesson 05](./Materials/05/README.md)
# [📗 Lesson 06](./Materials/06/README.md)
# [📗 Lesson 07](./Materials/07/README.md)
# [📗 Lesson 08](./Materials/08/README.md)
# [📗 Lesson 09](./Materials/09/README.md)

---

# Web API

## REST API

Приклад створення Web API з використанням EF Core

- [ ] [What is a REST API?](https://www.ibm.com/topics/rest-apis#:~:text=the%20next%20step-,What%20is%20a%20REST%20API%3F,representational%20state%20transfer%20architectural%20style)
- [ ] [Что такое API RESTful?](https://aws.amazon.com/ru/what-is/restful-api/)
- [ ] [REST](https://en.wikipedia.org/wiki/REST)
- [ ] [Understanding And Using REST APIs](https://www.smashingmagazine.com/2018/01/understanding-using-rest-api/)
- [ ] [Руководство по созданию веб-API с помощью ASP.NET Core](https://learn.microsoft.com/ru-ru/aspnet/core/tutorials/first-web-api?view=aspnetcore-7.0&tabs=visual-studio)
- [ ] [An awesome guide on how to build RESTful APIs with ASP.NET Core](https://www.freecodecamp.org/news/an-awesome-guide-on-how-to-build-restful-apis-with-asp-net-core-87b818123e28/)
- [ ] [RESTful APIs with ASP.NET Core](https://code-maze.com/net-core-series/)
- [ ] [Building REST API With ASP.NET Core](https://betterprogramming.pub/building-rest-api-with-asp-net-core-3cd144d222d8)
- [ ] [Build a RESTful Web API with ASP.NET Core 6 And EF Core](https://www.codingvila.com/2021/05/create-rest-api-with-asp-dot-net-core-5-entity-framework-core.html)
- [ ] [Гайд по написанию требований к REST API](https://wearecommunity.io/communities/bakyiv/articles/1264)
- [ ] [6 Rules of REST APIs](https://appmaster.io/blog/the-six-rules-of-rest-apis)
- [ ] [Які є конвенції в REST API та для чого їх дотримуватись](https://dou.ua/forums/topic/34550/)
- [ ] [CQRS Introduction | CQRS (wordpress.com)](https://cqrs.wordpress.com/documents/cqrs-introduction/)
- [ ] [Command query separation - Difference between CQRS and CQS - Stack Overflow](https://stackoverflow.com/questions/34255490/difference-between-cqrs-and-cqs)
---
### Інше

- [ ] [GIT як додати пусту папку](https://phoenixnap.com/kb/git-add-empty-directory)
- [ ] [Ковариантность и контравариантность обобщенных интерфейсов](https://metanit.com/sharp/tutorial/3.27.php)
- [ ] [SQL Megre](https://ru.wikipedia.org/wiki/Merge_(SQL))
- [ ] [Уроки по SQL](https://www.sqlservertutorial.net/)

---

# Rest API principes
- Stateless communications
- Use of standard methods and uniform interface - client and server are separated by a well-defined interface
- Clear and consistent naming conventions
- Cacheability and layered system
- HATEOAS (**H**ypermedia **A**s **T**he **E**ngine **O**f **A**pplication **S**tate)
- Support code-on-demand

---
# [ДЗ 3 (04.01) Web API. REST API](https://lms.ithillel.ua/groups/65a65fe34c3a2d3372eef8ea/homeworks/65f2ff97dcabf5dbd18dcb0f)

Створити REST API для інтернет магазина за допомогою Web API.
Зберігати все в MS SQL базу даних за допомогою code-first підходу, використовуючи Entity Framework Core.

Таблицi:
- Category
- Product
- Order
- Customer
- і т.д.

Створити ендпоінти для CRUD (create, read, update, delete) операцій із кожною сутністю.
Використовувати HTTP методи (GET, POST, PUT, PATCH, DELETE).

Необхідно, що API відповідало підходам REST API.
- [Гайд по написанию требований к REST API](https://wearecommunity.io/communities/bakyiv/articles/1264)
- [6 Rules of REST APIs](https://appmaster.io/blog/the-six-rules-of-rest-apis)
- [Які є конвенції в REST API та для чого їх дотримуватись](https://dou.ua/forums/topic/34550/)

---

Category
- Id
- Name
- Description
- [Products]

Category_Product
- IdCategory
- IdProduct

Product
- Id
- Name
- Description
- CurrentPricePerUnit
- [Categories]
- [ProductInOrder]

Product_Order
- IdProduct
- IdOrder
- ProductAmount
- PricePerUnit
- TotalSum

Order
- Id
- IdCustomer
- [ProductInOrder]
- TotalSum

Customer
- Id
- Name
- Description
- [Orders]

---
---


Паттерн медіатор

SQL Merge

# Entity Framework

- [ ] [Migrations in Entity Framework Core](https://www.entityframeworktutorial.net/efcore/entity-framework-core-migration.aspx)

## Migrations

```
add-migration InitialSchoolDB
```

- [Use .http files in Visual Studio 2022](https://learn.microsoft.com/en-us/aspnet/core/test/http-files?view=aspnetcore-8.0)
- [Safe storage of app secrets in development in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-8.0&tabs=windows)

# Заняття 7 • Unit, End To End, Integration tests. 

• Рівні ізоляції транзакції
• Нормальні форми БД
• ACID

Міграції 20-19

• Що таке Eadgerloading в EF?


---
---

# [Lesson 05 • Onion. MediatR.](https://lms.ithillel.ua/groups/65a65fe34c3a2d3372eef8ea/lessons/65a65fe44c3a2d3372eef96f)



---
## [Lesson video (02:12:51)](https://youtu.be/oIb72-Fq6mY)
[`00:08:50` Вступ](https://youtu.be/oIb72-Fq6mY?t=530)
[`00:16:47` ❓ Питання](https://youtu.be/oIb72-Fq6mY?t=1007) 
EF. Order with list of Products
- [Learn Entity Framework Core](https://www.learnentityframeworkcore.com/configuration/many-to-many-relationship-configuration)

[`00:29:44` ❓ Питання](https://youtu.be/oIb72-Fq6mY?t=1784)
Демонстрація екрану, "чому не працює мій код"

[`00:34:09` ▶️ Початок уроку](https://youtu.be/oIb72-Fq6mY?t=2049)
[`00:35:18` ❓ Питання](https://youtu.be/oIb72-Fq6mY?t=2118)
Чи є дана архітекрута одним із принципів SOLID?

[`00:36:28` Продовження](https://youtu.be/oIb72-Fq6mY?t=2188)
[`01:38:22` ⏸ Початок перерви](https://youtu.be/oIb72-Fq6mY?t=5902)
[`01:49:06` ▶️ Продовження уроку](https://youtu.be/oIb72-Fq6mY?t=6546)
[`01:57:15` Postman](https://youtu.be/oIb72-Fq6mY?t=7035)



---
## [Lesson materials](https://lms.ithillel.ua/groups/65a65fe34c3a2d3372eef8ea/lessons/65a65fe44c3a2d3372eef96f)
Onion. 
MediatR.

- [Общие архитектуры веб-приложений](https://learn.microsoft.com/ru-ru/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures)
- [Проектирование веб-API RESTFUL](https://learn.microsoft.com/ru-ru/azure/architecture/best-practices/api-design)
- [CQRS and MediatR in ASP.NET Core](https://code-maze.com/cqrs-mediatr-in-aspnet-core/)
- [CQRS And MediatR Pattern Implementation Using .NET Core 6 Web API](https://www.c-sharpcorner.com/article/cqrs-and-mediatr-pattern-implementation-using-net-core-6-web-api/)
- [MediatR — Beyond the basics](https://medium.com/@cristian_lopes/mediatr-beyond-the-basics-8ab90841a732)
- [Реализация прикладного уровня для микрослужб с помощью веб-API](https://learn.microsoft.com/ru-ru/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/microservice-application-layer-implementation-web-api)