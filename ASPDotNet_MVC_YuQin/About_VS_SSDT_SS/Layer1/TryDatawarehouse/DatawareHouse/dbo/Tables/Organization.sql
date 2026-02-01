CREATE TABLE [dbo].[Organization] (
    [OrganizationID]      INT            NOT NULL,
    [OrganizationName]    NVARCHAR (MAX) NULL,
    [OrganizationAddress] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Organization] PRIMARY KEY CLUSTERED ([OrganizationID] ASC)
);

