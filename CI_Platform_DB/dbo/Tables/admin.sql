CREATE TABLE [dbo].[admin] (
    [admin_id]     BIGINT          IDENTITY (1, 1) NOT NULL,
    [first_name]   VARCHAR (16)    NULL,
    [last_name]    VARCHAR (16)    NULL,
    [email]        VARCHAR (128)   NOT NULL,
    [admin_avatar] NVARCHAR (2048) NULL,
    [password]     VARCHAR (255)   NOT NULL,
    [created_at]   DATETIME        DEFAULT (getdate()) NOT NULL,
    [updated_at]   DATETIME        NULL,
    [deleted_at]   DATETIME        NULL,
    PRIMARY KEY CLUSTERED ([admin_id] ASC),
    UNIQUE NONCLUSTERED ([email] ASC)
);

