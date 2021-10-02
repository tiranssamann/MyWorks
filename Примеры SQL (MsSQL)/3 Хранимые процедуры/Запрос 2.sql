USE InternetShop_Zakarin_Artur
GO
CREATE PROCEDURE TopManufacturers AS
BEGIN
SELECT TOP(1) Products_Manufacturers.NameManufacturers , Quantityoftheorderedproduct
FROM Orders_Products

INNER JOIN Products ON Products.Code = Orders_Products.Itemcode
INNER JOIN Products_Manufacturers ON Products_Manufacturers.Code = Products.Code_Manufacturers
INNER JOIN Orders ON Orders.IDCHECK = Orders_Products.Ordercode 

WHERE Orders.StatusCheck = 5 
GROUP BY Products_Manufacturers.NameManufacturers ,Quantityoftheorderedproduct
ORDER BY max(Quantityoftheorderedproduct) DESC
 
END