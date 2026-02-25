SELECT "HostId",
       COUNT("Id")                    AS TotalApartments,
       ROUND(AVG("PricePerNight"), 2) AS AveragePrice,
       MIN("PricePerNight")           AS MinPrice,
       MAX("PricePerNight")           AS MaxPrice
FROM "Apartments"
GROUP BY "HostId"
HAVING COUNT("Id") >= 2
ORDER BY TotalApartments DESC
LIMIT @Limit OFFSET @Offset;