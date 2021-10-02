CREATE FUNCTION SumOfPriceProductS(@id INT)
RETURNS INT
AS 
BEGIN
DECLARE @sums INT
SET @sums = ( SELECT Products.Price * Customers.Bonus
 FROM Orders 
 INNER JOIN Orders_Products ON Orders.IDCHECK=Orders_Products.Ordercode
 INNER JOIN Customers ON Customers.Code_cos=Orders.CodeCustomer
 INNER JOIN Products ON Orders_Products.Itemcode=Products.Code
 WHERE Orders.IDCHECK = @id)
RETURN @sums
END