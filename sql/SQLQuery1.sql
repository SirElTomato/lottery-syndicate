update dbo.Transactions
set UserEmail = 'Owen.Harper@c5alliance.com'
where NumberOfTickets = 4;

update dbo.Transactions
set BuyDate = '2017-10-04 00:00:00.000', TimeStamp = '2017-10-04 00:00:00.000'
where UserEmail = 'katy.hughes@c5alliance.com';

select * from dbo.Transactions

delete from dbo.Transactions 
where UserEmail = 'katy.hughes@c5alliance.com';

UPDATE Customers
SET ContactName = 'Alfred Schmidt', City= 'Frankfurt'
WHERE CustomerID = 1;

INSERT INTO dbo.Transactions(UserEmail, NumberOfTickets, Amount)
VALUES ('Jack.Tomlinson@c5alliance.com', 20, 40.00);

select sum(NumberOfTickets), sum(Amount) 
from dbo.Transactions group by UserEmail