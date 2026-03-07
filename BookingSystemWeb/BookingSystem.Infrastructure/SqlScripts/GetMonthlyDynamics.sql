SELECT
    TO_CHAR(b."CheckInDate", 'YYYY-MM') AS BookingMonth,
    COUNT(b."Id") AS TotalBookings,
    ROUND(AVG(b."TotalPrice"), 2) AS AverageCheck,
    SUM(b."TotalPrice") AS MonthlyRevenue
FROM "Bookings" b
GROUP BY TO_CHAR(b."CheckInDate", 'YYYY-MM')
ORDER BY BookingMonth DESC;