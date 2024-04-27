[`🏠 HOME`](../../../README.md)  

[`⏪ Назад`](../12/README.md)  [`📗 LESSONS`](../README.md)  [`⏩ Вперед`](../14/README.md)  

# `📗 Lesson 13 (2024.04.23)` Azure auth та створення i публiкацiя nuget пакета.

## [Lesson video (02:04:00)](https://youtu.be/_0GdFNwawqc)

--

## [Lesson materials](https://lms.ithillel.ua/groups/65a65fe34c3a2d3372eef8ea/lessons/65a65fe44c3a2d3372eef977)

**Azure аутентифікація та авторизація.**

- Adal and msal  
- Auth techniques in Azure  
- [ ] [Differences between ADAL.NET and MSAL.NET apps](https://learn.microsoft.com/en-us/entra/msal/dotnet/how-to/differences-adal-msal-net)  
- [ ] [Проверка подлинности и авторизация в Службе приложений и Службе функций Azure](https://learn.microsoft.com/ru-ru/azure/app-service/overview-authentication-authorization)  
- [ ] [Microsoft Entra ID with ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/azure-active-directory/?view=aspnetcore-8.0)  
- [ ] [How to integrate Azure Active Directory with your ASP .Net Core project.](https://www.linkedin.com/pulse/how-integrate-azure-active-directory-your-asp-net-mendoza-bland%C3%B3n/)  
- [ ] [microsoft-authentication-library-for-dotnet](https://github.com/AzureAD/microsoft-authentication-library-for-dotnet)  
- [ ] [Describe authentication methods in Azure, including single sign-on (SSO), multifactor authentication, and passwordless](https://www.learnthecontent.com/exam/azure/az-900-microsoft-azure-fundamentals/s/describe-authentication-methods-in-azure-including-single-sign-on-sso-multifactor-authentication-and-passwordless#:~:text=The%20different%20authentication%20methods%20available%20in%20Azure%20AD%20are%20password,multifactor%20authentication%2C%20and%20passwordless%20authentication.)  
- [ ] [Azure Authentication Methods Policy: What is it and what does it change?](https://www.linkedin.com/pulse/azure-authentication-methods-policy-what-does-change-senserva/)  
- [ ] [How to integrate Azure Active Directory with your ASP .Net Core project.](https://www.linkedin.com/pulse/how-integrate-azure-active-directory-your-asp-net-mendoza-bland%C3%B3n)  
- [ ] [How to Deploy an ASP.NET Core Web App in Azure From Docker Image](https://hackernoon.com/how-to-deploy-an-aspnet-core-web-app-in-azure-from-docker-image)  
- [ ] [Understand Microsoft Entra ID](https://learn.microsoft.com/en-us/training/modules/understand-azure-active-directory/)  

**Створення i публiкацiя nuget пакета.**
- [ ] [Quickstart: Create and publish a NuGet package using Visual Studio (Windows only)](https://learn.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package-using-visual-studio?tabs=netcore-cli)  

**Додати у csproj вашого проекту nuget пакета:**

```xml
<PropertyGroup>
	<PackageId>MyNugetPackageUniqueName</PackageId>
	<Version>1.0.0</Version> // please increase version for update your package
	<Authors>Super Developer</Authors>
	<Description>My nuget package description</Description>
</PropertyGroup>
```

## `📕 Homework`
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