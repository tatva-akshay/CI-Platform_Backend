CREATE TABLE [dbo].[user_information] (
    [information_id] BIGINT        IDENTITY (1, 1) NOT NULL,
    [user_id]        BIGINT        NOT NULL,
    [description]    VARCHAR (255) NOT NULL,
    [gender]         INT           NOT NULL,
    [availability]   INT           NOT NULL,
    [age_group]      INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([information_id] ASC),
    CHECK ([age_group]=(6) OR [age_group]=(5) OR [age_group]=(4) OR [age_group]=(3) OR [age_group]=(2) OR [age_group]=(1)),
    CHECK ([availability]=(3) OR [availability]=(2) OR [availability]=(1)),
    CHECK ([gender]=(3) OR [gender]=(2) OR [gender]=(1)),
    FOREIGN KEY ([user_id]) REFERENCES [dbo].[user] ([user_id])
);

