CREATE TRIGGER newzakaz
ON Orders_Products
FOR INSERT
AS
DECLARE @A INT
DECLARE @B INT
DECLARE @C INT
SET @A = (SELECT Quantityoftheorderedproduct
FROM inserted)
SET @B = (SELECT Products.Amountproductsonsklad FROM Products,inserted
 WHERE Products.Code=inserted.Itemcode)
SET @C = (SELECT Products.Code FROM Products,inserted
WHERE Products.Code=inserted.Itemcode)
 IF(@A > @B)
 BEGIN
 PRINT 'Ќевозможно добавить тк нет на складе'
ROLLBACK TRANSACTION
 END
 ELSE
 BEGIN
 UPDATE Products
 SET Amountproductsonsklad = Amountproductsonsklad - @A
 WHERE Products.Code = @C
 END

