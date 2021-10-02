USE InternetShop_Zakarin_Artur
GO
CREATE PROCEDURE MaxPrice AS
BEGIN
 SELECT TOP(1) Products.Nameofproduct, Products.Price
 FROM Products
 GROUP BY Products.Nameofproduct, Products.Price
 ORDER BY MAX(Price) DESC

END