CREATE TABLE [dbo].[contact_uss] (
    [id]      BIGINT        IDENTITY (1, 1) NOT NULL,
    [user_id] BIGINT        NOT NULL,
    [subject] VARCHAR (255) NOT NULL,
    [message] TEXT          NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([user_id]) REFERENCES [dbo].[user] ([user_id])
);

