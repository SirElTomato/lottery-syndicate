update dbo.Transactions
set UserEmail = 'Owen.Harper@c5alliance.com'
where NumberOfTickets = 4;

select * from dbo.Transactions

UPDATE Customers
SET ContactName = 'Alfred Schmidt', City= 'Frankfurt'
WHERE CustomerID = 1;

INSERT INTO dbo.Transactions(UserEmail, NumberOfTickets, Amount)
VALUES ('Tom.Andrews@c5alliance.com', 4, 8.00);

select sum(NumberOfTickets), sum(Amount) 
from dbo.Transactions group by UserEmail