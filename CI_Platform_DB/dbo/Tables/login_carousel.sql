CREATE TABLE [dbo].[login_carousel] (
    [carousel_id]    BIGINT          IDENTITY (1, 1) NOT NULL,
    [carousel_image] VARBINARY (MAX) NOT NULL,
    [carousel_head]  VARCHAR (255)   NULL,
    [carousel_text]  VARCHAR (255)   NULL,
    [created_at]     DATETIME        DEFAULT (getdate()) NOT NULL,
    [updated_at]     DATETIME        NULL,
    [deleted_at]     DATETIME        NULL,
    PRIMARY KEY CLUSTERED ([carousel_id] ASC)
);

