CREATE TABLE [dbo].[mission_favs] (
    [favourite_id] BIGINT IDENTITY (1, 1) NOT NULL,
    [mission_id]   BIGINT NOT NULL,
    [user_id]      BIGINT NOT NULL,
    PRIMARY KEY CLUSTERED ([favourite_id] ASC),
    FOREIGN KEY ([mission_id]) REFERENCES [dbo].[mission] ([mission_id]),
    FOREIGN KEY ([user_id]) REFERENCES [dbo].[user] ([user_id])
);

