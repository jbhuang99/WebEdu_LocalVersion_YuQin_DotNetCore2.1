using CurriculumSelection.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CurriculumSelection.Data
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
                       $"INSERT INTO dbo.CurriculumCategory(CurriculumCategoryID,CurriculumCategoryName)  VALUES(1,'实践') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.CurriculumCategory(CurriculumCategoryID,CurriculumCategoryName)  VALUES(2,'技术') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.CurriculumCategory(CurriculumCategoryID,CurriculumCategoryName)  VALUES(3,'科学') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.CurriculumCategory(CurriculumCategoryID,CurriculumCategoryName)  VALUES(4,'人文') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.CurriculumCategory(CurriculumCategoryID,CurriculumCategoryName)  VALUES(5,'哲学') ");
            curriculumSelectionDbContext.SaveChanges();

                        
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.Curriculum(CurriculumID,CurriculumName,CurriculumCategoryID)  VALUES(1050, '化学',1) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.Curriculum(CurriculumID,CurriculumName,CurriculumCategoryID)  VALUES(4022, 'Microeconomics',1) "); 
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.Curriculum(CurriculumID,CurriculumName,CurriculumCategoryID)  VALUES(4041, 'Macroeconomics',1) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.Curriculum(CurriculumID,CurriculumName,CurriculumCategoryID)  VALUES(1045, 'Calculus',1) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.Curriculum(CurriculumID,CurriculumName,CurriculumCategoryID)  VALUES(3141, 'Trigonometry',1) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.Curriculum(CurriculumID,CurriculumName,CurriculumCategoryID)  VALUES(2021, 'Composition',1) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.Curriculum(CurriculumID,CurriculumName,CurriculumCategoryID)  VALUES(2042, 'Literature',1) ");
            curriculumSelectionDbContext.SaveChanges();


            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,Name)  VALUES(1,'学生1') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,Name)  VALUES(2,'HJB') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,Name)  VALUES(3,'Darson') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,Name)  VALUES(5,'Arturo') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,Name)  VALUES(7,'Gytis') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,Name)  VALUES(9,'Yan') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,Name)  VALUES(11,'Peggy') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,Name)  VALUES(13,'Laura') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,Name)  VALUES(15,'Nino') ");
            curriculumSelectionDbContext.SaveChanges();

            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Educator(EducatorID,Name,CurriculumID)  VALUES(1,'黄景碧',1045) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Educator(EducatorID,Name,CurriculumID)  VALUES(3,'Darson',1045) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Educator(EducatorID,Name,CurriculumID)  VALUES(5,'Arturo',1045) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Educator(EducatorID,Name,CurriculumID)  VALUES(7,'Gytis',1045) ");
            curriculumSelectionDbContext.SaveChanges();


            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(1, 1050,85.0) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(1, 4022,85.0) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(1, 4041,85.0) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(2, 1045,85.0) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(2, 3141,85.0) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(2, 1050,85.0) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(3, 1050,85.0) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(4, 1050,85.0) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(4, 4022,85.0) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(5, 4041,85.0) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(6, 1045,85.0) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(7, 3141,85.0) ");
          
            curriculumSelectionDbContext.SaveChanges();
        }
    }
}