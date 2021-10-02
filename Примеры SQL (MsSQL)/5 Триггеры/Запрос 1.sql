CREATE TRIGGER deleteprod
ON Products
INSTEAD OF DELETE
AS
BEGIN
DECLARE @A INT
SET @A = (SELECT Products.Amountproductsonsklad FROM deleted
 INNER JOIN Orders_Products ON deleted.Code=Orders_Products.Ordercode 
 INNER JOIN Products ON Orders_Products.Itemcode=Products.Code 
 WHERE deleted.Code=deleted.Code)
IF(@A>0)
BEGIN
RAISERROR('Невозможно удалить',0,1)
END
ELSE
BEGIN
DELETE Products WHERE Products.Code = @A
END
END