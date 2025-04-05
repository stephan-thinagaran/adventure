--Restore DB

--Use this to locate files
RESTORE FILELISTONLY 
FROM DISK = 'c:\Users\Admin\Downloads\AdventureWorks2022.bak' 

--use the path from above result below
RESTORE DATABASE [AdventureWorks2022]
FROM DISK = 'c:\Users\Admin\Downloads\AdventureWorks2022.bak'
WITH 
    MOVE 'AdventureWorks2022' TO 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\AdventureWorks2022.mdf',
    MOVE 'AdventureWorks2022_Log' TO 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\AdventureWorks2022_log.ldf',
    REPLACE;
GO

--To sign out all users
USE master;
GO
ALTER DATABASE <DatabaseName> SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO