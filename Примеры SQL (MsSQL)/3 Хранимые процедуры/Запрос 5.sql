USE InternetShop_Zakarin_Artur
GO
CREATE PROCEDURE SumProducts (@CODE FLOAT) AS
BEGIN
SELECT SUM(Quantityoftheorderedproduct) * Price AS 'SUM'
FROM Orders_Products
INNER JOIN Products ON Products.Code = Orders_Products.Itemcode
WHERE Orders_Products.Itemcode = @CODE
GROUP BY Quantityoftheorderedproduct, Products.Price
END