CREATE TABLE [dbo].[story_media] (
    [media_id]   BIGINT          IDENTITY (1, 1) NOT NULL,
    [image]      VARBINARY (MAX) NULL,
    [document]   VARBINARY (MAX) NULL,
    [mission_id] BIGINT          NOT NULL,
    [story_id]   BIGINT          NOT NULL,
    PRIMARY KEY CLUSTERED ([media_id] ASC),
    FOREIGN KEY ([mission_id]) REFERENCES [dbo].[mission] ([mission_id]),
    FOREIGN KEY ([story_id]) REFERENCES [dbo].[story] ([story_id])
);

