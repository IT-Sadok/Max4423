SELECT a."HostId",
       u."UserName" AS HostName,
       COUNT(a."Id")                    AS TotalApartments,
       ROUND(AVG(a."PricePerNight"), 2) AS AveragePrice,
       MIN(a."PricePerNight")           AS MinPrice,
       MAX(a."PricePerNight")           AS MaxPrice
FROM "Apartments" a
JOIN "AspNetUsers" u ON a."HostId" = u."Id"
GROUP BY a."HostId",
         u."UserName"
HAVING COUNT(a."Id") >= 2
ORDER BY TotalApartments DESC
LIMIT @Limit OFFSET @Offset;