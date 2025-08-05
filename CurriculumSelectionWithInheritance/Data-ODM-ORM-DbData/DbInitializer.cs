using CurriculumSelectionWithInheritance.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CurriculumSelectionWithInheritance.Data
{
    public static class DbInitializer
    {
        public static void Initialize(CurriculumSelectionWithInheritanceDbContext curriculumSelectionWithInheritanceDbContext)
        {
            curriculumSelectionWithInheritanceDbContext.Database.EnsureCreated();

            // Look for any students.
            if (curriculumSelectionWithInheritanceDbContext.LearnerDbSet.Any())
            {
                return;   // DB has been seeded
            }

            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.CurriculumCategory(CurriculumCategoryID,CurriculumCategoryName)  VALUES(1,'实践') ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.CurriculumCategory(CurriculumCategoryID,CurriculumCategoryName)  VALUES(2,'技术') ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.CurriculumCategory(CurriculumCategoryID,CurriculumCategoryName)  VALUES(3,'科学') ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.CurriculumCategory(CurriculumCategoryID,CurriculumCategoryName)  VALUES(4,'人文') ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.CurriculumCategory(CurriculumCategoryID,CurriculumCategoryName)  VALUES(5,'哲学') ");
            curriculumSelectionWithInheritanceDbContext.SaveChanges();


            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.Curriculum(CurriculumID,CurriculumName,CurriculumCategoryID)  VALUES(1050, '化学',1) ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.Curriculum(CurriculumID,CurriculumName,CurriculumCategoryID)  VALUES(4022, 'Microeconomics',1) ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.Curriculum(CurriculumID,CurriculumName,CurriculumCategoryID)  VALUES(4041, 'Macroeconomics',1) ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.Curriculum(CurriculumID,CurriculumName,CurriculumCategoryID)  VALUES(1045, 'Calculus',1) ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.Curriculum(CurriculumID,CurriculumName,CurriculumCategoryID)  VALUES(3141, 'Trigonometry',1) ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.Curriculum(CurriculumID,CurriculumName,CurriculumCategoryID)  VALUES(2021, 'Composition',1) ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.Curriculum(CurriculumID,CurriculumName,CurriculumCategoryID)  VALUES(2042, 'Literature',1) ");
            curriculumSelectionWithInheritanceDbContext.SaveChanges();

            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Person(PersonID,Name)  VALUES(1,'学生1') ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Person(PersonID,Name)  VALUES(2,'hero') ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Person(PersonID,Name)  VALUES(3,'Darson') ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Person(PersonID,Name)  VALUES(5,'Arturo') ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Person(PersonID,Name)  VALUES(7,'Gytis') ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Person(PersonID,Name)  VALUES(9,'Yan') ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Person(PersonID,Name)  VALUES(11,'Peggy') ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Person(PersonID,Name)  VALUES(13,'Laura') ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Person(PersonID,Name)  VALUES(15,'Nino') ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Person(PersonID,Name)  VALUES(1001,'黄景碧') ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Person(PersonID,Name)  VALUES(1003,'hjb2') ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Person(PersonID,Name)  VALUES(1005,'hjb3') ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Person(PersonID,Name)  VALUES(1007,'hjb4') ");
            curriculumSelectionWithInheritanceDbContext.SaveChanges();

            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,PersonID)  VALUES(101,1) ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,PersonID)  VALUES(102,2) ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,PersonID)  VALUES(103,3) ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,PersonID)  VALUES(105,5) ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,PersonID)  VALUES(107,7) ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,PersonID)  VALUES(109,9) ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,PersonID)  VALUES(111,11) ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,PersonID)  VALUES(113,13) ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,PersonID)  VALUES(115,15) ");
            curriculumSelectionWithInheritanceDbContext.SaveChanges();

            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Educator(EducatorID,PersonID,CurriculumID)  VALUES(1,1001,1045) ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Educator(EducatorID,PersonID,CurriculumID)  VALUES(3,1003,1045) ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Educator(EducatorID,PersonID,CurriculumID)  VALUES(5,1005,1045) ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Educator(EducatorID,PersonID,CurriculumID)  VALUES(7,1007,1045) ");
            curriculumSelectionWithInheritanceDbContext.SaveChanges();


            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(101, 1050, 85.0) ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(102, 4022, 85.0) ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(101, 4041, 85.0) ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(102, 1045, 85.0) ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(102, 3141, 85.0) ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(102, 1050, 85.0) ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(103, 1050, 85.0) ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(104, 1050, 85.0) ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(104, 4022, 85.0) ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(105, 4041, 85.0) ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(106, 1045, 85.0) ");
            curriculumSelectionWithInheritanceDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(107, 3141, 85.0) ");

            /**
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(PersonID,CurriculumID,Score)  VALUES(1, 1050,85.0) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(PersonID,CurriculumID,Score)  VALUES(2, 4022,85.0) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(PersonID,CurriculumID,Score)  VALUES(1, 4041,85.0) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(PersonID,CurriculumID,Score)  VALUES(2, 1045,85.0) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(PersonID,CurriculumID,Score)  VALUES(2, 3141,85.0) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(PersonID,CurriculumID,Score)  VALUES(2, 1050,85.0) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(PersonID,CurriculumID,Score)  VALUES(3, 1050,85.0) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(PersonID,CurriculumID,Score)  VALUES(4, 1050,85.0) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(PersonID,CurriculumID,Score)  VALUES(4, 4022,85.0) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(PersonID,CurriculumID,Score)  VALUES(5, 4041,85.0) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(PersonID,CurriculumID,Score)  VALUES(6, 1045,85.0) ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                     $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(PersonID,CurriculumID,Score)  VALUES(7, 3141,85.0) ");
            **/
            curriculumSelectionWithInheritanceDbContext.SaveChanges();
        }
    }
}