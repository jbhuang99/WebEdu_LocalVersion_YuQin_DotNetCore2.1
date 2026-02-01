CREATE TABLE [dbo].[Learner] (
    [LearnerID]           INT            NOT NULL,
    [PassportNumber]      NVARCHAR (MAX) NULL,
    [IndentityCordNumber] NVARCHAR (MAX) NULL,
    [TelephoneNumber]     NVARCHAR (MAX) NULL,
    [Email]               NVARCHAR (MAX) NULL,
    [Name]                NVARCHAR (MAX) NULL,
    [Gender]              BIT            NULL,
    [SorcePlace]          NVARCHAR (MAX) NULL,
    [Fav]                 NVARCHAR (MAX) NULL,
    [OrganizationID]      INT            NOT NULL,
    CONSTRAINT [PK_Learner] PRIMARY KEY CLUSTERED ([LearnerID] ASC),
    CONSTRAINT [FK_Learner_Organization_OrganizationID] FOREIGN KEY ([OrganizationID]) REFERENCES [dbo].[Organization] ([OrganizationID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Learner_OrganizationID]
    ON [dbo].[Learner]([OrganizationID] ASC);

