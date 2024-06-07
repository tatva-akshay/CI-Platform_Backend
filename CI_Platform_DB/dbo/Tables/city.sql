CREATE TABLE [dbo].[city] (
    [city_id]    BIGINT       IDENTITY (1, 1) NOT NULL,
    [country_id] BIGINT       NOT NULL,
    [city]       VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([city_id] ASC),
    FOREIGN KEY ([country_id]) REFERENCES [dbo].[country] ([country_id])
);

