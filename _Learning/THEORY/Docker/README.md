[`🏠 HOME`](../../README.md)  

[`📘 THEORY`](../README.md)  

# Docker

## Небольшой консольный туториал по докеру:

1. Створення нового проекту
```ps
dotnet new console -o App -n DotNet.Docker
```
1. dotnet new console -o App -n DotNet.Docker -> создание нового проекта
2. cd App -> перейти в папку с проектом
3. dotnet run -> запустить проект

Все параметры после -- не передаются команде dotnet run, а передаются в приложение.

добавим код в Program.cs:

var counter = 0;
var max = args.Length is not 0 ? Convert.ToInt32(args[0]) : -1;
while (max is -1 || counter < max)
{
    Console.WriteLine($"Counter: {++counter}");
    await Task.Delay(TimeSpan.FromMilliseconds(1_000));
}

4. dotnet publish -c Release -> публикация приложения в релиз варианте

создадим Dockerfile (без расширения) возле CSPROJ файла

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

Образ среды выполнения ASP.NET Core используется намеренно, хотя может использоваться образ mcr.microsoft.com/dotnet/runtime:8.0.

При использовании образов контейнеров под управлением Windows необходимо указать тег изображения, кроме простого 8.0, например, mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 вместо mcr.microsoft.com/dotnet/aspnet:8.0. 
Выберите имя образа в зависимости от того, используете ли вы Nano Server или Windows Server Core и какую версию этой ОС. 
Полный список всех поддерживаемых тегов можно найти тут: https://hub.docker.com/_/microsoft-dotnet

Команда COPY предписывает Docker скопировать указанную папку на вашем компьютере в папку в контейнере. В этом примере папка публикации копируется в папку с именем App/out в контейнере.

Команда WORKDIR изменяет текущий каталог в контейнере на App.

Следующая команда ENTRYPOINT используется, чтобы настроить с помощью Docker контейнер для запуска в качестве исполняемого файла. При запуске контейнера выполняется команда ENTRYPOINT. После выполнения команды контейнер автоматически остановится.

До .NET 8 контейнеры, настроенные для запуска только для чтения, могут завершиться ошибкой Failed to create CoreCLR, HRESULT: 0x8007000E. Чтобы устранить эту проблему, укажите DOTNET_EnableDiagnostics переменную среды как 0 (непосредственно перед ENTRYPOINT шагом):

ENV DOTNET_EnableDiagnostics=0

5. docker build -t counter-image -f Dockerfile . -> создадим докер образ по существующему докер файлу

Docker обработает все строки файла Dockerfile. В . команде docker build задается контекст сборки образа. Переключение -f — это путь к Dockerfile. Эта команда создает образ и локальный репозиторий с именем counter-image, который указывает на такой образ. После завершения работы этой команды выполните команду docker images, чтобы просмотреть список установленных образов.

6. docker images -> проверить какие докер образы существуют

Репозиторий counter-image — это имя образа. Тег latest — это тег, используемый для идентификации изображения. Идентификатор 2f15637dc1f6 изображения. Время 10 minutes ago создания образа. Размер 217MB изображения. Заключительные шаги Dockerfile — создать контейнер из образа и запустить приложение, скопировать опубликованное приложение в контейнер и определить точку входа.

7. docker create --name core-counter counter-image -> создание докер контейнера по докер образу (остановленного, нужно будет его запустить)
8. docker ps -a -> проверить какие докер контейнеры существуют
9. docker start core-counter -> запуск докер контейнера
10. docker stop core-counter -> остановить докер контейнер
11. подключимся снова к конейнеру чтоб проверить продолжает ли он работать

    - docker start core-counter
    - docker attach --sig-proxy=false core-counter
    - ....
    - docker attach --sig-proxy=false core-counter

--sig-proxy=false гарантирует, что ctrl+C не остановит процесс в контейнере.

12. docker stop core-counter -> остановить контейнер
13. docker ps -a -> проверить какие докер контейнеры существуют
14. docker rm core-counter -> удалить докер контейнер
15. docker ps -a -> снова проверить какие докер контейнеры существуют
16. docker run -it --rm counter-image -> запустить контейнер и после завершения сразу его удалить (docker run -it --rm)
17. docker run -it --rm counter-image 3 -> запустить контейнер и передать ему некоторое значение

При этом docker run -itкоманда CTRL+Cостанавливает процесс, выполняемый в контейнере, который, в свою очередь, останавливаетконтейнер. Так как в команде указан параметр --rm, контейнер автоматически удалится после остановки процесса.

18. docker ps -a -> проверим какие есть докер контейнеры
19. docker stop core-counter -> остановим докер контейнер по имени
20. docker rm core-counter -> удалим докер контейнер

Затем удалите все ненужные образы на компьютере. Удалите образ, созданный с помощью файла Dockerfile, а затем удалите образ .NET, на основе которого был создан файл Dockerfile. Вы можете использовать значение IMAGE ID или строку в формате РЕПОЗИТОРИЙ:МЕТКА.

    - docker rmi counter-image:latest
    - docker rmi mcr.microsoft.com/dotnet/aspnet:8.0

21. docker images -> проверить список докер образов


                        Изменения для Movie Manager:

1. Добавить Docker: правой кнопкой по MovieManager.API > Add > Docker Support.
    - Target OS: Linux
    - Container build type: Dockerfile

2. Поменять Connection Strings к базе, так как Linux не умеет работать с LocalDb.

"MovieManager": "Data Source=<replace_with_your_ip>;Initial Catalog=MovieManager;User ID=<your_sql_user>;Password=<your_sql_password>;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False"

    - Получить IP: 
        1. cmd
        2. ipconfig
        3. например секция "Ethernet adapter vEthernet (WSL (Hyper-V firewall))"
        4. скопировать  IPv4 Address

    - Создать SQL пользователя с админ правами для авторизации
    https://www.ibm.com/docs/en/capmp/8.1.4?topic=monitoring-creating-user-granting-permissions
