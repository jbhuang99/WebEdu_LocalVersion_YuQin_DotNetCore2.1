CREATE TABLE [dbo].[Curriculum] (
    [CurriculumID]           INT            NOT NULL,
    [CurriculumName]         NVARCHAR (MAX) NULL,
    [FiveLayerMVCCategoryID] INT            NOT NULL,
    CONSTRAINT [PK_Curriculum] PRIMARY KEY CLUSTERED ([CurriculumID] ASC),
    CONSTRAINT [FK_Curriculum_FiveLayerMVCCategory_FiveLayerMVCCategoryID] FOREIGN KEY ([FiveLayerMVCCategoryID]) REFERENCES [dbo].[FiveLayerMVCCategory] ([FiveLayerMVCCategoryID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Curriculum_FiveLayerMVCCategoryID]
    ON [dbo].[Curriculum]([FiveLayerMVCCategoryID] ASC);

