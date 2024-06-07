CREATE TABLE [dbo].[skills] (
    [skill_id] BIGINT       IDENTITY (1, 1) NOT NULL,
    [skills]   VARCHAR (20) NOT NULL,
    [status]   BIT          NOT NULL,
    PRIMARY KEY CLUSTERED ([skill_id] ASC)
);

