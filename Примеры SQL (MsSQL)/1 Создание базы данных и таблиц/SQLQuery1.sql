CREATE DATABASE InternetShop_Zakarin_Artur
GO
USE InternetShop_Zakarin_Artur
GO

CREATE TABLE Products_Manufacturers(
Code INT NOT NULL,
NameManufacturers NVARCHAR(255) NOT NULL,
CONSTRAINT PK_Code PRIMARY KEY(Code)
)
GO

CREATE TABLE Product_Categories(
Code INT NOT NULL,
NameCategory NVARCHAR(255) NOT NULL,
CONSTRAINT PK_CodeCategories PRIMARY KEY(Code)
)
GO

CREATE TABLE Products(
Code INT NOT NULL,
Nameofproduct NVARCHAR(255) NOT NULL,
Code_Category INT NOT NULL,
Code_Manufacturers INT NOT NULL,
Model NVARCHAR(255) NOT NULL,
Price FLOAT NOT NULL,
Garantiya INT NOT NULL,
Opisanie NVARCHAR(255),
Amountproductsonsklad int not null,
CONSTRAINT PK_CodeProducts PRIMARY KEY(Code),
CONSTRAINT PK_UNIQUEProducts UNIQUE(Model),
CONSTRAINT FK_CodeCAT FOREIGN KEY(Code_Category) REFERENCES Product_Categories(Code),
CONSTRAINT FK_CodeMAN FOREIGN KEY(Code_Manufacturers) REFERENCES Products_Manufacturers(Code)
)
GO

CREATE TABLE Customers(
Code_cos INT NOT NULL,
Name NVARCHAR(255) NOT NULL,
Surname NVARCHAR(255) NOT NULL,
Email VARCHAR(255) NOT NULL,
Passwords NVARCHAR(50) NOT NULL,
AdressToGo NVARCHAR(255) NOT NULL,
Phone NVARCHAR(11) NOT NULL,
Gender NVARCHAR(1) NOT NULL,
DateOfBirth DATETIME NOT NULL,
DateOfReg DATETIME NOT NULL DEFAULT GETDATE(),
Bonus INT NOT NULL DEFAULT 0,
CONSTRAINT PK_CodeCustomers PRIMARY KEY(Code_cos),
CONSTRAINT PK_UNIQUECustomersEmail UNIQUE(Email),
CONSTRAINT PK_UNIQUECustomersPhone UNIQUE(Phone),
CONSTRAINT PK_CustomersPasswords CHECK(LEN(Passwords)>6),
CONSTRAINT PK_CustomersGender CHECK(Gender = 'M' OR Gender = 'F'),
--CONSTRAINT PK_CustomersDateOfReg  FOR DateOfReg,
--CONSTRAINT PK_CustomersBonus  FOR Bonus
)
GO

CREATE TABLE Orders(
IDCHECK INT NOT NULL,
CodeCustomer INT NOT NULL,
DateofCheck DATETIME NOT NULL DEFAULT GETDATE(),
StatusCheck INT NOT NULL,
DateOfDostavka DATETIME,
PriceDostavka FLOAT,
SUMPriceDostavka FLOAT,
Commentary NVARCHAR(255),
CONSTRAINT PK_IDCHECK PRIMARY KEY(IDCHECK),
CONSTRAINT FK_CodeORDER FOREIGN KEY(CodeCustomer) REFERENCES Customers(Code_cos),
--CONSTRAINT PK_DateofCheck DEFAULT GETDATE() FOR DateofCheck,
CONSTRAINT PK_Check CHECK(StatusCheck BETWEEN 1 AND 5)
)
GO

CREATE TABLE Orders_Products(
Ordercode INT,
Itemcode INT,
Quantityoftheorderedproduct INT NOT NULL,
Costoftheorderedproduct FLOAT,
CONSTRAINT FK_Ordercode FOREIGN KEY(Ordercode) REFERENCES Orders(IDCHECK),
CONSTRAINT FK_Itemcode FOREIGN KEY(Itemcode) REFERENCES Products(Code)
)
GO