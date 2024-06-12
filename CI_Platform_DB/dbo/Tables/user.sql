CREATE TABLE [dbo].[user] (
    [user_id]         BIGINT          IDENTITY (1, 1) NOT NULL,
    [first_name]      VARCHAR (16)    NULL,
    [last_name]       VARCHAR (16)    NULL,
    [email]           VARCHAR (128)   NOT NULL,
    [password]        VARCHAR (255)   NOT NULL,
    [phone_number]    BIGINT          NOT NULL,
    [skills]          VARCHAR (255)   NULL,
    [why_i_volunteer] TEXT            NULL,
    [employee_id]     VARCHAR (16)    NULL,
    [department]      VARCHAR (16)    NULL,
    [city_id]         BIGINT          NULL,
    [country_id]      BIGINT          NULL,
    [profile_text]    TEXT            NULL,
    [linked_in_url]   VARCHAR (255)   NULL,
    [title]           VARCHAR (255)   NULL,
    [status]          TINYINT         NULL,
    [created_at]      DATETIME        DEFAULT (getdate()) NOT NULL,
    [updated_at]      DATETIME        NULL,
    [deleted_at]      DATETIME        NULL,
    [avatar]          VARBINARY (MAX) NULL,
    PRIMARY KEY CLUSTERED ([user_id] ASC),
    CHECK ([status]=(1) OR [status]=(0)),
    FOREIGN KEY ([city_id]) REFERENCES [dbo].[city] ([city_id]),
    FOREIGN KEY ([country_id]) REFERENCES [dbo].[country] ([country_id]),
    UNIQUE NONCLUSTERED ([email] ASC)
);



