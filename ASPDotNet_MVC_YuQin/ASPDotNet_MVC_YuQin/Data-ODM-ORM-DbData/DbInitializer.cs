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
                      $"INSERT INTO dbo.Organization(OrganizationID,OrganizationName,OrganizationAddress)  VALUES(3300, '师范大学','江西') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.Organization(OrganizationID,OrganizationName,OrganizationAddress)  VALUES(1001, '科技公司','北京') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.Organization(OrganizationID,OrganizationName,OrganizationAddress)  VALUES(3200, '理工公司大学','荷兰') ");
            curriculumSelectionDbContext.SaveChanges();

            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,Name,OrganizationID)  VALUES(1,'学生1',3300) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,Name,OrganizationID)  VALUES(2,'HJB',3300) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,Name,OrganizationID)  VALUES(3,'Darson',3300) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,Name,OrganizationID)  VALUES(5,'Arturo',3300) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,Name,OrganizationID)  VALUES(7,'Gytis',3300) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,Name,OrganizationID)  VALUES(9,'Yan',3300) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,Name,OrganizationID)  VALUES(11,'Peggy',3300) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,Name,OrganizationID)  VALUES(13,'Laura',3300) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,Name,OrganizationID)  VALUES(15,'Nino',3300) ");
            curriculumSelectionDbContext.SaveChanges();

            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Educator(EducatorID,Name,CurriculumID,OrganizationID)  VALUES(1,'黄景碧',1045,3300) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Educator(EducatorID,Name,CurriculumID,OrganizationID)  VALUES(3,'Darson',1045,3300) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Educator(EducatorID,Name,CurriculumID,OrganizationID)  VALUES(5,'Arturo',1045,3300) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Educator(EducatorID,Name,CurriculumID,OrganizationID)  VALUES(7,'Gytis',1045,3300) ");
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
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(5, 1050,85.0) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(5, 4022,85.0) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(5, 4041,85.0) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(7, 1045,85.0) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(7, 3141,85.0) ");
            curriculumSelectionDbContext.SaveChanges();
            
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.CurriculumHomeworkAndTest(CurriculumHomeworkAndTestID,CurriculumHomeworkAndTestName,FiveLayerMVCCategoryID)  VALUES(6050,'CurriculumHomeworkAndTestItemName1',1) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.CurriculumHomeworkAndTest(CurriculumHomeworkAndTestID,CurriculumHomeworkAndTestName,FiveLayerMVCCategoryID)  VALUES(7050,'CurriculumHomeworkAndTestItemName2',1) "); 
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.CurriculumHomeworkAndTest(CurriculumHomeworkAndTestID,CurriculumHomeworkAndTestName,FiveLayerMVCCategoryID)  VALUES(8050,'CurriculumHomeworkAndTestItemName3',1) "); 
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.CurriculumHomeworkAndTest(CurriculumHomeworkAndTestID,CurriculumHomeworkAndTestName,FiveLayerMVCCategoryID)  VALUES(9050,'CurriculumHomeworkAndTestItemName4',1) ");
            curriculumSelectionDbContext.SaveChanges();

            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumHomeworkAndTestByLearner(LearnerID,CurriculumHomeworkAndTestID,Score)  VALUES(1, 7050,100) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumHomeworkAndTestByLearner(LearnerID,CurriculumHomeworkAndTestID,Score)  VALUES(1, 6050,100) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumHomeworkAndTestByLearner(LearnerID,CurriculumHomeworkAndTestID,Score)  VALUES(1, 6050,0) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumHomeworkAndTestByLearner(LearnerID,CurriculumHomeworkAndTestID,Score)  VALUES(2, 6050,100) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumHomeworkAndTestByLearner(LearnerID,CurriculumHomeworkAndTestID,Score)  VALUES(2, 6050,0) ");
            curriculumSelectionDbContext.SaveChanges();
        }
    }
}