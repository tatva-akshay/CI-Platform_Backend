CREATE TABLE [dbo].[comments] (
    [comment_id]    BIGINT        IDENTITY (1, 1) NOT NULL,
    [mission_title] VARCHAR (50)  NOT NULL,
    [user_id]       BIGINT        NOT NULL,
    [user_name]     VARCHAR (50)  NOT NULL,
    [comment]       VARCHAR (256) NULL,
    [created_at]    DATETIME      DEFAULT (getdate()) NOT NULL,
    [updated_at]    DATETIME      NULL,
    [deleted_at]    DATETIME      NULL,
    [mission_id]    BIGINT        NULL,
    PRIMARY KEY CLUSTERED ([comment_id] ASC),
    FOREIGN KEY ([mission_id]) REFERENCES [dbo].[mission] ([mission_id]),
    FOREIGN KEY ([user_id]) REFERENCES [dbo].[user] ([user_id])
);

