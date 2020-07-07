--If database doesnt exist it is automatically created.
IF DB_ID('Zadatak_1') IS NULL
CREATE DATABASE Zadatak_1
GO
--Newly created database is set to be in use.
USE Zadatak_1
--All tables are reseted clean.
if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblOrder')
drop table tblOrder
if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblUser')
drop table tblUser
if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblMeal')
drop table tblMeal

create table tblUser
(
UserID int primary key IDENTITY(1,1),
Username varchar(50),
Password varchar(50)
)

create table tblMeal
(
MealID int primary key IDENTITY(1,1),
Name varchar(50),
Price int
)

create table tblOrder
(
OrderID int primary key IDENTITY(1,1),
UserID int foreign key references tblUser(UserID) not null,
OrderTimeStamp DateTime,
Price int,
Approved bit
)

insert into tblUser values ('1111111111111', 'Gost');
insert into tblUser values ('Zaposleni','Zaposleni');

insert into tblMeal values ('Meal 1', 100,0);
insert into tblMeal values ('Meal 2', '200');
insert into tblMeal values ('Meal 3', '300');