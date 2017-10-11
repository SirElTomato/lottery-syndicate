update dbo.Transactions
set UserEmail = 'Owen.Harper@c5alliance.com'
where NumberOfTickets = 4;

update dbo.Transactions
set NumberOfTickets = 36, Amount = 72
where UserEmail = 'Tom.Andrews@c5alliance.com';

select * from dbo.Transactions

delete from dbo.Transactions where UserEmail = 'Tom.Andrews@c5alliance.com';

UPDATE Customers
SET ContactName = 'Alfred Schmidt', City= 'Frankfurt'
WHERE CustomerID = 1;

INSERT INTO dbo.Transactions(UserEmail, NumberOfTickets, Amount)
VALUES ('Tom.Andrews@c5alliance.com', 36, 72.00);

select sum(NumberOfTickets), sum(Amount) 
from dbo.Transactions group by UserEmail