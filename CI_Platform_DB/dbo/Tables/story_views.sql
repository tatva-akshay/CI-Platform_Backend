CREATE TABLE [dbo].[story_views] (
    [view_id]  BIGINT IDENTITY (1, 1) NOT NULL,
    [story_id] BIGINT NOT NULL,
    [user_ids] TEXT   NULL,
    PRIMARY KEY CLUSTERED ([view_id] ASC),
    FOREIGN KEY ([story_id]) REFERENCES [dbo].[story] ([story_id])
);

