CREATE DATABASE Sales  
COLLATE Cyrillic_General_100_CI_AS
Go

USE Sales
Go

-- Покупці: ідентифікатор, ім'я, прізвище
CREATE TABLE Customers (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL, Check(LEN(Name) > 0),
    Surname NVARCHAR(100) NOT NULL, Check(LEN(Surname) > 0)
)
Go

INSERT INTO Customers (Name, Surname)
VALUES 
    ('Іван', 'Поліщук'),
    ('Марія', 'Кондратюк'),
    ('Петро', 'Янович'),
    ('Олена', 'Петренко'),
    ('Анна', 'Савченко');
Go

-- Продавці: ідентифікатор, ім'я, прізвище
CREATE TABLE Sellers (   
	Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL, Check(LEN(Name) > 0),
    Surname NVARCHAR(100) NOT NULL, Check(LEN(Surname) > 0)
)
Go

INSERT INTO Sellers (Name, Surname)
VALUES 
    ('Олексій', 'Симончук'),
    ('Наталя', 'Коваль'),
    ('Ігор', 'Поліщук'),
    ('Оксана', 'Лисенко'),
    ('Андрій', 'Мельник');
Go

-- Продажі: ідентифікатор покупця, ідентифікатор продавця, сума продажі, дата продажі
CREATE TABLE Sales (
    Id INT PRIMARY KEY IDENTITY,
    CustomerId INT REFERENCES Customers(Id),
    SellerId INT REFERENCES Sellers(Id),
    SaleAmount FLOAT NOT NULL,
    SaleDate DATE NOT NULL CHECK(SaleDate <= GETDATE())
)
Go

INSERT INTO Sales (CustomerId, SellerId, SaleAmount, SaleDate)
VALUES 
    (1, 1, 150.50, '2024-03-01'),
    (2, 2, 200.75, '2024-03-09'),
    (3, 3, 100.00, '2024-03-08'),
    (4, 4, 75.25, '2024-03-12'),
    (5, 5, 300.00, '2024-03-13'),
	(1, 5, 750.00, '2023-10-11'),
	(2, 3, 600.00, '2023-11-12'),
	(3, 1, 650.00, '2023-12-13')
Go

select * from Customers
select * from Sellers
select * from Sales
