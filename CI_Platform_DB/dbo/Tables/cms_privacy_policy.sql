CREATE TABLE [dbo].[cms_privacy_policy] (
    [cms_id]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [page_title]       VARCHAR (50)   NOT NULL,
    [page_description] VARCHAR (2048) NOT NULL,
    [slug]             VARCHAR (1000) NOT NULL,
    [status]           BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([cms_id] ASC)
);

