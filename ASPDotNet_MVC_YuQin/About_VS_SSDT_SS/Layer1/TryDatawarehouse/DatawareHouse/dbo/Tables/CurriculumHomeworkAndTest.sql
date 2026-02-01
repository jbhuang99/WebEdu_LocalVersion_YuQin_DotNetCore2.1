CREATE TABLE [dbo].[CurriculumHomeworkAndTest] (
    [CurriculumHomeworkAndTestID]   INT            NOT NULL,
    [CurriculumHomeworkAndTestName] NVARCHAR (MAX) NULL,
    [FiveLayerMVCCategoryID]        INT            NOT NULL,
    CONSTRAINT [PK_CurriculumHomeworkAndTest] PRIMARY KEY CLUSTERED ([CurriculumHomeworkAndTestID] ASC),
    CONSTRAINT [FK_CurriculumHomeworkAndTest_FiveLayerMVCCategory_FiveLayerMVCCategoryID] FOREIGN KEY ([FiveLayerMVCCategoryID]) REFERENCES [dbo].[FiveLayerMVCCategory] ([FiveLayerMVCCategoryID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_CurriculumHomeworkAndTest_FiveLayerMVCCategoryID]
    ON [dbo].[CurriculumHomeworkAndTest]([FiveLayerMVCCategoryID] ASC);

