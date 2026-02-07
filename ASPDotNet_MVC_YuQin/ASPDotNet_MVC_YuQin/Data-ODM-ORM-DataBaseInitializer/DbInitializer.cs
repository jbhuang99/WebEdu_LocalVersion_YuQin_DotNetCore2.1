using CurriculumSelection.DB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CurriculumSelection.DB.Data
{
    public static class DbInitializer
    {
        public static void Initialize(CurriculumSelectionDbContext curriculumSelectionDbContext)
        {
            curriculumSelectionDbContext.Database.EnsureCreated();

            // Look for any students.
            if (curriculumSelectionDbContext.LearnerDbSet.Any())
            {
                return;   // DB has been seeded
            }

            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.FiveLayerMVCCategory(FiveLayerMVCCategoryID,FiveLayerMVCCategoryName)  VALUES(1,'实践') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.FiveLayerMVCCategory(FiveLayerMVCCategoryID,FiveLayerMVCCategoryName)  VALUES(2,'技术') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.FiveLayerMVCCategory(FiveLayerMVCCategoryID,FiveLayerMVCCategoryName)  VALUES(3,'科学') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.FiveLayerMVCCategory(FiveLayerMVCCategoryID,FiveLayerMVCCategoryName)  VALUES(4,'人文') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.FiveLayerMVCCategory(FiveLayerMVCCategoryID,FiveLayerMVCCategoryName)  VALUES(5,'哲学') ");
            curriculumSelectionDbContext.SaveChanges();


            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.Curriculum(CurriculumID,CurriculumName,FiveLayerMVCCategoryID)  VALUES(1050, '化学',1) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.Curriculum(CurriculumID,CurriculumName,FiveLayerMVCCategoryID)  VALUES(4022, 'Microeconomics',1) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.Curriculum(CurriculumID,CurriculumName,FiveLayerMVCCategoryID)  VALUES(4041, 'Macroeconomics',1) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.Curriculum(CurriculumID,CurriculumName,FiveLayerMVCCategoryID)  VALUES(1045, 'Calculus',1) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.Curriculum(CurriculumID,CurriculumName,FiveLayerMVCCategoryID)  VALUES(3141, 'Trigonometry',1) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.Curriculum(CurriculumID,CurriculumName,FiveLayerMVCCategoryID)  VALUES(2021, 'Composition',1) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.Curriculum(CurriculumID,CurriculumName,FiveLayerMVCCategoryID)  VALUES(2042, 'Literature',1) ");
            curriculumSelectionDbContext.SaveChanges();

            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Person(PersonID,Name)  VALUES(1,'学生1') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Person(PersonID,Name)  VALUES(2,'hero') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Person(PersonID,Name)  VALUES(3,'Darson') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Person(PersonID,Name)  VALUES(5,'Arturo') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Person(PersonID,Name)  VALUES(7,'Gytis') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Person(PersonID,Name)  VALUES(9,'Yan') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Person(PersonID,Name)  VALUES(11,'Peggy') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Person(PersonID,Name)  VALUES(13,'Laura') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Person(PersonID,Name)  VALUES(15,'Nino') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Person(PersonID,Name)  VALUES(1001,'黄景碧') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Person(PersonID,Name)  VALUES(1003,'hjb2') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Person(PersonID,Name)  VALUES(1005,'hjb3') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Person(PersonID,Name)  VALUES(1007,'hjb4') ");
            curriculumSelectionDbContext.SaveChanges();

            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,PersonID)  VALUES(101,1) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,PersonID)  VALUES(102,2) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,PersonID)  VALUES(103,3) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,PersonID)  VALUES(105,5) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,PersonID)  VALUES(107,7) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,PersonID)  VALUES(109,9) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,PersonID)  VALUES(111,11) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,PersonID)  VALUES(113,13) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,PersonID)  VALUES(115,15) ");
            curriculumSelectionDbContext.SaveChanges();

            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Educator(EducatorID,PersonID,CurriculumID)  VALUES(1,1001,1045) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Educator(EducatorID,PersonID,CurriculumID)  VALUES(3,1003,1045) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Educator(EducatorID,PersonID,CurriculumID)  VALUES(5,1005,1045) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Educator(EducatorID,PersonID,CurriculumID)  VALUES(7,1007,1045) ");
            curriculumSelectionDbContext.SaveChanges();

            // Use a single open connection + transaction when toggling IDENTITY_INSERT
            // so SET IDENTITY_INSERT and the INSERTs run in the same session.
            curriculumSelectionDbContext.Database.OpenConnection();
            try
            {
                using (var tx = curriculumSelectionDbContext.Database.BeginTransaction())
                {
                    curriculumSelectionDbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.ScoreOfSelectedCurriculumByLearner ON;");
                    curriculumSelectionDbContext.Database.ExecuteSqlInterpolated($"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(ScoreOfSelectedCurriculumByLearnerID, PersonID, CurriculumID, Score) VALUES({1}, {1}, {1050}, {85.0}) ");
                    curriculumSelectionDbContext.Database.ExecuteSqlInterpolated($"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(ScoreOfSelectedCurriculumByLearnerID, PersonID, CurriculumID, Score) VALUES({2}, {9}, {3141}, {83.0}) ");
                    // curriculumSelectionDbContext.Database.ExecuteSqlInterpolated($"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(ScoreOfSelectedCurriculumByLearnerID, LearnerID, CurriculumID, Score) VALUES({1}, {101}, {1050}, {85.0}) ");//LearnerID外键出错
                    /**
                    curriculumSelectionDbContext.Database.ExecuteSqlInterpolated($"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(ScoreOfSelectedCurriculumByLearnerID, LearnerID, CurriculumID, Score) VALUES({2}, {102}, {4022}, {85.0}) ");
                    curriculumSelectionDbContext.Database.ExecuteSqlInterpolated($"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(ScoreOfSelectedCurriculumByLearnerID, LearnerID, CurriculumID, Score) VALUES({3}, {101}, {4041}, {85.0}) ");
                    curriculumSelectionDbContext.Database.ExecuteSqlInterpolated($"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(ScoreOfSelectedCurriculumByLearnerID, LearnerID, CurriculumID, Score) VALUES({5}, {102}, {1045}, {85.0}) ");
                    curriculumSelectionDbContext.Database.ExecuteSqlInterpolated($"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(ScoreOfSelectedCurriculumByLearnerID, LearnerID, CurriculumID, Score) VALUES({6}, {102}, {3141}, {85.0}) ");
                    curriculumSelectionDbContext.Database.ExecuteSqlInterpolated($"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(ScoreOfSelectedCurriculumByLearnerID, LearnerID, CurriculumID, Score) VALUES({7}, {102}, {1050}, {85.0}) ");
                    curriculumSelectionDbContext.Database.ExecuteSqlInterpolated($"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(ScoreOfSelectedCurriculumByLearnerID, LearnerID, CurriculumID, Score) VALUES({8}, {103}, {1050}, {85.0}) ");
                    curriculumSelectionDbContext.Database.ExecuteSqlInterpolated($"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(ScoreOfSelectedCurriculumByLearnerID, LearnerID, CurriculumID, Score) VALUES({9}, {105}, {1050}, {85.0}) ");
                    curriculumSelectionDbContext.Database.ExecuteSqlInterpolated($"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(ScoreOfSelectedCurriculumByLearnerID, LearnerID, CurriculumID, Score) VALUES({10}, {105}, {4022}, {85.0}) ");
                    curriculumSelectionDbContext.Database.ExecuteSqlInterpolated($"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(ScoreOfSelectedCurriculumByLearnerID, LearnerID, CurriculumID, Score) VALUES({20}, {105}, {4041}, {85.0}) ");
                    curriculumSelectionDbContext.Database.ExecuteSqlInterpolated($"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(ScoreOfSelectedCurriculumByLearnerID, LearnerID, CurriculumID, Score) VALUES({30}, {109}, {1045}, {85.0}) ");
                    curriculumSelectionDbContext.Database.ExecuteSqlInterpolated($"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(ScoreOfSelectedCurriculumByLearnerID, LearnerID, CurriculumID, Score) VALUES({31}, {107}, {3141}, {85.0}) ");
                    **/
                    curriculumSelectionDbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.ScoreOfSelectedCurriculumByLearner OFF;");
                    tx.Commit();
                }
            }
            finally
            {
                curriculumSelectionDbContext.Database.CloseConnection();
            }

            curriculumSelectionDbContext.SaveChanges();
        }
    }
}
