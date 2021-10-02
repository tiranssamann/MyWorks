CREATE FUNCTION TimeDostavka(@Dates INT)
RETURNS INT
AS
BEGIN
DECLARE @DATE INT
SET @DATE=(SELECT (
(DATEDIFF(MONTH,Orders.DateofCheck,Orders.DateOfDostavka)*30)+
DATEDIFF(DAY,Orders.DateofCheck,Orders.DateOfDostavka))  
FROM Orders
WHERE Orders.IDCHECK=@Dates
)
RETURN @DATE
END