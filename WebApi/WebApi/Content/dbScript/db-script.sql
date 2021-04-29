----------------------------------------- MASTER DETAILS CURD---------------------------------
CREATE DATABASE RestaurantDB
GO
USE RestaurantDB
GO
CREATE TABLE Customer(
CustomerID int primary key identity(1,1),
Name varchar(50)
)
CREATE TABLE Item(
ItemID int primary key identity(1,1),
Name varchar(50),
Price decimal(10,2)
)
go
--MasterTable:
CREATE TABLE Orders(
OrderID bigint primary key identity (1,1),
OrderNo varchar(50),
CustomerID INT NOT NULL FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
PMethod varchar(50),
GTotal decimal (18,2)
)
go
--details table:
create TABLE OrderItems(
OrderItemID bigint primary key identity(1,1),
OrderID BIGINT NOT NULL FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
ItemID	   INT NOT NULL FOREIGN KEY (ItemID) REFERENCES Item(ItemID),
Quantity   INT
)
INSERT INTO OrderItems (OrderID,ItemID,Quantity) VALUES(1,2,5)
---------------drop table Orders
---------------drop table Customer
---------------drop table OrderItems
INSERT INTO Customer (Name) VALUES('Rasel Kabir')
INSERT INTO Customer (Name) VALUES('Raihan Kabir')
INSERT INTO Customer (Name) VALUES('Azid khan')
INSERT INTO Customer (Name) VALUES('Rayed Ahmed')
go
select * from Customer
go
INSERT INTO Item(Name,Price) VALUES('Chicken Tenders',500)
INSERT INTO Item(Name,Price) VALUES('Chicken Fries',700)
INSERT INTO Item(Name,Price) VALUES('Chicken Sandwich',550)
INSERT INTO Item(Name,Price) VALUES('Grilled Chees Sandwich',400)
INSERT INTO Item(Name,Price) VALUES('Lettuce and Tomato Burger',300)
INSERT INTO Item(Name,Price) VALUES('Kashmeri Birayani',450)
go
SELECT * from Item
select * from Customer
SELECT * from Orders
SELECT * from OrderItems
