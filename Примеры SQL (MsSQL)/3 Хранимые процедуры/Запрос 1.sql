USE InternetShop_Zakarin_Artur
GO
CREATE PROCEDURE ProductCategory(@cat NVARCHAR(255)) AS
BEGIN
SELECT * FROM Products
INNER JOIN Product_Categories ON Product_Categories.Code = Products.Code_Category
WHERE Product_Categories.NameCategory LIKE N'%'+@cat+'%'
END
