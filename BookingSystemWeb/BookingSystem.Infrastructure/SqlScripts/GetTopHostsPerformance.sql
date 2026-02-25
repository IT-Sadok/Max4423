SELECT a."HostId",
       COUNT(b."Id")       AS TotalBookings,
       SUM(b."TotalPrice") AS TotalRevenue
FROM "Apartments" a
         JOIN "Bookings" b ON a."Id" = b."ApartmentId"
GROUP BY a."HostId"
ORDER BY TotalRevenue DESC LIMIT 10;