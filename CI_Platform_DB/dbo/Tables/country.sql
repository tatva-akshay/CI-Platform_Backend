CREATE TABLE [dbo].[country] (
    [country_id] BIGINT       IDENTITY (1, 1) NOT NULL,
    [country]    VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([country_id] ASC)
);

