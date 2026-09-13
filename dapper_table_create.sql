create database ETradeDB
use ETradeDB

create table Category(
	CategoryId int not null primary key identity(1,1),
	[Name] varchar(50)
)

create table Product(
	ProductId int not null primary key identity(1,1),
	[Name] varchar(50),
	Price decimal,
	CategoryId int references Category(CategoryId)
)