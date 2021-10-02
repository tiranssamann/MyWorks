CREATE VIEW VIewZakazOjidanie AS
SELECT Products.Nameofproduct,
Products.Model,
Products_Manufacturers.NameManufacturers,
Orders.StatusCheck
From Orders
INNER JOIN Products_Manufacturers ON Orders.CodeCustomer = Products_Manufacturers.Code
INNER JOIN Products ON Orders.IDCHECK = Products.Code
WHERE Orders.StatusCheck = 3