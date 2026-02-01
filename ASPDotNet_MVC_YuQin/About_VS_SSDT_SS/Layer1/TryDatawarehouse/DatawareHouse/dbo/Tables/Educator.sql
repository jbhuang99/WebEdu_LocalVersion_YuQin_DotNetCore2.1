CREATE TABLE [dbo].[Educator] (
    [EducatorID]                  INT            NOT NULL,
    [PassportNumber]              NVARCHAR (MAX) NULL,
    [IndentityCordNumber]         NVARCHAR (MAX) NULL,
    [TelephoneNumber]             NVARCHAR (MAX) NULL,
    [Email]                       NVARCHAR (MAX) NULL,
    [Name]                        NVARCHAR (MAX) NULL,
    [CurriculumID]                INT            NOT NULL,
    [OrganizationID]              INT            NOT NULL,
    [CurriculumHomeworkAndTestID] INT            NULL,
    CONSTRAINT [PK_Educator] PRIMARY KEY CLUSTERED ([EducatorID] ASC),
    CONSTRAINT [FK_Educator_Curriculum_CurriculumID] FOREIGN KEY ([CurriculumID]) REFERENCES [dbo].[Curriculum] ([CurriculumID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Educator_CurriculumHomeworkAndTest_CurriculumHomeworkAndTestID] FOREIGN KEY ([CurriculumHomeworkAndTestID]) REFERENCES [dbo].[CurriculumHomeworkAndTest] ([CurriculumHomeworkAndTestID]),
    CONSTRAINT [FK_Educator_Organization_OrganizationID] FOREIGN KEY ([OrganizationID]) REFERENCES [dbo].[Organization] ([OrganizationID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Educator_CurriculumHomeworkAndTestID]
    ON [dbo].[Educator]([CurriculumHomeworkAndTestID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Educator_CurriculumID]
    ON [dbo].[Educator]([CurriculumID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Educator_OrganizationID]
    ON [dbo].[Educator]([OrganizationID] ASC);

