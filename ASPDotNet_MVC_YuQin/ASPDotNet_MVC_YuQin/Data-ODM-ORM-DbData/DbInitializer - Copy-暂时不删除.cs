using CurriculumSelection.Models;
using Microsoft.EntityFrameworkCore;
using OpenXmlPowerTools;
using System;
using System.Linq;

namespace CurriculumSelection.Data
{
    public static class DbInitializer
    {
        public static Int32 GetNum(Int32 loopNum, Int32 minValue, Int32 maxValue,Random random, Int32[] arrayNum) 
        {
            Int32 tmp= random.Next(minValue, maxValue);//生成minValue, maxValue之间的随机整数。NextDouble()：回传 0.0（含）~ 1.0（不含）的浮点数。
            arrayNum[loopNum] = tmp;
            Int32 arrNumLength = arrayNum.Length;            
            for (Int32 tmpNum = 0; tmpNum < arrNumLength; tmpNum++)
            {
               // Console.Write(arrNum[tmpNum].ToString() + "；");
                if (tmpNum != loopNum)
                {
                    if (arrayNum[tmpNum] == tmp)
                    {
                      //  Console.Write(arrNum[tmpNum].ToString()+"；");
                        GetNum(loopNum, minValue, maxValue, random, arrayNum);//递归:如果取出来的数字和已取得的数字有重复就重新随机获取。               
                    }
                }
            }            
            return arrayNum[loopNum];
        }
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

            ///**
           
            Int32 minValueCurriculumID = 1; Int32 maxValueCurriculumID = 801; Random randomCurriculumID = new Random(); Int32[] arrayNumCurriculumID = new Int32[maxValueCurriculumID - 1];
            for (Int32 i = 0; i < maxValueCurriculumID - 1; i++)
            {
                //Int32 curriculumId = random.Next(1, 801); //生成1到800之间的随机整数作为CurriculumID。NextDouble()：回传 0.0（含）~ 1.0（不含）的浮点数。
                Int32 curriculumID = GetNum(i, minValueCurriculumID, maxValueCurriculumID, randomCurriculumID, arrayNumCurriculumID);
                String curriculumName = "课程名称" + i.ToString()+"（待修改更真实仿制）";
                Int32 fiveLayerMVCCategoryID = randomCurriculumID.Next(1, 6);        
                curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                        $"INSERT INTO dbo.Curriculum(CurriculumID,CurriculumName,FiveLayerMVCCategoryID)  VALUES(" + curriculumID + ",'" + curriculumName + "'," + fiveLayerMVCCategoryID + ")"); //对应：curriculumSelectionDbContext.Database.ExecuteSqlRaw($"INSERT INTO dbo.Curriculum(CurriculumID,CurriculumName,FiveLayerMVCCategoryID)  VALUES(1050, '化学',1) ");
        }
          //  **/
           /**
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
            **/
            curriculumSelectionDbContext.SaveChanges();

            Int32 minValueOrganizationID = 1; Int32 maxValueOrganizationID = 161; Random randomOrganizationID = new Random(); Int32[] arrayNumOrganizationID = new Int32[maxValueOrganizationID - 1];
            for (Int32 i = 0; i < maxValueOrganizationID - 1; i++)
            {
                //Int32 curriculumId = random.Next(1, 801); //生成1到800之间的随机整数作为CurriculumID。NextDouble()：回传 0.0（含）~ 1.0（不含）的浮点数。
                Int32 OrganizationID = GetNum(i, minValueOrganizationID, maxValueOrganizationID, randomOrganizationID, arrayNumOrganizationID);
                String OrganizationName = "机构名称" + i.ToString() + "（待修改更真实仿制）";
                String OrganizationAddress ="地址" + i.ToString() + "（待修改更真实仿制）";
                curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                        $"INSERT INTO dbo.Organization(OrganizationID,OrganizationName,OrganizationAddress)  VALUES(" + OrganizationID + ",'" + OrganizationName + "','"+OrganizationAddress + "')"); //对应：curriculumSelectionDbContext.Database.ExecuteSqlRaw(                $"INSERT INTO dbo.Organization(OrganizationID,OrganizationName,OrganizationAddress)  VALUES(3300, '师范大学','江西') "); ;
            }

            /**
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.Organization(OrganizationID,OrganizationName,OrganizationAddress)  VALUES(3300, '师范大学','江西') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.Organization(OrganizationID,OrganizationName,OrganizationAddress)  VALUES(1001, '科技公司','北京') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                      $"INSERT INTO dbo.Organization(OrganizationID,OrganizationName,OrganizationAddress)  VALUES(3200, '理工公司大学','荷兰') ");
           **/
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