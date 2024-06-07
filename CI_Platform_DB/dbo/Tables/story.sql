CREATE TABLE [dbo].[story] (
    [story_id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [story_title]       VARCHAR (50)   NOT NULL,
    [mission_title]     VARCHAR (50)   NOT NULL,
    [user_id]           BIGINT         NOT NULL,
    [story_description] VARCHAR (1000) NOT NULL,
    [publish]           BIT            NULL,
    [created_at]        DATETIME       DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([story_id] ASC),
    FOREIGN KEY ([user_id]) REFERENCES [dbo].[user] ([user_id])
);

