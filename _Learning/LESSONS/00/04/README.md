[`🏠 HOME`](../../../README.md)  

[`⏪ Назад`](../03/README.md)  [`📗 LESSONS`](../../README.md)  [`⏩ Вперед`](../05/README.md)

# `📗 Lesson 04 (2024.03.22)` REST API. Postman. N-tier архітектура. CQS and CQRS.

## [Lesson video (02:08:13)](https://youtu.be/a2gj5C-mFcc)

--

## [Lesson materials](https://lms.ithillel.ua/groups/65a65fe34c3a2d3372eef8ea/lessons/65a65fe44c3a2d3372eef96e)

- Web API
- REST API
- Приклад створення Web API з використанням EF Core

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

## [`📕 Homework 03 (Lesson 04)` Web API. REST API](https://lms.ithillel.ua/groups/65a65fe34c3a2d3372eef8ea/homeworks/65f2ff97dcabf5dbd18dcb0f)

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

# 📘 Extra materials

- [ ] [GIT як додати пусту папку](https://phoenixnap.com/kb/git-add-empty-directory)
- [ ] [Ковариантность и контравариантность обобщенных интерфейсов](https://metanit.com/sharp/tutorial/3.27.php)
- [ ] [SQL Megre](https://ru.wikipedia.org/wiki/Merge_(SQL))
- [ ] [Уроки по SQL](https://www.sqlservertutorial.net/)

# 📘 TODO
*Завдання для самостійного опрацювання відстутні*

# 📘 Questions
*Запитання відсутні*

# 📘 NOTES

**Rest API principes:** 
- Stateless communications
- Use of standard methods and uniform interface - client and server are separated by a well-defined interface
- Clear and consistent naming conventions
- Cacheability and layered system
- HATEOAS (**H**ypermedia **A**s **T**he **E**ngine **O**f **A**pplication **S**tate)
- Support code-on-demand
