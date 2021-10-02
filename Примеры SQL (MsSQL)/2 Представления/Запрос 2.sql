CREATE VIEW OJIDANIETOVAR AS
SELECT Orders.DateofCheck,
Orders.StatusCheck,
Orders.DateOfDostavka,
Orders.PriceDostavka,
Orders.SUMPriceDostavka,
Orders.Commentary
FROM Orders
WHERE Orders.StatusCheck = 3
