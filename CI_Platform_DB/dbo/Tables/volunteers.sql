CREATE TABLE [dbo].[volunteers] (
    [volunteer_id] BIGINT   IDENTITY (1, 1) NOT NULL,
    [mission_id]   BIGINT   NOT NULL,
    [user_id]      BIGINT   NOT NULL,
    [status]       INT      DEFAULT ((1)) NOT NULL,
    [created_at]   DATETIME DEFAULT (getdate()) NOT NULL,
    [updated_at]   DATETIME NULL,
    [deleted_at]   DATETIME NULL,
    PRIMARY KEY CLUSTERED ([volunteer_id] ASC),
    FOREIGN KEY ([mission_id]) REFERENCES [dbo].[mission] ([mission_id]),
    FOREIGN KEY ([user_id]) REFERENCES [dbo].[user] ([user_id])
);

