SELECT a."Id"                                                           AS ApartmentId,
       a."Title",
       COUNT(b."Id")                                                    AS BookingsCount,
       COALESCE(SUM(b."CheckOutDate"::date - b."CheckInDate"::date), 0) AS TotalBookedDays
FROM "Apartments" a
         LEFT JOIN "Bookings" b ON a."Id" = b."ApartmentId"
    AND b."CheckInDate" >= @StartDate
    AND b."CheckInDate" <= @EndDate
GROUP BY a."Id", a."Title"
ORDER BY TotalBookedDays DESC;