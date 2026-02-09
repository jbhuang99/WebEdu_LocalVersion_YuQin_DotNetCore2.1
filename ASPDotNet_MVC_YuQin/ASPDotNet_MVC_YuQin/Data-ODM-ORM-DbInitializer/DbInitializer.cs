/**using CurriculumSelection.Models;
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
                       $"INSERT INTO dbo.FiveLayerMVCCategory(FiveLayerMVCCategoryID,FiveLayerMVCCategoryName)  VALUES(1,'（数智思维核心要素的五层MVC的）实践（数据读写封装）') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.FiveLayerMVCCategory(FiveLayerMVCCategoryID,FiveLayerMVCCategoryName)  VALUES(2,'（数智思维核心要素的五层MVC的）技术（信息提取运用）') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.FiveLayerMVCCategory(FiveLayerMVCCategoryID,FiveLayerMVCCategoryName)  VALUES(3,'（数智思维核心要素的五层MVC的）科学（规律预测探究）') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.FiveLayerMVCCategory(FiveLayerMVCCategoryID,FiveLayerMVCCategoryName)  VALUES(4,'（数智思维核心要素的五层MVC的）人文（情感交流共鸣）') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.FiveLayerMVCCategory(FiveLayerMVCCategoryID,FiveLayerMVCCategoryName)  VALUES(5,'（数智思维核心要素的五层MVC的）哲学（智能建构生成）') ");
            curriculumSelectionDbContext.SaveChanges();

                     
            Int32 minValueCurriculumID = 1; Int32 maxValueCurriculumID = 801; Random randomCurriculumID = new Random(); Int32[] arrayNumCurriculumID = new Int32[maxValueCurriculumID - 1];
            for (Int32 i = 0; i < maxValueCurriculumID - 1; i++)
            {
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
                Int32 OrganizationID = GetNum(i, minValueOrganizationID, maxValueOrganizationID, randomOrganizationID, arrayNumOrganizationID);
                String OrganizationName = "（人类学校←隐喻→人形机器人训练厂商）品牌名称" + i.ToString() + "（待修改更真实仿制）";
                String OrganizationAddress ="地址" + i.ToString() + "（待修改更真实仿制）";
                curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                        $"INSERT INTO dbo.Organization(OrganizationID,OrganizationName,OrganizationAddress)  VALUES(" + OrganizationID + ",'" + OrganizationName + "','"+OrganizationAddress + "')"); //对应：curriculumSelectionDbContext.Database.ExecuteSqlRaw(                $"INSERT INTO dbo.Organization(OrganizationID,OrganizationName,OrganizationAddress)  VALUES(3300, '师范大学','江西') "); ;
            }           
            curriculumSelectionDbContext.SaveChanges();


            Int32 minValueLearnerID = 1; Int32 maxValueLearnerID = 801; Random randomLearnerID = new Random(); Int32[] arrayNumLearnerID = new Int32[maxValueLearnerID - 1];
            for (Int32 i = 0; i < maxValueLearnerID - 1; i++)
            {
                Int32 learnerID = GetNum(i, minValueLearnerID, maxValueLearnerID, randomLearnerID, arrayNumLearnerID);
                String learnerName = "（人类学习者←隐喻→人形机器人学习者）名称" + i.ToString() + "（待修改更真实仿制）";
                Int32 organizationID = randomLearnerID.Next(1,160);
                curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                        $"INSERT INTO dbo.Learner(LearnerID,Name,OrganizationID)  VALUES(" + learnerID + ",'" + learnerName + "'," + organizationID + ")"); //对应：curriculumSelectionDbContext.Database.ExecuteSqlRaw( $"INSERT INTO dbo.Learner(LearnerID,Name,OrganizationID)  VALUES(1,'学生1',3300) ");
            }        
            curriculumSelectionDbContext.SaveChanges();


            Int32 minValueEducatorID = 1; Int32 maxValueEducatorID = 201; Random randomEducatorID = new Random(); Int32[] arrayNumEducatorID = new Int32[maxValueEducatorID - 1];
            for (Int32 i = 0; i < maxValueLearnerID - 1; i++)
            {
                Int32 educatorID = GetNum(i, minValueEducatorID, maxValueEducatorID, randomEducatorID, arrayNumEducatorID);
                String educatorName = "（人类教育科研者←隐喻→人形机器人教育科研者）名称" + i.ToString() + "（待修改更真实仿制）";
                Int32 curriculumID = randomEducatorID.Next(1, 800);
                Int32 organizationID = randomEducatorID.Next(1, 160);
                curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                        $"INSERT INTO dbo.Educator(EducatorID,Name,OrganizationID,CurriculumID)  VALUES(" + educatorID + ",'" + educatorName + "'," + organizationID + "," + curriculumID + ")"); //对应：curriculumSelectionDbContext.Database.ExecuteSqlRaw($"INSERT INTO dbo.Educator(EducatorID,Name,CurriculumID,OrganizationID)  VALUES(1,'黄景碧',1045,3300) "); ;
            }        
            curriculumSelectionDbContext.SaveChanges();

            
            Int32 minValueScoreOfSelectedCurriculumByLearnerID = 1; Int32 maxValueScoreOfSelectedCurriculumByLearnerID = 6401; Random randomScoreOfSelectedCurriculumByLearnerID = new Random(); Int32[] arrayNumScoreOfSelectedCurriculumByLearnerID = new Int32[maxValueScoreOfSelectedCurriculumByLearnerID - 1];
            for (Int32 i = 0; i < maxValueScoreOfSelectedCurriculumByLearnerID - 1; i++)
            {
                Int32 scoreOfSelectedCurriculumByLearnerID = GetNum(i, minValueScoreOfSelectedCurriculumByLearnerID, maxValueScoreOfSelectedCurriculumByLearnerID, randomScoreOfSelectedCurriculumByLearnerID, arrayNumScoreOfSelectedCurriculumByLearnerID);
                String scoreOfSelectedCurriculumByLearnerNote = "（“人类学习者所选课程成绩”←隐喻→“人形机器人学习者所选课程成绩”）的备注" + i.ToString() + "（待修改更真实仿制）";
                Int32 curriculumID = randomScoreOfSelectedCurriculumByLearnerID.Next(1, 800);
                Int32 learnerID = randomScoreOfSelectedCurriculumByLearnerID.Next(1, 800);
                // Single score = (Single)randomScoreOfSelectedCurriculumByLearnerID.NextDouble();
               // Single score = 80.00F;
                 curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                        $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(ScoreOfSelectedCurriculumByLearnerID,ScoreOfSelectedCurriculumByLearnerNote,CurriculumID,LearnerID,Score)  VALUES(" + scoreOfSelectedCurriculumByLearnerID + ",'" + scoreOfSelectedCurriculumByLearnerNote + "'," + curriculumID + "," + learnerID + ","
                        //+ score
                        + ")"); //对应：curriculumSelectionDbContext.Database.ExecuteSqlRaw(                $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(LearnerID,CurriculumID,Score)  VALUES(1, 1050,85.0) ");;
            }            
            curriculumSelectionDbContext.SaveChanges();

            
            Int32 minValueScoreOfSelectedCurriculumHomeworkAndTestByLearnerID = 1; Int32 maxValueScoreOfSelectedCurriculumHomeworkAndTestByLearnerID = 8001; Random randomScoreOfSelectedCurriculumHomeworkAndTestByLearnerID = new Random(); Int32[] arrayNumScoreOfSelectedCurriculumHomeworkAndTestByLearnerID = new Int32[maxValueScoreOfSelectedCurriculumHomeworkAndTestByLearnerID - 1];
            for (Int32 i = 0; i < maxValueScoreOfSelectedCurriculumHomeworkAndTestByLearnerID - 1; i++)
            {
                Int32 scoreOfScoreOfSelectedCurriculumHomeworkAndTestByLearnerID = GetNum(i, minValueScoreOfSelectedCurriculumHomeworkAndTestByLearnerID, maxValueScoreOfSelectedCurriculumHomeworkAndTestByLearnerID, randomScoreOfSelectedCurriculumHomeworkAndTestByLearnerID, arrayNumScoreOfSelectedCurriculumHomeworkAndTestByLearnerID);
                String scoreOfSelectedCurriculumHomeworkAndTestByLearnerNote = "（“人类学习者所选作业测验单选题答题成绩”←隐喻→“人形机器人学习者所选作业测验单选题答题成绩”）的备注" + i.ToString() + "（待修改更真实仿制）";
                Int32 learnerID = randomScoreOfSelectedCurriculumByLearnerID.Next(1, 800);
                Single score = (Single)randomScoreOfSelectedCurriculumByLearnerID.NextDouble();
                curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                        $"INSERT INTO dbo.ScoreOfSelectedCurriculumHomeworkAndTestByLearner(ScoreOfSelectedCurriculumHomeworkAndTestByLearnerID,ScoreOfSelectedCurriculumHomeworkAndTestByLearnerNote,LearnerID,Score)  VALUES(" + scoreOfScoreOfSelectedCurriculumHomeworkAndTestByLearnerID+ ",'" + scoreOfSelectedCurriculumHomeworkAndTestByLearnerNote + "'," +  learnerID + "," + score + ")"); //对应：curriculumSelectionDbContext.Database.ExecuteSqlRaw(          $"INSERT INTO dbo.ScoreOfSelectedCurriculumHomeworkAndTestByLearner(LearnerID,CurriculumHomeworkAndTestID,Score)  VALUES(1, 7050,100) "); ;
            }
            curriculumSelectionDbContext.SaveChanges();      
        }
    }
}
**/
using CurriculumSelection.Models;
using Microsoft.EntityFrameworkCore;
using OpenXmlPowerTools;
using System;
using System.Linq;
using System.Collections.Generic;

namespace CurriculumSelection.Data
{
    public static class DbInitializer
    {
        // Generate `count` unique IDs in range [minInclusive, maxInclusive] using a partial Fisher‑Yates shuffle.
        private static int[] GenerateUniqueIds(int count, int minInclusive, int maxInclusive, Random rng)
        {
            int range = maxInclusive - minInclusive + 1;
            if (count > range) throw new ArgumentException($"Requested {count} unique IDs but range size is {range}.");

            int[] pool = Enumerable.Range(minInclusive, range).ToArray();

            // Partial Fisher-Yates: shuffle only first `count` positions
            for (int i = 0; i < count; i++)
            {
                int j = rng.Next(i, pool.Length);
                int tmp = pool[i];
                pool[i] = pool[j];
                pool[j] = tmp;
            }

            int[] result = new int[count];
            Array.Copy(pool, 0, result, 0, count);
            return result;
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
                       $"INSERT INTO dbo.FiveLayerMVCCategory(FiveLayerMVCCategoryID,FiveLayerMVCCategoryName)  VALUES(1,'（数智思维核心要素的五层MVC的）实践（数据读写封装）') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.FiveLayerMVCCategory(FiveLayerMVCCategoryID,FiveLayerMVCCategoryName)  VALUES(2,'（数智思维核心要素的五层MVC的）技术（信息提取运用）') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.FiveLayerMVCCategory(FiveLayerMVCCategoryID,FiveLayerMVCCategoryName)  VALUES(3,'（数智思维核心要素的五层MVC的）科学（规律预测探究）') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.FiveLayerMVCCategory(FiveLayerMVCCategoryID,FiveLayerMVCCategoryName)  VALUES(4,'（数智思维核心要素的五层MVC的）人文（情感交流共鸣）') ");
            curriculumSelectionDbContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.FiveLayerMVCCategory(FiveLayerMVCCategoryID,FiveLayerMVCCategoryName)  VALUES(5,'（数智思维核心要素的五层MVC的）哲学（智能建构生成）') ");
            curriculumSelectionDbContext.SaveChanges();

            // Reuse single RNG instance
            Random rng = new Random();

            // Years: generate 25 unique IDs in [1,15]
            Int32 yearCount = 15;
            Int32[] yearIds = GenerateUniqueIds(yearCount, 1, 15, rng);
            for (Int32 i = 0; i < yearCount; i++)
            {
                Int32 yearId = yearIds[i];//Int32 yearID = yearIds[i];竟然出错
                Int32 yearName = rng.Next(2010, 2025);
                curriculumSelectionDbContext.Database.ExecuteSqlInterpolated(
                    $"INSERT INTO dbo.Year(YearID,YearName) VALUES({yearId},{yearName})");
            }
            curriculumSelectionDbContext.SaveChanges();

            // YearFirstHierarchyMonths: generate 12 unique IDs in [1,12]
            Int32 yearFirstHierarchyMonthCount = 12;
            Int32[] yearFirstHierarchyMonthIds = GenerateUniqueIds(yearFirstHierarchyMonthCount, 1, 12, rng);
            Int32 yearID = rng.Next(1, 13);
            for (Int32 i = 0; i < yearFirstHierarchyMonthCount; i++)
            {
                Int32 yearFirstHierarchyMonthId = yearFirstHierarchyMonthIds[i];
                Int32 yearFirstHierarchyMonthName = rng.Next(1, 13);
                curriculumSelectionDbContext.Database.ExecuteSqlInterpolated(
                    $"INSERT INTO dbo.YearFirstHierarchyMonth(YearFirstHierarchyMonthID,YearFirstHierarchyMonthName,YearID) VALUES({yearFirstHierarchyMonthId},{yearFirstHierarchyMonthName},{yearID})");
            }
            curriculumSelectionDbContext.SaveChanges();

            // Countrys: generate 197 unique IDs in [1,197]
            Int32 countryCount = 197;
            Int32[] countryIds = GenerateUniqueIds(countryCount, 1, 197, rng);
            for (Int32 i = 0; i < countryCount; i++)
            {
                Int32 countryId = countryIds[i];
                String countryName = "国家名称" + i.ToString() + "（待修改更真实仿制）" ;
                curriculumSelectionDbContext.Database.ExecuteSqlInterpolated(
                    $"INSERT INTO dbo.Country(CountryID,CountryName) VALUES({countryId},{countryName})");
            }
            curriculumSelectionDbContext.SaveChanges();

            // CountryFirstHierarchy: generate 100 unique IDs in [1,100]
            Int32 countryFirstHierarchyCount = 100;
            Int32 countryID = rng.Next(1, 198);
            Int32[] countryFirstHierarchyIds = GenerateUniqueIds(countryFirstHierarchyCount, 1, 100, rng);
            for (Int32 i = 0; i < countryFirstHierarchyCount; i++)
            {
                Int32 countryFirstHierarchyId = countryFirstHierarchyIds[i];
                String countryFirstHierarchyName = "国家下属第一层次名称" + i.ToString() + "（待修改更真实仿制）";
                Int32 yearFirstHierarchyMonthName = rng.Next(1, 13);
                curriculumSelectionDbContext.Database.ExecuteSqlInterpolated(
                    $"INSERT INTO dbo.CountryFirstHierarchy(CountryFirstHierarchyID,CountryFirstHierarchyName,CountryID) VALUES({countryFirstHierarchyId},{countryFirstHierarchyName},{countryID})");
            }
            curriculumSelectionDbContext.SaveChanges();

            // CountrySecondHierarchy: generate 150 unique IDs in [1,150]
            Int32 countrySecondHierarchyCount = 150;
            Int32 countryFirstHierarchyID = rng.Next(1, 101);
            Int32[] countrySecondHierarchyIds = GenerateUniqueIds(countrySecondHierarchyCount, 1, 150, rng);
            for (Int32 i = 0; i < countrySecondHierarchyCount; i++)
            {
                Int32 countrySecondHierarchyId = countrySecondHierarchyIds[i];
                String countrySecondHierarchyName = "国家下属第二层次名称" + i.ToString() + "（待修改更真实仿制）"; ;
                curriculumSelectionDbContext.Database.ExecuteSqlInterpolated(
                    $"INSERT INTO dbo.CountrySecondHierarchy(CountrySecondHierarchyID,CountrySecondHierarchyName,CountryFirstHierarchyID) VALUES({countrySecondHierarchyId},{countrySecondHierarchyName},{countryFirstHierarchyID})");
            }
            curriculumSelectionDbContext.SaveChanges();

            // Curricula: generate 800 unique IDs in [1,800]
            int curriculumCount = 800;
            int[] curriculumIds = GenerateUniqueIds(curriculumCount, 1, 800, rng);
            for (int i = 0; i < curriculumCount; i++)
            {
                int curriculumID = curriculumIds[i];
                string curriculumName = "课程名称" + i.ToString() + "（待修改更真实仿制）";
                int fiveLayerMVCCategoryID = rng.Next(1, 6);
                curriculumSelectionDbContext.Database.ExecuteSqlInterpolated(
                    $"INSERT INTO dbo.Curriculum(CurriculumID,CurriculumName,FiveLayerMVCCategoryID) VALUES({curriculumID}, {curriculumName}, {fiveLayerMVCCategoryID})");
            }
            curriculumSelectionDbContext.SaveChanges();

            // CurriculumHomeworkAndTest: generate 8000 unique IDs in [1,8000]
            int chtCount = 8000;
            int[] chtIds = GenerateUniqueIds(chtCount, 1, 8000, rng);
            for (int i = 0; i < chtCount; i++)
            {
                int id = chtIds[i];
                string name = "课程作业测验名称" + i.ToString() + "（待修改更真实仿制）";
                int cat = rng.Next(1, 6);
                curriculumSelectionDbContext.Database.ExecuteSqlInterpolated(
                    $"INSERT INTO dbo.CurriculumHomeworkAndTest(CurriculumHomeworkAndTestID,CurriculumHomeworkAndTestName,FiveLayerMVCCategoryID) VALUES({id}, {name}, {cat})");
                if (i % 200 == 0) curriculumSelectionDbContext.SaveChanges(); // batch to avoid huge transaction
            }
            curriculumSelectionDbContext.SaveChanges();

            // Organization: generate 160 unique IDs in [1,160]
            int orgCount = 160;
            int[] orgIds = GenerateUniqueIds(orgCount, 1, 160, rng);
            for (int i = 0; i < orgCount; i++)
            {
                int id = orgIds[i];
                string name = "（人类学校←隐喻→人形机器人训练厂商）品牌名称" + i.ToString() + "（待修改更真实仿制）";
                string addr = "地址" + i.ToString() + "（待修改更真实仿制）";
                curriculumSelectionDbContext.Database.ExecuteSqlInterpolated(
                    $"INSERT INTO dbo.Organization(OrganizationID,OrganizationName,OrganizationAddress) VALUES({id}, {name}, {addr})");
            }
            curriculumSelectionDbContext.SaveChanges();

            // Learner: generate 800 unique IDs in [1,800]
            int learnerCount = 800;
            int[] learnerIds = GenerateUniqueIds(learnerCount, 1, 800, rng);
            for (int i = 0; i < learnerCount; i++)
            {
                int id = learnerIds[i];
                string name = "（人类学习者←隐喻→人形机器人学习者）名称" + i.ToString() + "（待修改更真实仿制）";
                int organizationID = rng.Next(1, Math.Min(160, orgCount) + 1);
                curriculumSelectionDbContext.Database.ExecuteSqlInterpolated(
                    $"INSERT INTO dbo.Learner(LearnerID,Name,OrganizationID) VALUES({id}, {name}, {organizationID})");
            }
            curriculumSelectionDbContext.SaveChanges();

            // Educator: generate 200 unique IDs in [1,200]
            int educatorCount = 200;
            int[] educatorIds = GenerateUniqueIds(educatorCount, 1, 200, rng);
            for (int i = 0; i < educatorCount; i++)
            {
                int id = educatorIds[i];
                string name = "（人类教育科研者←隐喻→人形机器人教育科研者）名称" + i.ToString() + "（待修改更真实仿制）";
                int curriculumID = rng.Next(1, curriculumCount + 1);
                int organizationID = rng.Next(1, Math.Min(160, orgCount) + 1);
                curriculumSelectionDbContext.Database.ExecuteSqlInterpolated(
                    $"INSERT INTO dbo.Educator(EducatorID,Name,OrganizationID,CurriculumID) VALUES({id}, {name}, {organizationID}, {curriculumID})");
            }
            curriculumSelectionDbContext.SaveChanges();

            // ScoreOfSelectedCurriculumByLearner: generate 64000 unique IDs in [1,64000]
            int scoreCount = 64000;
            int[] scoreIds = GenerateUniqueIds(scoreCount, 1, 64000, rng);
            for (int i = 0; i < scoreCount; i++)
            {
                int id = scoreIds[i];
                string note = "（“人类学习者所选课程成绩”←隐喻→“人形机器人学习者所选课程成绩”）的备注" + i.ToString() + "（待修改更真实仿制）";
                int curriculumID = rng.Next(1, curriculumCount + 1);
                int learnerID = rng.Next(1, learnerCount + 1);
                double score = Math.Round(rng.NextDouble() * 100.0, 2);
                /**Original failing INSERT that included the ID:
                curriculumSelectionDbContext.Database.ExecuteSqlInterpolated(
                    $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(ScoreOfSelectedCurriculumByLearnerID,ScoreOfSelectedCurriculumByLearnerNote,CurriculumID,LearnerID,Score) VALUES({id}, {note}, {curriculumID}, {learnerID}, {score})");
                **/
                // Replace the failing INSERT that included the ID with one that omits it:
                curriculumSelectionDbContext.Database.ExecuteSqlInterpolated(
                    $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(ScoreOfSelectedCurriculumByLearnerNote, CurriculumID, LearnerID, Score) VALUES({note}, {curriculumID}, {learnerID}, {score})");
                if (i % 500 == 0) curriculumSelectionDbContext.SaveChanges();
            }
            curriculumSelectionDbContext.SaveChanges();
            //

            //

            // ScoreOfSelectedCurriculumHomeworkAndTestByLearner: generate 80000 unique IDs in [1,80000]
            int scoreChtCount = 80000;
            int[] scoreChtIds = GenerateUniqueIds(scoreChtCount, 1, 80000, rng);
            for (int i = 0; i < scoreChtCount; i++)
            {
                int id = scoreChtIds[i];
                string note = "（“人类学习者所选作业测验单选题答题成绩”←隐喻→“人形机器人学习者所选作业测验单选题答题成绩”）的备注" + i.ToString() + "（待修改更真实仿制）";
                int curriculumHomeworkAndTestID = rng.Next(1, chtCount + 1);
                int learnerID = rng.Next(1, learnerCount + 1);
                //double score = Math.Round(rng.NextDouble() * 100.0, 2);
                int score= rng.Next(0,2);
                /**Original failing INSERT that included the ID:
               curriculumSelectionDbContext.Database.ExecuteSqlInterpolated(
                   $"INSERT INTO dbo.ScoreOfSelectedCurriculumHomeworkAndTestByLearner(ScoreOfSelectedCurriculumHomeworkAndTestByLearnerID,ScoreOfSelectedCurriculumHomeworkAndTestByLearnerNote,LearnerID,Score) VALUES({id}, {note}, {learnerID}, {score})");
                **/
                // Replace the failing INSERT that included the ID with one that omits it:
                curriculumSelectionDbContext.Database.ExecuteSqlInterpolated(
                    $"INSERT INTO dbo.ScoreOfSelectedCurriculumHomeworkAndTestByLearner(ScoreOfSelectedCurriculumHomeworkAndTestByLearnerNote,CurriculumHomeworkAndTestID,LearnerID,Score) VALUES({note}, {curriculumHomeworkAndTestID}, {learnerID}, {score})");
                if (i % 500 == 0) curriculumSelectionDbContext.SaveChanges();
            }
            curriculumSelectionDbContext.SaveChanges();
        }
    }
}