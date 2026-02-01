CREATE TABLE [dbo].[ScoreOfSelectedCurriculumHomeworkAndTestByLearner] (
    [ScoreOfSelectedCurriculumHomeworkAndTestByLearnerID]   INT            IDENTITY (1, 1) NOT NULL,
    [ScoreOfSelectedCurriculumHomeworkAndTestByLearnerNote] NVARCHAR (MAX) NULL,
    [Score]                                                 INT            NOT NULL,
    [LearnerID]                                             INT            NOT NULL,
    [CurriculumHomeworkAndTestSelectedTime]                 DATETIME2 (7)  NULL,
    [CurriculumHomeworkAndTestID]                           INT            NULL,
    CONSTRAINT [PK_ScoreOfSelectedCurriculumHomeworkAndTestByLearner] PRIMARY KEY CLUSTERED ([ScoreOfSelectedCurriculumHomeworkAndTestByLearnerID] ASC),
    CONSTRAINT [FK_ScoreOfSelectedCurriculumHomeworkAndTestByLearner_CurriculumHomeworkAndTest_CurriculumHomeworkAndTestID] FOREIGN KEY ([CurriculumHomeworkAndTestID]) REFERENCES [dbo].[CurriculumHomeworkAndTest] ([CurriculumHomeworkAndTestID]),
    CONSTRAINT [FK_ScoreOfSelectedCurriculumHomeworkAndTestByLearner_Learner_LearnerID] FOREIGN KEY ([LearnerID]) REFERENCES [dbo].[Learner] ([LearnerID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ScoreOfSelectedCurriculumHomeworkAndTestByLearner_CurriculumHomeworkAndTestID]
    ON [dbo].[ScoreOfSelectedCurriculumHomeworkAndTestByLearner]([CurriculumHomeworkAndTestID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ScoreOfSelectedCurriculumHomeworkAndTestByLearner_LearnerID]
    ON [dbo].[ScoreOfSelectedCurriculumHomeworkAndTestByLearner]([LearnerID] ASC);

