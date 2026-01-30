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
            curriculumSelectionDbContext.SaveChanges();

            Int32 minValueCurriculumHomeworkAndTestID = 1; Int32 maxValueCurriculumHomeworkAndTestID = 8001; Random randomCurriculumHomeworkAndTestID = new Random(); Int32[] arrayNumCurriculumHomeworkAndTestID = new Int32[maxValueCurriculumHomeworkAndTestID - 1];
            for (Int32 i = 0; i < maxValueCurriculumHomeworkAndTestID - 1; i++)
            {
                //Int32 curriculumId = random.Next(1, 801); //生成1到800之间的随机整数作为CurriculumID。NextDouble()：回传 0.0（含）~ 1.0（不含）的浮点数。
                Int32 curriculumHomeworkAndTestID = GetNum(i, minValueCurriculumHomeworkAndTestID, maxValueCurriculumHomeworkAndTestID, randomCurriculumHomeworkAndTestID, arrayNumCurriculumHomeworkAndTestID);
                String curriculumHomeworkAndTestName = "课程作业测验名称" + i.ToString() + "（待修改更真实仿制）";
                Int32 fiveLayerMVCCategoryID = randomCurriculumHomeworkAndTestID.Next(1, 6);
                curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                        $"INSERT INTO dbo.CurriculumHomeworkAndTest(CurriculumHomeworkAndTestID,CurriculumHomeworkAndTestName,FiveLayerMVCCategoryID)  VALUES(" + curriculumHomeworkAndTestID + ",'" + curriculumHomeworkAndTestName + "'," + fiveLayerMVCCategoryID + ")"); //对应： curriculumSelectionDbContext.Database.ExecuteSqlRaw(                $"INSERT INTO dbo.CurriculumHomeworkAndTest(CurriculumHomeworkAndTestID,CurriculumHomeworkAndTestName,FiveLayerMVCCategoryID)  VALUES(6050,'CurriculumHomeworkAndTestItemName1',1) ");          
                curriculumSelectionDbContext.SaveChanges();
            }

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
            curriculumSelectionDbContext.SaveChanges();


            Int32 minValueLearnerID = 1; Int32 maxValueLearnerID = 801; Random randomLearnerID = new Random(); Int32[] arrayNumLearnerID = new Int32[maxValueLearnerID - 1];
            for (Int32 i = 0; i < maxValueLearnerID - 1; i++)
            {
                //Int32 curriculumId = random.Next(1, 801); //生成1到800之间的随机整数作为CurriculumID。NextDouble()：回传 0.0（含）~ 1.0（不含）的浮点数。
                Int32 learnerID = GetNum(i, minValueLearnerID, maxValueLearnerID, randomLearnerID, arrayNumLearnerID);
                String learnerName = "学生名称" + i.ToString() + "（待修改更真实仿制）";
                Int32 organizationID = randomLearnerID.Next(1,160);
                curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                        $"INSERT INTO dbo.Learner(LearnerID,Name,OrganizationID)  VALUES(" + learnerID + ",'" + learnerName + "'," + organizationID + ")"); //对应：curriculumSelectionDbContext.Database.ExecuteSqlRaw( $"INSERT INTO dbo.Learner(LearnerID,Name,OrganizationID)  VALUES(1,'学生1',3300) ");
            }        
            curriculumSelectionDbContext.SaveChanges();


            Int32 minValueEducatorID = 1; Int32 maxValueEducatorID = 201; Random randomEducatorID = new Random(); Int32[] arrayNumEducatorID = new Int32[maxValueEducatorID - 1];
            for (Int32 i = 0; i < maxValueLearnerID - 1; i++)
            {
                //Int32 curriculumId = random.Next(1, 801); //生成1到800之间的随机整数作为CurriculumID。NextDouble()：回传 0.0（含）~ 1.0（不含）的浮点数。
                Int32 educatorID = GetNum(i, minValueEducatorID, maxValueEducatorID, randomEducatorID, arrayNumEducatorID);
                String educatorName = "教师名称" + i.ToString() + "（待修改更真实仿制）";
                Int32 curriculumID = randomEducatorID.Next(1, 800);
                Int32 organizationID = randomEducatorID.Next(1, 160);
                curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                        $"INSERT INTO dbo.Educator(EducatorID,Name,OrganizationID,CurriculumID)  VALUES(" + educatorID + ",'" + educatorName + "'," + organizationID + "," + curriculumID + ")"); //对应：curriculumSelectionDbContext.Database.ExecuteSqlRaw($"INSERT INTO dbo.Educator(EducatorID,Name,CurriculumID,OrganizationID)  VALUES(1,'黄景碧',1045,3300) "); ;
            }        
            curriculumSelectionDbContext.SaveChanges();

            Int32 minValueScoreOfSelectedCurriculumByLearnerID = 1; Int32 maxValueScoreOfSelectedCurriculumByLearnerID = 6401; Random randomScoreOfSelectedCurriculumByLearnerID = new Random(); Int32[] arrayNumScoreOfSelectedCurriculumByLearnerID = new Int32[maxValueScoreOfSelectedCurriculumByLearnerID - 1];
            for (Int32 i = 0; i < maxValueScoreOfSelectedCurriculumByLearnerID - 1; i++)
            {
                //Int32 curriculumId = random.Next(1, 801); //生成1到800之间的随机整数作为CurriculumID。NextDouble()：回传 0.0（含）~ 1.0（不含）的浮点数。
                Int32 scoreOfSelectedCurriculumByLearnerID = GetNum(i, minValueScoreOfSelectedCurriculumByLearnerID, maxValueScoreOfSelectedCurriculumByLearnerID, randomScoreOfSelectedCurriculumByLearnerID, arrayNumScoreOfSelectedCurriculumByLearnerID);
                Int32 curriculumID = randomScoreOfSelectedCurriculumByLearnerID.Next(1, 800);
                Int32 learnerID = randomScoreOfSelectedCurriculumByLearnerID.Next(1, 800);
                Single score = (Single)randomScoreOfSelectedCurriculumByLearnerID.NextDouble();
                curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                        $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(ScoreOfSelectedCurriculumByLearnerID,CurriculumID,LearnerID,Score)  VALUES(" + scoreOfSelectedCurriculumByLearnerID+ "," + curriculumID + "," + learnerID + "," + score + ")"); //对应：curriculumSelectionDbContext.Database.ExecuteSqlRaw(                $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(1, 1050,85.0) ");;
            }            
            curriculumSelectionDbContext.SaveChanges();

            Int32 minValueScoreOfSelectedCurriculumHomeworkAndTestByLearnerID = 1; Int32 maxValueScoreOfSelectedCurriculumHomeworkAndTestByLearnerID = 8001; Random randomScoreOfSelectedCurriculumHomeworkAndTestByLearnerID = new Random(); Int32[] arrayNumScoreOfSelectedCurriculumHomeworkAndTestByLearnerID = new Int32[maxValueScoreOfSelectedCurriculumHomeworkAndTestByLearnerID - 1];
            for (Int32 i = 0; i < maxValueScoreOfSelectedCurriculumHomeworkAndTestByLearnerID - 1; i++)
            {
                //Int32 curriculumId = random.Next(1, 801); //生成1到800之间的随机整数作为CurriculumID。NextDouble()：回传 0.0（含）~ 1.0（不含）的浮点数。
                Int32 scoreOfScoreOfSelectedCurriculumHomeworkAndTestByLearnerID = GetNum(i, minValueScoreOfSelectedCurriculumHomeworkAndTestByLearnerID, maxValueScoreOfSelectedCurriculumHomeworkAndTestByLearnerID, randomScoreOfSelectedCurriculumHomeworkAndTestByLearnerID, arrayNumScoreOfSelectedCurriculumHomeworkAndTestByLearnerID);
                Int32 learnerID = randomScoreOfSelectedCurriculumByLearnerID.Next(1, 800);
                Single score = (Single)randomScoreOfSelectedCurriculumByLearnerID.NextDouble();
                curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                        $"INSERT INTO dbo.ScoreOfSelectedCurriculumHomeworkAndTestByLearner(ScoreOfSelectedCurriculumHomeworkAndTestByLearnerID,LearnerID,Score)  VALUES(" + scoreOfScoreOfSelectedCurriculumHomeworkAndTestByLearnerID + "," + learnerID + "," + score + ")"); //对应：curriculumSelectionDbContext.Database.ExecuteSqlRaw(          $"INSERT INTO dbo.ScoreOfSelectedCurriculumHomeworkAndTestByLearner(LearnerID,CurriculumHomeworkAndTestID,Score)  VALUES(1, 7050,100) "); ;
            }
            curriculumSelectionDbContext.SaveChanges();      
        }
    }
}