CREATE VIEW VIewCustomers AS
Select Customers.Name,
Customers.Surname,
Customers.DateOfBirth,
Customers.DateOfReg,
Customers.Phone,
Customers.Email,
Orders.StatusCheck,
Products.Nameofproduct
FROM Customers
INNER JOIN Orders ON Orders.CodeCustomer = Customers.Code_cos
INNER JOIN Products ON Products.Code = Orders.IDCHECK