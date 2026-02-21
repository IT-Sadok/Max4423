INSERT INTO "Apartments" ("Id",
                          "Title",
                          "Description",
                          "Address",
                          "PricePerNight",
                          "ExternalId",
                          "HostId",
                          "CustomData")
VALUES (@Id,
        @Title,
        @Description,
        @Address,
        @PricePerNight,
        @ExternalId,
        @HostId,
        @CustomData::jsonb) 
ON CONFLICT ("ExternalId")
DO
UPDATE SET
    "Title" = EXCLUDED."Title",
    "Description" = EXCLUDED."Description",
    "Address" = EXCLUDED."Address",
    "PricePerNight" = EXCLUDED."PricePerNight",
    "HostId" = EXCLUDED."HostId",
    "CustomData" = EXCLUDED."CustomData";