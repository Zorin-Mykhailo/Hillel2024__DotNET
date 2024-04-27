[`🏠 HOME`](../../../README.md)  

[`⏪ Назад`](../00/09/README.md)  [`📗 LESSONS`](../README.md)  [`⏩ Вперед`](../11/README.md)  

# `📗 Lesson 10 (2024.04.12)` Microservices vs Monolith. Введення у Docker.

## [Lesson video (02:06:41)](https://youtu.be/wA_tnC_4k6o)

[`00:04:46` Вступ](https://youtu.be/wA_tnC_4k6o?t=286)  
[`00:06:03` Авторизація](https://youtu.be/wA_tnC_4k6o)  
[`00:06:44` IdentityDbContext](https://youtu.be/wA_tnC_4k6o?t=404)  
[`00:07:47` Що додається в БД](https://youtu.be/wA_tnC_4k6o?t=467)  
[`00:20:27` RegistrationCommand](https://youtu.be/wA_tnC_4k6o?t=1227)  
[`00:33:46` LoginCommand](https://youtu.be/wA_tnC_4k6o?t=2026)  
[`00:36:34` Читання даних JWT токена з конфігураційного файла та збереження секретів у проекті](https://youtu.be/wA_tnC_4k6o?t=2194)  
[`00:41:00` RefreshTokenCommand](https://youtu.be/wA_tnC_4k6o?t=2460)  
[`00:58:00` RevokeTokenCommand](https://youtu.be/wA_tnC_4k6o?t=3480)  
[`01:00:04` TokenApiBase](https://youtu.be/wA_tnC_4k6o?t=3604)  
[`01:05:10` Як все працює в цілому](https://youtu.be/wA_tnC_4k6o?t=3910)  
[`01:15:50` ⏸ Початок перерви](https://youtu.be/wA_tnC_4k6o?t=4550)  
[`01:25:19` ▶️ Продовження уроку. Angular UI>](https://youtu.be/wA_tnC_4k6o?t=5119)  
[`01:37:58` Мікросервісна архітектура та моноліт](https://youtu.be/wA_tnC_4k6o?t=5878)  
[`01:39:58` Монолітна архітектура](https://youtu.be/wA_tnC_4k6o?t=5998)  
[`01:40:09` Переваги монолітної архітектури](https://youtu.be/wA_tnC_4k6o?t=6009)  
[`01:40:41` Недоліки монолітної архітектури](https://youtu.be/wA_tnC_4k6o?t=6041)  
[`01:41:57` Мікросервіси](https://youtu.be/wA_tnC_4k6o?t=6117)  
[`01:42:02` Переваги мікросервісів](https://youtu.be/wA_tnC_4k6o?t=6122)  
[`01:42:33` Недоліки мікросервісів](https://youtu.be/wA_tnC_4k6o?t=6153)  
[`01:42:58` Підсумовуючи](https://youtu.be/wA_tnC_4k6o?t=6178)  
[`01:44:22` Основні види взаємодії мікросервісів](https://youtu.be/wA_tnC_4k6o?t=6262)  
[`01:49:07` CAP теорема](https://youtu.be/wA_tnC_4k6o?t=6547)  
Доступність, Консистентність, Стійкість до розділення — можна реалізувати тільки 2-ва із 3-х принципів.  
- [ ] [Шаблон распределенных транзакций Saga](https://learn.microsoft.com/ru-ru/azure/architecture/reference-architectures/saga/saga)  

[`01:54:44` Що таке Docker: для чого він потрібний і де використовується](https://youtu.be/wA_tnC_4k6o?t=6884)  
[`01:59:53` Ще недавно програми розгортали на фізичних серверах, тому виникали складнощі, коли це потрібно було зробити швидко](https://youtu.be/wA_tnC_4k6o?t=7193)  
[`02:00:16` Що таке контейнер?](https://youtu.be/wA_tnC_4k6o?t=7216)  
[`02:00:57` Чому контейнети та Docker?](https://youtu.be/wA_tnC_4k6o?t=7257)  
[`02:01:58` Особливості контейнерів](https://youtu.be/wA_tnC_4k6o?t=7318)  
[`02:02:44` Перевафги які дає Docker](https://youtu.be/wA_tnC_4k6o?t=7364)  
[`02:03:06` Завдяки яким механізмам працює Docker](https://youtu.be/wA_tnC_4k6o?t=7386)  
[`02:03:58` Docker Architecture](https://youtu.be/wA_tnC_4k6o?t=7438)  

## [Lesson materials](https://lms.ithillel.ua/groups/65a65fe34c3a2d3372eef8ea/lessons/65a65fe44c3a2d3372eef974)

- Microservices vs Monolith.  
- Введення у Docker.  
- Взаємодія сервісів.  
- Розгортання сервісів у Docker.  
- Microservices vs Monolith  

- [ ] [В чем разница между монолитной архитектурой и архитектурой микросервисов?](https://aws.amazon.com/compare/the-difference-between-monolithic-and-microservices-architecture/)  
- [ ] [Monolithic Approach vs. Microservices Approach: Which is Right for Your Application?](https://www.linkedin.com/pulse/monolithic-approach-vs-microservices-which-right-your-majid-sheikh/)  
- [ ] [Monolithic vs Microservice Architecture - Which Should You Use?](https://www.alexhyett.com/monolithic-vs-microservices/)  

**Docker**

- [ ] [Чем отличаются Kubernetes и Docker](https://aws.amazon.com/compare/the-difference-between-kubernetes-and-docker/)  
- [ ] [Kubernetes vs Docker: What’s the difference?](https://www.dynatrace.com/news/blog/kubernetes-vs-docker/)  
- [ ] [Kubernetes vs Docker – What’s the difference?](https://k21academy.com/docker-kubernetes/kubernetes-vs-docker/)  
- [ ] [Kubernetes vs. Docker](https://azure.microsoft.com/en-us/resources/cloud-computing-dictionary/kubernetes-vs-docker)  
- [ ] [Get started with Docker today](https://www.docker.com/blog/docker-and-kubernetes/е)  

**Докер команди**:

- [ ] [20 корисних команд для роботи з Docker в Linux](https://itedu.center/ua/blog/ratings/docker_linux/)  
- [ ] [Docker. CLI Cheat Sheet](https://docs.docker.com/get-started/docker_cheatsheet.pdf)  
- [ ] [The Ultimate Docker Cheat Sheet](https://dockerlabs.collabnix.com/docker/cheatsheet/)  

## [`📕 Homework 00 (Lesson 00)` Назва]()  
*ДЗ відсутнє*
--

---

# 📘 Extra materials

*Додаткові матеріали відсутні*

# 📘 TODO
*Завдання для самостійного опрацювання відстутні*

# 📘 Questions
*Запитання відсутні*

# 📘 NOTES
*Нотатки відсутні*

