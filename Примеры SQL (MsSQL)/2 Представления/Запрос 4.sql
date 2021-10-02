CREATE VIEW VIewZakazKonec AS
SELECT Products.Nameofproduct,
Products.Model,
Products.Amountproductsonsklad,
Products.Price
FROM Products
WHERE Products.Amountproductsonsklad <= 0