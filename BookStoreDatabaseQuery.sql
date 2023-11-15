CREATE DATABASE BookStoreDB;

USE BookStoreDB;

CREATE TABLE UserTable
(
	UserID INT PRIMARY KEY IDENTITY,
	UserName VARCHAR(50) NOT NULL,
	UserEmailID VARCHAR(50) UNIQUE,
	UserPassword VARCHAR(50) NOT NULL,
	UserPhoneNumber VARCHAR(20) NOT NULL,
	UserRole VARCHAR(10) CHECK(UserRole IN ('Admin','User')),
);

CREATE TABLE BookTable
(
	BookID INT PRIMARY KEY IDENTITY,
	BookName varchar(100),
	AuthorName varchar(100),
	DiscountPrice INT,
	OriginalPrice INT,
	BookDescription varchar(MAX),
	BookImage varchar(200),
	BookQuantity INT,
);

---Altering Table to add Not null constraint to Book's Name---

ALTER TABLE BookTable
ALTER COLUMN BookName VARCHAR(100) NOT NULL;

---Altering Table to add Not null constraint to Book's AuthorName---

ALTER TABLE BookTable
ALTER COLUMN AuthorName VARCHAR(100) NOT NULL;

---Altering Table to add Not null constraint to Book's Original Price---

ALTER TABLE BookTable
ALTER COLUMN OriginalPrice INT NOT NULL;

---Created Order Table---

CREATE TABLE OrderTable
(
	OrderID INT PRIMARY KEY IDENTITY,
	OrderPrice INT,
	OrderQuantity INT,
	UserID INT FOREIGN KEY REFERENCES UserTable(UserID)
);

---Altering table to add not null constraint in OrderPrice Column---

ALTER TABLE OrderTable
ALTER COLUMN OrderPrice INT NOT NULL;

---Altering table to add not null constraint in OrderPrice Column---

ALTER TABLE OrderTable
ALTER COLUMN OrderQuantity INT NOT NULL;

---Created Address Table---

CREATE TABLE AddressTable
(
	AddressID INT PRIMARY KEY IDENTITY,
	UserAddress VARCHAR(200) NOT NULL,
	City VARCHAR(50) NOT NULL,
	State VARCHAR(50)NOT NULL,
	Pincode INT NOT NULL,
	UserID INT FOREIGN KEY REFERENCES UserTable(UserID),
);

---Adding relationship between Address table and order table---

ALTER TABLE OrderTable
ADD AddressID INT;

ALTER TABLE OrderTable
ADD FOREIGN KEY(AddressID)
REFERENCES AddressTable(AddressID);

INSERT INTO AddressTable(UserAddress,City,State,Pincode,UserID)
VALUES ('Magdi Raste','Bangalore','Karanataka',560056,7);

SELECT * FROM OrderTable;

