SELECT * 
FROM Customers;


SELECT BookingId, CustomerId, RoomId, StartDate, EndDate, Status
FROM Bookings
WHERE StartDate > '2025-01-01';

SELECT CustomerId, Name, Email, Phone
FROM Customers
ORDER BY Name ASC;

