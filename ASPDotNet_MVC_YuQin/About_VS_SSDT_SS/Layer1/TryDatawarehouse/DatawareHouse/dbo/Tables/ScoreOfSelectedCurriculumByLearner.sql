CREATE TABLE [dbo].[ScoreOfSelectedCurriculumByLearner] (
    [ScoreOfSelectedCurriculumByLearnerID]   INT            IDENTITY (1, 1) NOT NULL,
    [ScoreOfSelectedCurriculumByLearnerNote] NVARCHAR (MAX) NULL,
    [Score]                                  REAL           NOT NULL,
    [CurriculumID]                           INT            NOT NULL,
    [LearnerID]                              INT            NOT NULL,
    [CurriculumSelectedTime]                 DATETIME2 (7)  NULL,
    CONSTRAINT [PK_ScoreOfSelectedCurriculumByLearner] PRIMARY KEY CLUSTERED ([ScoreOfSelectedCurriculumByLearnerID] ASC),
    CONSTRAINT [FK_ScoreOfSelectedCurriculumByLearner_Curriculum_CurriculumID] FOREIGN KEY ([CurriculumID]) REFERENCES [dbo].[Curriculum] ([CurriculumID]) ON DELETE CASCADE,
    CONSTRAINT [FK_ScoreOfSelectedCurriculumByLearner_Learner_LearnerID] FOREIGN KEY ([LearnerID]) REFERENCES [dbo].[Learner] ([LearnerID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ScoreOfSelectedCurriculumByLearner_CurriculumID]
    ON [dbo].[ScoreOfSelectedCurriculumByLearner]([CurriculumID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ScoreOfSelectedCurriculumByLearner_LearnerID]
    ON [dbo].[ScoreOfSelectedCurriculumByLearner]([LearnerID] ASC);

