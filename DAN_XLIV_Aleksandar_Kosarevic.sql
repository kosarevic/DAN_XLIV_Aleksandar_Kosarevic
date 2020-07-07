--If database doesnt exist it is automatically created.
IF DB_ID('Zadatak_1') IS NULL
CREATE DATABASE Zadatak_1
GO
--Newly created database is set to be in use.
USE Zadatak_1
--All tables are reseted clean.
if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblUser')
drop table tblUser

create table tblUser
(
UserID int primary key IDENTITY(1,1),
Username varchar(50),
Password varchar(50)
)

insert into tblUser values ('1111111111111', 'Gost');
insert into tblUser values ('Zaposleni','Zaposleni');