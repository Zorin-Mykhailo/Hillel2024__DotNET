[`📝 NOTES`](../README.md)

# Entity Framework

[PMC/PowerShell Commands for Migrations](https://www.entityframeworktutorial.net/efcore/pmc-commands-for-ef-core-migration.aspx)

|PMC Command |Usage |
|---|---|
| Get-Help entityframework | Displays information about entity framework commands. | 
| Add-Migration \<migration name\> | Creates a migration by adding a migration snapshot. |
| Remove-Migration | Removes the last migration snapshot. |
| Update-Database| Updates the database schema based on the last migration snapshot. |
| Script-Migration | Generates a SQL script using all the migration snapshots.  |
| Scaffold-DbContext | Generates a DbContext and entity type classes for a specified database. This is called reverse engineering. |
| Get-DbContext | Gets information about a DbContext type. |
| Drop-Database | Drops the database. |