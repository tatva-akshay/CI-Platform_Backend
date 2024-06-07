CREATE TABLE [dbo].[mission_application] (
    [application_id] BIGINT   IDENTITY (1, 1) NOT NULL,
    [user_id]        BIGINT   NOT NULL,
    [mission_id]     BIGINT   NOT NULL,
    [created_at]     DATETIME DEFAULT (getdate()) NOT NULL,
    [updated_at]     DATETIME NULL,
    [deleted_at]     DATETIME NULL,
    [is_approved]    BIT      NULL,
    PRIMARY KEY CLUSTERED ([application_id] ASC),
    FOREIGN KEY ([mission_id]) REFERENCES [dbo].[mission] ([mission_id]),
    FOREIGN KEY ([user_id]) REFERENCES [dbo].[user] ([user_id])
);

