CREATE VIEW VIewZakaz AS
SELECT Orders.DateofCheck,
Orders.StatusCheck,
Orders.DateOfDostavka,
Orders.PriceDostavka,
Orders.SUMPriceDostavka,
Orders.Commentary,
Customers.Name,
Customers.Surname,
Products.Nameofproduct
From Orders
INNER JOIN Customers ON Orders.CodeCustomer = Customers.Code_cos
INNER JOIN Products ON Orders.IDCHECK = Products.Code