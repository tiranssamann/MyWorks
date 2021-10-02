DELETE FROM Products
WHERE Products.Code = 1
INSERT INTO Orders_Products(Ordercode,Itemcode,Quantityoftheorderedproduct,Costoftheorderedproduct)
VALUES (1,3,50,NULL)