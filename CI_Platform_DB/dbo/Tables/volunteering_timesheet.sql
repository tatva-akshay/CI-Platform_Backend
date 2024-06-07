CREATE TABLE [dbo].[volunteering_timesheet] (
    [volunteering_id] BIGINT       IDENTITY (1, 1) NOT NULL,
    [mission_id]      BIGINT       NOT NULL,
    [mission_title]   VARCHAR (50) NOT NULL,
    [date]            DATE         NOT NULL,
    [hours]           TIME (7)     NOT NULL,
    [minutes]         TIME (7)     NOT NULL,
    [action]          BIGINT       NOT NULL,
    PRIMARY KEY CLUSTERED ([volunteering_id] ASC),
    FOREIGN KEY ([mission_id]) REFERENCES [dbo].[mission] ([mission_id])
);

