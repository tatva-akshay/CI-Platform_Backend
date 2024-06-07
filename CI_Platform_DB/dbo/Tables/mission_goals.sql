CREATE TABLE [dbo].[mission_goals] (
    [goal_id]     BIGINT        IDENTITY (1, 1) NOT NULL,
    [mission_id]  BIGINT        NOT NULL,
    [goal]        VARCHAR (100) NOT NULL,
    [goal_status] INT           NULL,
    PRIMARY KEY CLUSTERED ([goal_id] ASC),
    FOREIGN KEY ([mission_id]) REFERENCES [dbo].[mission] ([mission_id])
);

