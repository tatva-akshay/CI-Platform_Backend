CREATE TABLE [dbo].[theme] (
    [theme_id] BIGINT       IDENTITY (1, 1) NOT NULL,
    [theme]    VARCHAR (50) NOT NULL,
    [status]   BIT          NOT NULL,
    PRIMARY KEY CLUSTERED ([theme_id] ASC)
);

