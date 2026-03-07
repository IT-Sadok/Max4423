INSERT INTO "Apartments" ("Title",
                          "Description",
                          "Address",
                          "PricePerNight",
                          "ExternalId",
                          "HostId",
                          "CustomData")
VALUES (@Title,
        @Description,
        @Address,
        @PricePerNight,
        @ExternalId,
        @HostId,
        @CustomData::jsonb) ON CONFLICT ("ExternalId")
DO
UPDATE SET
    "Title" = EXCLUDED."Title",
    "Description" = EXCLUDED."Description",
    "Address" = EXCLUDED."Address",
    "PricePerNight" = EXCLUDED."PricePerNight",
    "CustomData" = EXCLUDED."CustomData"
WHERE "Apartments"."HostId" = EXCLUDED."HostId"
    RETURNING "Id";