-- SQL Indexes
-- https://learn.microsoft.com/ru-ru/sql/relational-databases/indexes/indexes?view=sql-server-ver16
-- https://www.c-sharpcorner.com/article/the-complete-reference-table-scan-index-scan-and-index-seek/
-- SQL Server Clustered Indexes

--Сканування таблиці
--Це дуже простий процес.
--Виконуючи сканування таблиці, система запитів запускається з фізичного початку таблиці та 
--проходить через кожен рядок таблиці. Якщо рядок відповідає критеріям, він включає його в набір результатів.
--Це найшвидший спосіб отримати дані, особливо якщо таблиця невелика.
--Для маленької таблиці система запитів може завантажити всі дані за один раз, 
--але для великої таблиці це неможливо, тобто для обробки цих великих даних знадобиться більше вводу-виводу 
--та більше часу.
--Як правило, повне сканування таблиці використовується, коли запит не має пропозиції WHERE, тобто всіх даних.

-- Наприклад, таблиця Employee без індексу та наступний запит використовуватиме сканування таблиці.
--SELECT * FROM Employee

--Індексне сканування
--Якщо у вас є кластеризований індекс і для вашого запиту потрібні всі або 
--більшість записів (тобто запит без речення where або having), тоді він використовує сканування індексу.
--Сканування індексу працює подібно до сканування таблиці під час процесу оптимізації запиту. 
--Оптимізатор запитів переглядає доступний індекс і вибирає один із найкращих на основі речень JOIN і WHERE.
--Коли вибрано правильний індекс, механізм обробки запитів SQL перемістить структуру дерева до вказівника даних, 
--який відповідає критеріям, і далі витягне лише необхідні/необхідні записи.
--Ключова відмінність між скануванням таблиці та скануванням індексу полягає в тому, 
--що дані зберігаються в дереві індексів, процесор запитів дізнається про це, коли досягає кінця поточного, 
--який він шукає. Потім він може надіслати запит або перейти до наступного діапазону даних.
--Сканування індексу трохи швидше, ніж сканування таблиці, але значно повільніше, ніж сканування індексу.

--Індексний пошук
--Коли критерій пошуку достатньо добре відповідає індексу, що дозволяє переходити безпосередньо 
--до певних точок даних, це називається пошуком індексу. 
--Індекс шукає найшвидший спосіб отримати дані в базі даних.
--Наприклад, наступний запит використовуватиме пошук індексу, який можна підтвердити, 
--перевіривши план виконання запиту
--Оптимізатор запитів може використовувати індекс безпосередньо для переходу до ідентифікатора 
--третього працівника та отримання даних.

--SELECT name FROM Employee WHERE id=5
---------------
--Різниця між скануванням таблиці, скануванням індексу та пошуком індексу
 
--Індексне сканування

--1. Використовується, коли нам потрібно отримати всі дані, наприклад від 90% до 100%

--2. Запит не містить речення WHERE, а таблиця не має кластеризованого індексу, 
--тоді використовується повне сканування таблиці

--3. Table scan повільніша за Індекс

--4. Сканування повільніше, ніж пошук

-- Індексний пошук

--1. Використовується, коли нам потрібно отримати деякі дані на основі певної умови, наприклад 10% даних

--2. У запиті немає пропозиції WHERE, а таблиця має кластеризований індекс, тоді використовується сканування індексу

--3. Індекс працює швидше, ніж Table scan

--4. Пошук швидше, ніж сканування
----------------


CREATE TABLE dbo.parts(
    part_id   INT NOT NULL, 
    part_name VARCHAR(100),
	part_code VARCHAR(100),
);

INSERT INTO 
    dbo.parts(part_id, part_name, part_code)
VALUES
    (1,'Frame', '1111'),
    (2,'Head Tube', '2222'),
    (3,'Handlebar Grip', '3333'),
    (4,'Shock Absorber', '4444'),
    (5,'Fork', '5555');

--
SELECT 
    part_id, 
    part_name
FROM 
    dbo.parts
WHERE 
    part_id = 5;

--
ALTER TABLE dbo.parts
ADD PRIMARY KEY(part_id);

--
SELECT 
    part_id, 
    part_name
FROM 
    dbo.parts
WHERE 
    part_id = 5;

-- SQL Server CREATE CLUSTERED INDEX syntax
--CREATE CLUSTERED INDEX index_name
--ON schema_name.table_name (column_list);  

---
-- SQL Server non-clustered indexes

-- SQL Server CREATE non-clustered INDEX syntax
--CREATE [NONCLUSTERED] INDEX index_name
--ON table_name(column_list);

SELECT 
    part_id, 
    part_name
FROM 
    dbo.parts
WHERE 
    part_name = 'Fork';

--
CREATE INDEX ix_parts_name
ON parts(part_name);

--
SELECT 
    part_id, 
    part_name
FROM 
    dbo.parts
WHERE 
    part_name = 'Fork';

-- create a nonclustered index for multiple columns
--SELECT 
--    customer_id, 
--    first_name, 
--    last_name
--FROM 
--    sales.customers
--WHERE 
--    last_name = 'Berg' AND 
--    first_name = 'Monika';

----
--CREATE INDEX ix_customers_name 
--ON sales.customers(last_name, first_name);

-------------
-- SQL Server Rename Index

-- The statement renames an index:

--EXEC sp_rename 
--    index_name, 
--    new_index_name, 
--    N'INDEX';  

-- or use the explicit parameters
--EXEC sp_rename 
--    @objname = N'index_name', 
--    @newname = N'new_index_name',   
--    @objtype = N'INDEX';

EXEC sp_rename 
        N'dbo.parts.ix_parts_name',
        N'ix_parts_naming' ,
        N'INDEX';

-----------
-- SQL Server Unique Index

-- A unique index ensures the index key columns do not contain any duplicate values.

-- syntax
--CREATE UNIQUE INDEX index_name
--ON table_name(column_list);

SELECT 
    part_id, 
    part_name,
	part_code
FROM 
    dbo.parts
WHERE 
    part_code = '4444';

CREATE UNIQUE INDEX ix_part_code
ON dbo.parts(part_code);

--
INSERT INTO 
    dbo.parts(part_id, part_name, part_code)
VALUES
    (1,'Frame', '1111')

-------------
-- SQL Server Disable Indexes

-- syntax
--ALTER INDEX index_name
--ON table_name
--DISABLE;

-- disable all indexes of a table
--ALTER INDEX ALL ON table_name
--DISABLE;

--
alter index ix_part_code
ON dbo.parts
disable

--
alter index all on dbo.parts disable

--------------
-- Enable Indexes

--ALTER INDEX index_name 
--ON table_name  
--REBUILD;

alter index all on dbo.parts REBUILD

--------------
-- DROP INDEX

-- syntax
--DROP INDEX [IF EXISTS] index_name
--ON table_name;

DROP INDEX IF EXISTS ix_part_code
ON dbo.parts

--- Indexes with Included Columns

CREATE UNIQUE INDEX ix_part_code
ON dbo.parts(part_code);


SELECT
    part_name,
	part_code
FROM 
    dbo.parts
WHERE 
    part_code = '4444';

--
DROP INDEX ix_part_code
ON dbo.parts;

--
CREATE UNIQUE INDEX ix_part_code_inc
ON dbo.parts(part_code)
INCLUDE(part_name)

---------
SELECT 
    part_name,
	part_code
FROM 
    dbo.parts
WHERE 
    part_code = '4444';

--------
-- Computed Columns
-- https://www.sqlservertutorial.net/sql-server-indexes/sql-server-indexes-on-computed-columns/

---------
-- Filtered Indexes
-- https://www.sqlservertutorial.net/sql-server-indexes/sql-server-filtered-indexes/

----
-- https://sqlperformance.com/2019/03/sql-performance/filtered-indexes-and-included-columns
--

--REORGANIZE це онлайнова операція, яка дефрагментує листові сторінки в 
--кластеризованому чи некластеризованому індексі сторінка за сторінкою, використовуючи невеликий 
--додатковий робочий простір.

--REBUILD це операція онлайн у випусках Enterprise, офлайн в інших випусках і знову використовує 
--стільки додаткового робочого простору, скільки розмір індексу. 
--Він створює нову копію індексу, а потім видаляє стару, таким чином позбавляючись від фрагментації. 
--За замовчуванням у рамках цієї операції статистика перераховується, але це можна вимкнути.

-- https://learn.microsoft.com/en-us/sql/relational-databases/indexes/reorganize-and-rebuild-indexes?view=sql-server-ver16&redirectedfrom=MSDN

--Не використовуйте SHRINK, окрім як із TRUNCATEONLY опцією, і навіть тоді, якщо файл буде знову зростати, 
--вам слід добре подумати, чи це необхідно:

-- https://www.sqlservercentral.com/articles/why-shrinkfile-is-a-very-bad-thing-and-what-to-do-about-it

----------------
-- execution-plans
-- https://learn.microsoft.com/ru-ru/sql/relational-databases/performance/execution-plans?view=sql-server-ver16

------------------------
-- https://learn.microsoft.com/en-us/sql/odbc/reference/develop-app/transaction-isolation-levels?view=sql-server-ver16
-- https://www.sqlservercentral.com/articles/isolation-levels-in-sql-server
-- https://www.ibm.com/docs/en/cics-ts/5.4?topic=processing-acid-properties-transactions
-- https://www.ibm.com/topics/cap-theorem

--begin transaction  -- begin tran

--select * from parts
--INSERT INTO 
--    dbo.parts(part_id, part_name, part_code)
--VALUES
--    (7,'Some-Part', '123')

--select * from parts

----commit
--rollback

------------------------
-- backup and rollback

-- 1. Як SQL Server реєструє транзакції для бази даних.
-- 2. Чи потрібне резервне копіювання журналу транзакцій бази даних.
-- 3. Які операції відновлення доступні для відновлення бази даних.

--Simple
--Full
--Bulk-logged

CREATE DATABASE HR;
GO

USE HR;

CREATE TABLE People (
  Id int IDENTITY PRIMARY KEY,
  FirstName varchar(50) NOT NULL,
  LastName varchar(50) NOT NULL
);

INSERT INTO People (FirstName, LastName)
  VALUES ('John', 'Doe'),
  ('Jane', 'Doe'),
  ('Upton', 'Luis'),
  ('Dach', 'Keon');

SELECT * FROM People;

-- To view the recovery model of the HR database, you use the following query:
SELECT
  name,
  recovery_model_desc
FROM master.sys.databases
WHERE name = 'HR';

-- To view the recovery model of all the databases in the current server, you use the following query:
SELECT
  name,
  recovery_model_desc
FROM master.sys.databases
ORDER BY name;

-- To change the recovery model to another, you use the ALTER DATABASE following statement:
ALTER DATABASE HR
SET RECOVERY SIMPLE;

-- SIMPLE recovery model

--У моделі відновлення SIMPLE SQL Server видаляє журнали транзакцій із файлів журналів
--транзакцій у кожній контрольній точці. Це призводить до відносно невеликих файлів журналу транзакцій.

--Окрім того, у моделі відновлення SIMPLE журнали транзакцій не зберігають записи транзакцій.
--Тому ви не зможете використовувати розширені стратегії резервного копіювання,
--щоб звести до мінімуму втрату даних.

--На практиці ви використовуєте модель відновлення SIMPLE для бази даних,
--яка може бути перезавантажена з інших джерел, таких як бази даних для цілей звітності.

-- FULL recovery model

--У моделі повного відновлення SQL Server зберігає журнали транзакцій у файлах журналів
--транзакцій доти, доки не буде виконана інструкція BACKUP LOG.
--Іншими словами, оператор BACKUP LOG видаляє журнали транзакцій із файлів журналів транзакцій.

--Якщо ви не виконуєте інструкцію BACKUP LOG регулярно,
--SQL Server зберігає всі журнали транзакцій у файлах журналу транзакцій до тих пір,
--поки файли журналу транзакцій не будуть заповнені і база даних стане недоступною.
--Ось чому вам потрібно запускати оператор BACKUP LOG через регулярні проміжки часу,
--Щоб файли журналу транзакцій були заповнені.

-- Коротше кажучи, модель відновлення FULL дозволяє відновити базу даних у будь-який час.

-- BULK_LOGGED recovery model

--Модель відновлення BULK_LOGGED працює майже так само, як і модель відновлення FULL,
-- За винятком операцій з неповним протоколюванням.

--Модель відновлення BULK_LOGGED не дозволяє відновити базу даних у будь-який час.
-- Практичний сценарій відновлення BULK_LOGGED виглядає так:

--Перед періодичним завантаженням даних встановіть для моделі відновлення значення BULK_LOGGED.
--Завантажити дані до бази
--Встановіть для моделі відновлення значення FULL після завершення завантаження даних.
--Резервне копіювання бази даних

-- Summary

--Модель відновлення – це властивість бази даних, яка управляє реєстрацією транзакцій.
--Модель відновлення може бути однією з наступних: SIMPLE, FULL та BULK_LOGGED.
--Використовуйте модель відновлення SIMPLE для баз даних, дані яких можуть бути перезавантажені з інших джерел.
--Використовуйте модель повного відновлення, якщо ви хочете відновити базу даних у будь-який час.
--Використовуйте модель відновлення BULK_LOGGED для операцій з неповним протоколюванням, таких як BULK INSERT.

-- Backup Types

--Full backup
--Differential backup
--Transaction log backup

-- Full Backup

--Повна резервна копія містить копію всієї бази даних, включаючи використані сторінки даних та файли журналів,
--записані під час резервного копіювання. Повна резервна копія не усікає журналу транзакцій.

--Для відновлення бази завжди потрібний повний бекап. Іншими словами, ви не можете відновити
--Розрізну резервну копію або резервну копію журналу транзакцій без повної резервної копії.

--Повне резервне копіювання може призвести до значного обсягу вводу-виводу.
--Тому виконувати його слід у той час, коли робоче навантаження невелике.

-- syntax
--BACKUP DATABASE database_name
--TO DISK = path_to_backup_file;

-- Differential Backup

-- Диференційна резервна копія містить лише ті дані, які були змінені з
--Моменту останньої повної резервної копії.
-- Крім того, диференційна резервна копія ідентична повній резервній копії.

-- syntax
--BACKUP DATABASE database_name 
--TO DISK = path_to_backup_file 
--WITH DIFFERENTIAL;

-- Transaction Log Backup

--Резервна копія журналу транзакцій містить усі зміни, внесені до бази даних.
--Резервна копія журналу транзакцій потрібна, коли ви використовуєте модель відновлення
-- З повним або неповним протоколюванням, оскільки їм необхідно урізати журнал.

--Зверніть увагу, що дві наступні резервні копії журналу транзакцій не містять надлишкових даних.
--Крім того, резервне копіювання журналу транзакцій не впливає на продуктивність,
--тому його можна виконувати при високому робочому навантаженні.

--Щоб виконати резервне копіювання журналу транзакцій,
--модель відновлення бази даних має бути FULL або BULK_LOGGED.

-- syntax
--BACKUP LOG database_name 
--TO DISK = path_to_backup_file;

-- Summary

--SQL Server надає три типи резервного копіювання:
-- Повне резервне копіювання, диференціальне резервне копіювання та резервне копіювання журналу транзакцій.

--Повна резервна копія створює резервну копію всієї бази даних та активної частини журналів транзакцій.
-- Не очищає журнал транзакцій.
--Диференційне резервне копіювання ґрунтується на повному резервному копіюванні.
--Диференційне резервне копіювання зберігає зміни з моменту
--останнього повного резервного копіювання та активної частини журналів транзакцій наприкінці резервного копіювання.
--Резервна копія журналу транзакцій містить журнали транзакцій,
-- які ще не були скопійовані до останнього запису, що існує на момент закінчення резервного копіювання.

-- Create a full backup of a database using T-SQL

BACKUP DATABASE HR 
TO  DISK = 'D:\backup\hr.bak'
WITH INIT,
NAME = 'HR-Full Database Backup';

RESTORE HEADERONLY   
FROM DISK ='D:\backup\hr.bak';

--
INSERT INTO People (FirstName, LastName)
VALUES ('Bob', 'Climo');

select * from People


BACKUP DATABASE HR 
TO  DISK = 'D:\backup\hr.bak'
WITH NOINIT,
NAME = 'HR-Full Database Backup';

-- Restoring from the first full backup

USE master;

DROP DATABASE HR;

RESTORE DATABASE HR
FROM  DISK = N'D:\backup\hr.bak'
WITH FILE = 1;

USE hr;
SELECT * FROM People;

-- Restoring from the second full backup

USE master;
DROP DATABASE HR;

RESTORE DATABASE HR
FROM  DISK = N'D:\backup\hr.bak'
WITH FILE = 2;

USE hr;
SELECT * FROM People;

-- summary

--Використовуйте оператор BACKUP DATABASE, щоб створити повну резервну копію бази даних.
--Використовуйте опцію WITH INIT, щоб перезаписати резервну копію, та опцію WITH NOINIT,
--щоб додати її до існуючого файлу резервної копії.
--Використовуйте оператор RESTORE DATABASE для відновлення бази даних із повної резервної копії.

-- Introduction to the SQL Server differential backup

-- Differential backups vs. full backups

-- Диференційне резервне копіювання має такі переваги, ніж повне резервне копіювання:

--Швидкість - створення диференціальної резервної копії може бути дуже швидким по
--порівняно зі створенням повної резервної копії, оскільки диференціальна резервна копія захоплює лише дані,
--які змінилися з моменту останньої повної резервної копії.
--Сховище — диференційна резервна копія потребує менше місця для зберігання, ніж повна резервна копія.
-- Менший ризик втрати даних - оскільки для диференціального резервного копіювання потрібно менше
--Місця для зберігання, ви можете виконувати диференціальне резервне копіювання частіше, що знижує ризик втрати даних.
--Однак відновлення з різної резервної копії потребує більше часу, ніж відновлення
--з повної резервної копії, оскільки вам необхідно відновити щонайменше два файли резервної копії:

-- Спочатку відновіть із останньої повної резервної копії.
-- Потім виконайте відновлення з резервної копії.

-- Create differential backups example

-- First, switch to the master and drop the HR database:
USE master;
DROP DATABASE IF EXISTS HR;

-- Second, create the HR database with the People table that has one row:
CREATE DATABASE HR;
GO

USE HR;
GO

CREATE TABLE People (
  Id int IDENTITY PRIMARY KEY,
  FirstName varchar(50) NOT NULL,
  LastName varchar(50) NOT NULL
);

INSERT INTO People (FirstName, LastName)
VALUES ('John', 'Doe');

-- Third, create a full backup of the HR database:
BACKUP DATABASE HR 
TO  DISK = 'D:\backup\hr.bak'
WITH INIT,
NAME = 'HR-Full Database Backup';

-- Fourth, insert one more row into the People table:
INSERT INTO People(FirstName, LastName)
VALUES ('Jane', 'Doe')

-- Fifth, create the first differential backup of the HR database:
BACKUP DATABASE HR
TO  DISK = N'D:\backup\hr.bak' 
WITH  DIFFERENTIAL , 
NAME = N'HR-Differential Database Backup';

-- Sixth, insert one more row into the People table:
INSERT INTO People(FirstName, LastName)
VALUES ('Dach', 'Keon');

-- Seventh, create a second differential backup:
BACKUP DATABASE HR
TO  DISK = N'D:\backup\hr.bak' 
WITH  DIFFERENTIAL , 
NAME = N'HR-Differential Database Backup';

-- Eighth, create a second full backup:
BACKUP DATABASE HR 
TO  DISK = 'D:\backup\hr.bak'
WITH NOINIT,
NAME = 'HR-Full Database Backup';

-- Ninth, insert one more row into the People table:
INSERT INTO People(FirstName, LastName)
VALUES('Dach', 'Keon');

-- Tenth, create a third differential backup:
BACKUP DATABASE HR
TO  DISK = N'D:\backup\hr.bak' 
WITH  DIFFERENTIAL , 
NAME = N'HR-Differential Database Backup';

-- Finally, examine the backup file:
RESTORE HEADERONLY   
FROM DISK = N'D:\backup\hr.bak';

-- Restore a differential backup

-- First, drop the HR database:

USE master;
DROP DATABASE IF EXISTS HR;

-- Second, restore the HR database from the second full backup:
RESTORE DATABASE HR
FROM  DISK = N'D:\backup\hr.bak'
WITH FILE = 4, NORECOVERY;

-- Third, restore the HR database from the last differential backup:
RESTORE DATABASE HR
FROM DISK = N'D:\backup\hr.bak'
WITH FILE = 5, RECOVERY;

-- Finally, select data from the People table in the HR database:
USE HR;
SELECT * FROM people;

-- Summary
--Диференційна резервна копія фіксує зміни з останньої повної резервної копії.
-- І диференційна резервна копія завжди ґрунтується на повній резервній копії.
--Використовуйте оператор BACKUP DATABASE із параметром DIFFERENTIAL, щоб створити диференціальну резервну копію.
--Завжди відновлює спочатку з повної резервної копії, а потім із резервної копії.

-- Introduction to the SQL Server transaction log backup
--Рекомендується частіше робити резервні копії журналу транзакцій, щоб:

--Зведіть до мінімуму втрату даних
--Скоротіть файли журналу

-- To create a transaction log backup, you use the BACKUP LOG statement:

--BACKUP LOG database_name
--TO DISK = path_to_backup_file
--WITH options;

-- SQL Server transaction log backup example

-- First, drop the HR database:
USE master;
DROP DATABASE IF EXISTS HR;

-- Second, create the new HR database:
CREATE DATABASE HR;
GO

-- Third, ensure the HR database is in the FULL recovery mode to perform a transaction log backup:
ALTER DATABASE HR 
SET RECOVERY FULL;

-- Fourth, create the People table and insert a row:
USE HR;
GO

CREATE TABLE People (
  Id int IDENTITY PRIMARY KEY,
  FirstName varchar(50) NOT NULL,
  LastName varchar(50) NOT NULL
);

INSERT INTO People (FirstName, LastName)
VALUES ('John', 'Doe');

-- Fifth, create a full backup:
BACKUP DATABASE HR 
TO  DISK = 'D:\backup\hr.bak'
WITH INIT,
NAME = 'HR-Full Database Backup';

-- Sixth, insert one more row into the People table:
INSERT INTO People(FirstName, LastName)
VALUES ('Jane', 'Doe');

-- Seventh, create the first transaction log backup:
BACKUP LOG HR
TO  DISK = N'D:\backup\hr.bak' 
WITH NAME = N'HR-Transaction Log Backup';

-- Eighth, insert another row into the People table:
INSERT INTO People(FirstName, LastName)
VALUES ('Upton', 'Luis');

-- Ninth, create the second transaction log backup:
BACKUP LOG HR
TO  DISK = N'D:\backup\hr.bak' 
WITH NAME = N'HR-Transaction Log Backup';

-- Tenth, create the second full backup:
BACKUP DATABASE HR 
TO  DISK = 'D:\backup\hr.bak'
WITH NOINIT,
NAME = 'HR-Full Database Backup';

-- Eleventh, insert another row into the People table:
INSERT INTO People(FirstName, LastName)
VALUES('Dach', 'Keon');

BACKUP LOG HR
TO  DISK = N'D:\backup\hr.bak' 
WITH NAME = N'HR-Transaction Log Backup';

select * from People

-- Finally, view the backup file:
RESTORE HEADERONLY   
FROM DISK = N'D:\backup\hr.bak';

------------------------
-- Restore a database from a transaction log backup

-- First, drop the HR database:

USE master;
DROP DATABASE HR;

-- Second, restore the database from the second full backup:
RESTORE DATABASE HR
FROM  DISK = N'D:\backup\hr.bak'
WITH FILE = 4, NORECOVERY;

-- Third, restore the transaction log backup using the RESTORE LOG statement:
RESTORE LOG HR
FROM DISK = N'D:\backup\hr.bak'
WITH FILE = 5, RECOVERY;

-- Finally, switch the current database to HR and select data from the People table:
USE HR;
SELECT * FROM people;

-- summary

--Для резервного копіювання журналу транзакцій модель відновлення бази даних має бути FULL або BULK_LOGGED.
--Резервна копія журналу транзакцій містить журнал транзакцій бази даних.
--Використовуйте оператор BACKUP LOG для резервного копіювання журналів транзакцій.
--Використовуйте RESTORE LOG для відновлення бази даних із резервних копій журналу транзакцій.

-------------------------------------------------------------------
-- roles and permissions

-- Introduction to the SQL Server CREATE LOGIN statement

--CREATE LOGIN login_name 
--WITH PASSWORD = password;

-- The following statement creates a new login called bob with the password Ebe2di68.:

CREATE LOGIN bob
WITH PASSWORD='Ebe2di68.';

-- To view all the logins of an SQL Server instance, you use the following query:

SELECT
  sp.name AS login,
  sp.type_desc AS login_type,
  CASE
    WHEN sp.is_disabled = 1 THEN 'Disabled'
    ELSE 'Enabled'
  END AS status,
  sl.password_hash,
  sp.create_date,
  sp.modify_date
FROM sys.server_principals sp
LEFT JOIN sys.sql_logins sl
  ON sp.principal_id = sl.principal_id
WHERE sp.type NOT IN ('G', 'R')
ORDER BY create_date DESC;

-- The following shows the CREATE LOGIN statement with the CHECK_POLICY option:

--CREATE LOGIN login_name
--WITH PASSWORD = password, 
--CHECK_POLICY = {ON | OFF};

-- The following shows the CREATE LOGIN statement with the CHECK_EXPIRATION option:

--CREATE LOGIN login_name
--WITH PASSWORD = password, 
--CHECK_EXPIRATION = {ON | OFF};

--
-- The MUST_CHANGE option

--CREATE LOGIN login_name
--WITH PASSWORD = password MUST_CHANGE,
--     CHECK_POLICY=ON,
--     CHECK_EXPIRATION=ON;

-- 
CREATE LOGIN alice
WITH PASSWORD = 'UcxSj12.' MUST_CHANGE,
     CHECK_POLICY=ON, 
     CHECK_EXPIRATION=ON;

--
-- Creating a login from a Windows domain account
-- To create a login from a Windows domain account, you use the following statement:

--CREATE LOGIN ACER\peter
--FROM WINDOWS;

--

-- SQL Server CREATE USER
-- SQL Server CREATE USER statement

-- First, create a new login called alex with the password 'Uvxs245!':

CREATE LOGIN alex
WITH PASSWORD='Uvxs245!';

-- 
-- Second, switch the current database to the BikeStores:

USE BikeStores;

-- Third, create a user with the username alex that uses the alex login:

CREATE USER alex
FOR LOGIN alex;

-- Summary

-- Use the CREATE USER statement to add a user to the current database.

-- Use the CREATE USER statement to add a user to the current database.

-- The GRANT statement allows you to grant permissions on a securable to a principal.

--Об'єкт, що захищається - це ресурс, доступ до якого регулює система авторизації SQL Server.
--Наприклад, таблиця захищається.

--Принципал – це об'єкт, який може вимагати ресурс SQL Server.
--Наприклад, користувач є принципалом у SQL Server.

-- syntax

--GRANT permissions
--ON securable TO principal;

--

-- SQL Server GRANT example

-- First, create the HR database with a People table:

USE master;
GO

DROP DATABASE IF EXISTS HR;
GO

CREATE DATABASE HR;
GO

USE HR;

CREATE TABLE People (
  Id int IDENTITY PRIMARY KEY,
  FirstName varchar(50) NOT NULL,
  LastName varchar(50) NOT NULL
);

INSERT INTO People (FirstName, LastName)
  VALUES ('John', 'Doe'),
  ('Jane', 'Doe'),
  ('Upton', 'Luis'),
  ('Dach', 'Keon');

-- Second, create a login with the name peter:

CREATE LOGIN peter
WITH PASSWORD='XUnVe2di45.';

-- Third, create a user peter in the HR database for the peter login:

USE HR;

CREATE USER peter
FOR LOGIN peter;

-- Fourth, connect the SQL Server using the peter user. 
--And you’ll see that the user peter can access the HR database but cannot view any tables.

--Fifth, switch to the system administrator connection and grant the SELECT permission to the user peter on the People table:

GRANT SELECT 
ON People TO peter;

-- Sixth, the user peter can see the People table and select data from it. For example:

SELECT * FROM People;

-- However, the user peter cannot insert data into the People table:

INSERT INTO People(FirstName, LastName)
VALUES('Tony','Blair');

-- Similarly, the user peter also cannot delete data from the People table:

DELETE FROM People
WHERE Id = 1;

-- Fifth, grant the INSERT and DELETE permissions on the People table to the user peter

GRANT INSERT, DELETE
ON People TO peter;

-- Sixth, switch to the user peter‘s connection and insert a new row into the People table:

INSERT INTO People(FirstName, LastName)
VALUES('Tony','Blair');

-- Now, the user peter can insert data into and delete data from the People table.

--Summary

--Use the GRANT statement to grant permissions on a securable to a principal.

-- Introduction to the SQL Server REVOKE statement

-- syntax

--REVOKE permissions
--ON securable
--FROM principal;

-- SQL Server REVOKE statement example

-- First, connect the SQL Server using the system administrator (sa) account 
-- and use the REVOKE statement to remove the DELETE permission on the People table from the user peter:

REVOKE DELETE
ON People
FROM peter;

-- Second, connect to the SQL Server using the user peter and issue the DELETE statement to verify the permission:

DELETE FROM People;

-- error
-- The DELETE permission was denied on the object 'People', database 'HR', schema 'dbo'.
--It works as expected.

--Third, select data from the People table:

SELECT * FROM People;

-- Fourth, remove the SELECT and UPDATE permissions on the People table from the user peter:

REVOKE SELECT, INSERT
ON People
FROM peter;

-- Finally, switch the connection to the user peter and select data from the People table:

SELECT * FROM People;

-- Error
-- The SELECT permission was denied on the object 'People', database 'HR', schema 'dbo'.
-- The error indicates that the revoke was executed successfully.

-- Summary

-- Use SQL Server REVOKE statement to remove the previously granted permissions on a securable from a principal.

-- SQL Server ALTER LOGIN

CREATE LOGIN bobcat
WITH PASSWORD = 'Mou$eY2k.';

--

-- Disable a login

--ALTER LOGIN login_name
--DISABLE;

-- Enable a disabled login

--ALTER LOGIN bobcat
--ENABLE;

-- Rename a login

--ALTER LOGIN login_name 
--WITH NAME = new_name;

-- Change the password of a login

--ALTER LOGIN login_name
--WITH PASSWORD = new_password;

-- Unlock a login

--ALTER LOGIN login_name
--WITH PASSWORD=password
--UNLOCK;

--Summary

--Use the ALTER LOGIN statement to change the properties of a login account.

-- Introduction to the SQL Server ALTER USER statement

-- To rename a user, you use the ALTER USER ... WITH NAME statement:

--ALTER USER user_name
--WITH NAME new_name;

-- First, create a new login called zack:

CREATE LOGIN zack 
WITH PASSWORD = 'Zu$c3suik.';

-- Second, create a user for the same login:

CREATE USER zack
FOR LOGIN zack;

-- Third, change the name of the user zack to zachary:

ALTER USER zack
WITH NAME = zachary;

-- Change the default schema

-- To change the default schema of a user to another, you use the ALTER USER .. WITH DEFAULT_SCHEMA statement:

--ALTER USER user_name
--WITH DEFAULT_SCHEMA = new_schema;

--

ALTER USER zachary
WITH DEFAULT_SCHEMA = sales;

-- Map the user with another login account

-- To map the user with another login account, you use following ALTER USER ... WITH LOGIN statement:

--ALTER USER user_name
--WITH LOGIN = new_login;

-- 

CREATE LOGIN zachary
WITH PASSWORD = 'Na%c8suik#';

ALTER USER zachary
WITH LOGIN = zachary;

-- Changing several options at once

-- The following statement changes the name, default schema, and login of the user zachary:

ALTER USER zachary
WITH NAME = zack,
     LOGIN = zack,
     DEFAULT_SCHEMA = production;

--Summary

--Use the ALTER USER statement to change the name of a user, map it with a new login and change the default schema.

-- Introduction to the SQL Server DROP LOGIN statement

-- DROP LOGIN login_name;

-- First, create a new login called jack with a password:

CREATE LOGIN jack
WITH PASSWORD = 'iOvT84xn.';

-- Second, drop the login jack using the DROP LOGIN statement:

DROP LOGIN jack;

-- Using the DROP LOGIN statement to remove a login that maps to a database user

-- First, create a new login called joe with a password:

CREATE LOGIN joe
WITH PASSWORD = 'NBgs23we$';

-- Second, create a new user for the login joe:

CREATE USER joe
FOR LOGIN joe;

-- Third, drop the login joe using the DROP LOGIN statement:

DROP LOGIN joe;

--The user joe becomes orphaned.

--To get all orphaned users in the current database server, you use the following query:

SELECT
  dp.type_desc,
  dp.sid,
  dp.name AS user_name
FROM sys.database_principals AS dp
LEFT JOIN sys.server_principals AS sp
  ON dp.sid = sp.sid
WHERE sp.sid IS NULL
AND dp.authentication_type_desc = 'INSTANCE';

-- Resolve an orphaned user

CREATE LOGIN ocean
WITH PASSWORD = 'bNHXUYT321#',
	 SID = 0xC48461C284AE024EB42149BFDBCD18A8;

--
--Now, the user joe can log in to the database server.

--If you already have a login and want to map it with the orphaned user, you can use the ALTER USER statement. For example:

ALTER USER orphaned_user 
WITH LOGIN = login_name;

--Summary

--Use the DROP LOGIN statement to delete a login account from the SQL Server.
--The DROP LOGIN cannot delete a login account that is logged in or owns a securable, 
-- server-level object, or SQL Server Agent job.
--Drop a login that maps to database users will make these database users orphaned.

-- Introduction to the SQL Server DROP USER statement

-- DROP USER

--The DROP USER statement also cannot drop the guest user. 
--However, you can disable the guest user by revoking the CONNECT permission. 
--The following statement revokes the CONNECT permission from the guest user:

REVOKE CONNECT FROM GUEST;

-- Note that you execute the above statement in any databases other than master or tempdb

-- Using the DROP USER to delete a user in the current database example

-- First, create a new login jin with a password:
CREATE LOGIN jin
WITH PASSWORD ='uJIKng12.';

-- Second, create a new user and map it with the login jin:
CREATE USER jin
FOR LOGIN jin;

-- Third, drop the user jin from the current database:
DROP USER IF EXISTS jin;

--  Drop a user that owns a securable example

-- First, create a new login called anthony with a password:
CREATE LOGIN anthony
WITH PASSWORD ='uNMng78!';

-- Second, create a new user for the login anthony:
CREATE USER tony
FOR LOGIN anthony;

-- Third, create a schema called report and grant authorization to the user tony:
CREATE SCHEMA report 
AUTHORIZATION tony;

-- Fourth, connect to the SQL Server using the login anthony and create a table called daily_sales in the schema report:
USE BikeStores;


CREATE TABLE report.daily_sales (
	Id INT IDENTITY PRIMARY KEY,
	Day DATE NOT NULL,
	Amount DECIMAL(10,2) NOT NULL DEFAULT 0
)

-- Fifth, switch the connection to the system administrator (sa) account and drop the user tony:
DROP USER tony;

-- SQL Server issued the following error:
--The database principal owns a schema in the database, and cannot be dropped.

--Because the user tony owns the schema report, the DROP USER statement cannot delete it.

--To remove the user tony, you need to transfer the authorization of the schema report to another user first. 
--For example, the following statement changes the authorization of the schema report to the user dbo:

ALTER AUTHORIZATION 
ON SCHEMA::report 
TO dbo;

--If you execute the DROP USER statement to delete the user tony, you’ll see that it executes successfully:

DROP USER tony;

--Summary

--Use the DROP USER statement to delete a user in the current database.
--If a user owns one or more securables, you need to transfer the ownership of 
--the securables to another user before deleting the user.

-- Introduction to the SQL Server Roles

-- Роль - це група дозволів. Ролі допомагають спростити керування дозволами.
-- Наприклад, замість того, щоб призначати дозволи користувачам окремо,
-- Ви можете згрупувати дозволи в ролі та додати користувачів до цієї ролі:

-- Спочатку створіть роль.
-- По-друге, призначте дозволи ролі.
-- По-третє, додайте в роль одного або кількох користувачів.
--SQL Server надає вам три основні типи ролей:

--Ролі рівня сервера — керуйте дозволами на зміну конфігурації сервера, як у SQL Server.
--Ролі на рівні бази даних — керуйте дозволами для баз даних, такими як створення таблиць та запит даних.
--Ролі рівня програми — дозволяють програмі працювати зі своїми власними дозволами, подібними до користувачів.
--Для кожного типу SQL Server надає два типи:

--Фіксовані ролі сервера: вбудовані ролі SQL Server. Ці ролі мають фіксований набір дозволів.
--Ролі, що визначаються користувачем: ролі, які ви визначаєте для задоволення певних вимог безпеки.

-- Adding a user to a role example

-- First, create a new login called tiger:

CREATE LOGIN tiger
WITH PASSWORD='UyxIv.12';

-- Next, switch the current database to BikeStores and create a user for the tiger login:

Use BikeStores;

CREATE USER tiger
FOR LOGIN tiger;

--Then, connect to the BikeStores database using the user tiger. 
--The user tiger can see the BikeStores database but cannot view any database objects.

--After that, add the user tiger to the db_datareader role:

ALTER ROLE db_datareader
ADD MEMBER peter;

--The db_datareader is a fixed database role. The db_datareader role allows 
--all the members to read data from all user tables and views in the database. 
--Technically, it’s equivalent to the following GRANT statement:

--GRANT SELECT 
--ON DATABASE::BikeStores
--TO tiger;
--In this example, the DATABASE is a class type that indicates the securable which follows the :: is a database. 
-- The following are the available class types:

--LOGIN
--DATABASE
--OBJECT
--ROLE
--SCHEMA
--USER
--Finally, switch the connection to the user peter and select data from the sales.orders table:

SELECT * FROM sales.orders;

-- Creating a user-defined role

-- First, set the current database to master and create a new login called mary:
USE master;

CREATE LOGIN mary 
WITH PASSWORD='XUjxse19!';

-- Second, switch the current database to BikeStores and create a new user called mary for the login mary:
USE BikeStores;

CREATE USER mary 
FOR LOGIN mary;

-- Third, create a new role called sales_report in the BikeStores database:
CREATE ROLE sales_report;

--In this example, we use the CREATE ROLE statement to create a new role. The sales_report is the role name.

--Fourth, grant the SELECT privilege on the Sales schema to the sales_report:

GRANT SELECT 
ON SCHEMA::Sales 
TO sales_report;

-- Fifth, add the tiger user to the sales_report role:

ALTER ROLE sales_report
ADD MEMBER tiger;

--Finally, connect to the BikeStores database using the user mary. 
--In this case, the user mary only can see the tables in sales schema

--Summary

--A role is a group of permissions.
--Use the CREATE ROLE statement to create a new role.
--Use the ALTER ROLE ... ADD MEMBER ... statement to add a user to a role.

-- Introduction to the CREATE ROLE statement

--CREATE ROLE role_name 
--[AUTHORIZATION owner_name];

-- Creating a new role example

-- First, create the new login called james in the master database:
CREATE LOGIN james 
WITH PASSWORD = 'Ux!sa123ayb';

-- Next, create a new user for the login james:
CREATE USER james 
FOR LOGIN james;

-- Then, create a new role called sales:
CREATE ROLE sales;

-- After that, grant the SELECT, INSERT, DELETE, and UPDATE privileges on the sales schema to the sales role:
GRANT SELECT, INSERT, UPDATE, DELETE 
ON SCHEMA::sales
TO sales;

-- Finally, add the user james to the sales role:
ALTER ROLE sales
ADD MEMBER james;

-- Creating a new role owned by a fixed database role example

--The following example uses the CREATE ROLE statement to create 
--a new role owned by the db_securityadmin fixed database role:

CREATE ROLE sox_auditors
AUTHORIZATION db_securityadmin;

-- Examining the roles
--The roles and their members are visible in the sys.database_principals and sys.database_role_members views.

--The following shows the information on the sales and sox_auditors roles:

SELECT
  name,
  principal_id,
  type,
  type_desc,
  owning_principal_id
FROM sys.database_principals
WHERE name in ('sales', 'sox_auditors');

--Summary

--Use the SQL Server CREATE ROLE statement to create a new role in a database.


-- Introduction to the SQL Server ALTER ROLE statement

--ALTER ROLE role_name 
--WITH NAME = new_name;


-- To add a member to a role, you use the ALTER ROLE... ADD MEMBER statement:
--ALTER ROLE role_name
--ADD MEMBER database_principal;

-- To remove a member from a role, you use the ALTER ROLE ... DROP MEMBER statement:
--ALTER ROLE role_name
--DROP MEMBER database_principal;

-- Using the SQL Server ALTER ROLE to rename a role

-- First, create a new role called production:
CREATE ROLE production;

-- Second, rename the role production to manufacturing using the ALTER ROLE statement:
ALTER ROLE production
WITH NAME = manufacturing;

-- Using the SQL Server ALTER ROLE to add a member to a role example

-- First, create a new login called robert:
CREATE LOGIN robert 
WITH PASSWORD = 'Uikbm!#90';

-- Second, create a new user for the login robert:
CREATE USER robert 
FOR LOGIN robert;

-- Third, add the user robert to the manufacturing role:
ALTER ROLE manufacturing 
ADD MEMBER robert;

-- The following query verifies that the user robert is a member of the role manufacturing:
SELECT
  r.name role_name,
  r.type role_type,
  r.type_desc role_type_desc,
  m.name member_name,
  m.type member_type,
  m.type_desc meber_type_desc
FROM sys.database_principals r
INNER JOIN sys.database_role_members rm ON rm.role_principal_id = r.principal_id
INNER JOIN sys.database_principals m ON m.principal_id = rm.member_principal_id
WHERE r.name ='manufacturing';

-- Using the SQL Server ALTER ROLE to remove a member from a role example
-- The following example uses the ALTER ROLE ... DROP MEMBER to remove the user robert from the role manufacturing:
ALTER ROLE manufacturing
DROP MEMBER robert;

--Summary

--Use the ALTER ROLE ... WITH NAME to rename a role.
--Use the ALTER ROLE ... ADD MEMBER to add a member to role.
--Use the ALTER ROLE ... DROP MEMBER to remove a member from a role.


-- Introduction to the SQL Server DROP ROLE statement

-- DROP ROLE [IF EXISTS] role_name;

-- Using the SQL Server DROP ROLE statement to drop a role

-- The following example uses the DROP ROLE statement to drop the sox_auditors role from the BikeStores database:
DROP ROLE IF EXISTS sox_auditors;

-- Since the role sox_auditors has no member, the statement executes successfully.

-- Remove a role that has members example

-- First, use the DROP ROLE statement to remove the role sales from the database:
DROP ROLE sales;
--Since the role sales has members, SQL Server issues the following error:
-- The role has members. It must be empty before it can be dropped.
-- Second, to find the members that belong to the role sales, you use the following statement:

SELECT
  r.name role_name,
  r.type role_type,
  r.type_desc role_type_desc,
  m.name member_name,
  m.type member_type,
  m.type_desc member_type_desc
FROM sys.database_principals r
INNER JOIN  sys.database_role_members  rm on rm.role_principal_id = r.principal_id
INNER JOIN sys.database_principals m on m.principal_id = rm.member_principal_id
WHERE r.name ='sales';

-- The output shows that the role sales has one member which is the user james.

--Third, remove the user james from the role sales using the ALTER ROLE... DROP MEMBER statement:
ALTER ROLE sales
DROP MEMBER james;

--Finally, remove the sales roles using the DROP ROLE statement:
DROP ROLE sales;

--Summary
--Use the DROP ROLE statement to remove a role from the current database.
----------------------------------------
