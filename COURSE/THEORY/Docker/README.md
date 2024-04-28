[`🏠 HOME`](../../README.md)  

[`📘 THEORY`](../README.md)  

# Docker

## Невеликий консольний туторіал по Docker:

1. Створення нового проекту
```powershell
dotnet new console -o App -n DotNet.Docker
```

2. Перейти в папку з проектом
```powershell
cd App
```

3. Запустити проект
```powershell
dotnet run
```

Всі параметри після - не перередаються команді **dotnet run**, а передаються в додаток.  
Додамо код в **Program.cs**:

```cs
var counter = 0;
var max = args.Length is not 0 ? Convert.ToInt32(args[0]) : -1;
while (max is -1 || counter < max)
{
    Console.WriteLine($"Counter: {++counter}");
    await Task.Delay(TimeSpan.FromMilliseconds(1_000));
}
```

4. Публікація додатку в релізному варіанті
```powershell
dotnet publish -c Release
```

---

Створимо `Dockerfile` (без розширення) поруч із `*.*csproj` файлом
та зкопіюємо в нього наступний вміст

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "DotNet.Docker.dll"]
```

Образ середовища виконання ASP.NET Core використовується цілеспрямовано, хоча може використовуватись образ `mcr.microsoft.com/dotnet/runtime:8.0`.

При використанні образів контейнерів під керуванням Windows необхідно вказати тег образу, крім простого 8.0, наприклад, mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 замісь mcr.microsoft.com/dotnet/aspnet:8.0.
Виберіть ім'я образу в залежності від того чи використовуєти ви Nano Server, або ж Windows Server Core та яку версію цієї ОС. 
Повний список всіх тегів що підтримуються можна знайти [тут](https://hub.docker.com/_/microsoft-dotnet)

Команда `WORKDIR` змінює поточний каталог в контейнері на `App`.
Команда `COPY` предпиує Docker зкопіювати вказану папку на вашому коп'ютері в контейнер. В цьому прикладі папка публікації копіюється в папку з іменем `App/out` в контейнері.
Наступна команда `ENTRYPOINT` використовується щоб налаштувати за допомогою Docker контейнер для запуску в якості виконуваного файла. При запуску контейнера виконується команда ENTRYPOINT. Після виконання контейнер автоматично зупиниться.

До .NET 8 контейнери, налаштовані для запуску тільки для читання, можуть завершуватись з помилкою `Failed to create CoreCLR, HRESULT: 0x8007000E`. Щоб усунути цю проблему, вкажіть, безпосередньо перед кроком `ENTRYPOINT`, змінну середовища `DOTNET_EnableDiagnostics` як `0`:

```dockerfile
ENV DOTNET_EnableDiagnostics=0
```

5. Створюємо докер-образ по існуючому докер файлу

```powershell
docker build -t counter-image -f Dockerfile .
```

Docker опрацює всі стрічки файлу Dockerfile. В `.` команді docker build задається контекст збірки образу. Перемикання `-f` - це шлях до Dockerfile. Ця команда створює образ та локальний репозиторій з іменем `counter-image`, що вказує на такий образ. Після завершення роботи цієї команді виконайте команду `docker images`, щоб продивитись список встановлених образів.

6. Перевірити які докер-образи існують

```powershell
docker images
```

Ви побачите дані наступного роду:
Репозиторій `counter-image` - це ім'я образу. Тег `latest` - тег, що використовується для ідентифікації образу. Идентифікатор накшталт `2f15637dc1f6` - ідентифікатор образу, та інші.

Заключні кроки Dockerfile - створити контейнер із образу та запустити додаток, скопіювати опублікований додаток в контейнер та визначити точку входу.

7. Створення докер-контейнера по докер-образу (зупиненого, його необхідно буде запустити)

```powershell
docker create --name core-counter counter-image
```

8. Перевірити які докер-контейнери існують

```powershell
docker ps -a
```

9. Запуск докер-контейнера

```powershell
docker start core-counter
```

10. Зупинити докер-контейнер

```powershell
docker stop core-counter
```

11. Знову під'єднаємось до докер-контейнера щоб перевірити чи продовжує він працювати

```powershell
docker start core-counter
docker attach --sig-proxy=false core-counter
...
docker attach --sig-proxy=false core-counter
```

`--sig-proxy=false` гарантирует, что ctrl+C не остановит процесс в контейнере.

12. Зупинити контейнер
```powershell
docker stop core-counter
```

13. Перевірити які докер-контейнери існують
```powershell
docker ps -a
```

14. Видалити докер контейнер
```powershell
docker rm core-counter
```

15. Знову перевірити які докер-контейнери існують
```powershell
docker ps -a
```

16. Запустити контейнер та одразу видалити його по завершенню роботи (`docker run -it --rm`)
```powershell
docker run -it --rm counter-image
```

17. Запустити контейнер та передати йому деяке значення
```powershell
 docker run -it --rm counter-image 3
```

При виконанні команди `docker run -it --rm` наступна команда `CTRL+C` зупинить процес, що виконується в контейнері, зупинить сам контейней, а далі цей контейнер видалить (так як в команді вказано параметр `--rm`)

18. Перевіримо існуючі докер-контейнери
```powershell
docker ps -a
```

19. Зупинимо контейнер по його імені
```powershell
docker stop core-counter
```

20. Видалимо докер-контейнер
```powershell
docker rm core-counter
```

Потім видалимо всі непотрібні образи на комп'ютері. Видаліть обрааз, що був створения з файлу `Dockerfile`, а потім видаліть образ .NET, на основі якого було створено файл `Dockerfile`. Ви можете використовувати значення `IMAGE ID` або стрічку в форматі `РЕПОЗИТОРІЙ:МІТКА`

```powershell
docker rmi counter-image:latest
docker rmi mcr.microsoft.com/dotnet/aspnet:8.0
```

21. Перевірити список докер-образів
```powershell
docker images
``` 

## Підтримка Docker у Visual Studio. Зміни для `Movie Manager` 

1. Додати Docker: ПКМ по `MovieManager.API` > `Add` > `Docker Support`
    - Target OS: Linux
    - Container build type: Dockerfile 

2. Змінити Connection Strings до БД, так як Linux не вміє працювати з LocalDb.

```xml
"MovieManager": "Data Source=<replace_with_your_ip>; Initial Catalog=MovieManager; User ID=<your_sql_user>; Password=<your_sql_password>; Connect Timeout=30; Encrypt=True; Trust Server Certificate=True; Application Intent=ReadWrite; Multi Subnet Failover=False"
``` 

- Отримати IP: 
    - cmd
    - ipconfig
    - например секция "Ethernet adapter vEthernet (WSL (Hyper-V firewall))"
    - скопировать  IPv4 Address

- [Створити SQL-користувача з адмінськими правати для авторизації](https://www.ibm.com/docs/en/capmp/8.1.4?topic=monitoring-creating-user-granting-permissions)