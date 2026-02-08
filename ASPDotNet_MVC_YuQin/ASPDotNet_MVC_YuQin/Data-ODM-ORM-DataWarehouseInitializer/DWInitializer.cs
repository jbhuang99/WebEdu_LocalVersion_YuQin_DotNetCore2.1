using CurriculumSelection.Models;
using Microsoft.EntityFrameworkCore;
using OpenXmlPowerTools;
using System;
using System.Linq;
using System.Collections.Generic;

namespace CurriculumSelectionDW.Data
{
    public static class DWInitializer
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

        public static void Initialize(CurriculumSelectionDWContext curriculumSelectionDWContext)
        {
            curriculumSelectionDWContext.Database.EnsureCreated();

            // Look for any students.
            if (curriculumSelectionDWContext.DimLearnerDbSet.Any())
            {
                return;   // DB has been seeded
            }

            curriculumSelectionDWContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.DimFiveLayerMVCCategory(FiveLayerMVCCategoryID,FiveLayerMVCCategoryName)  VALUES(1,'（数智思维核心要素的五层MVC的）实践（数据读写封装）') ");
            curriculumSelectionDWContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.DimFiveLayerMVCCategory(FiveLayerMVCCategoryID,FiveLayerMVCCategoryName)  VALUES(2,'（数智思维核心要素的五层MVC的）技术（信息提取运用）') ");
            curriculumSelectionDWContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.DimFiveLayerMVCCategory(FiveLayerMVCCategoryID,FiveLayerMVCCategoryName)  VALUES(3,'（数智思维核心要素的五层MVC的）科学（规律预测探究）') ");
            curriculumSelectionDWContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.DimFiveLayerMVCCategory(FiveLayerMVCCategoryID,FiveLayerMVCCategoryName)  VALUES(4,'（数智思维核心要素的五层MVC的）人文（情感交流共鸣）') ");
            curriculumSelectionDWContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.DimFiveLayerMVCCategory(FiveLayerMVCCategoryID,FiveLayerMVCCategoryName)  VALUES(5,'（数智思维核心要素的五层MVC的）哲学（智能建构生成）') ");
            curriculumSelectionDWContext.SaveChanges();

            // Reuse single RNG instance
            Random rng = new Random();

            // Curricula: generate 800 unique IDs in [1,800]
            int curriculumCount = 800;
            int[] curriculumIds = GenerateUniqueIds(curriculumCount, 1, 800, rng);
            for (int i = 0; i < curriculumCount; i++)
            {
                int curriculumID = curriculumIds[i];
                string curriculumName = "课程名称" + i.ToString() + "（待修改更真实仿制）";
                int fiveLayerMVCCategoryID = rng.Next(1, 6);
                curriculumSelectionDWContext.Database.ExecuteSqlInterpolated(
                    $"INSERT INTO dbo.DimCurriculum(CurriculumID,CurriculumName,FiveLayerMVCCategoryID) VALUES({curriculumID}, {curriculumName}, {fiveLayerMVCCategoryID})");
            }
            curriculumSelectionDWContext.SaveChanges();

            // CurriculumHomeworkAndTest: generate 8000 unique IDs in [1,8000]
            int chtCount = 8000;
            int[] chtIds = GenerateUniqueIds(chtCount, 1, 8000, rng);
            for (int i = 0; i < chtCount; i++)
            {
                int id = chtIds[i];
                string name = "课程作业测验名称" + i.ToString() + "（待修改更真实仿制）";
                int cat = rng.Next(1, 6);
                curriculumSelectionDWContext.Database.ExecuteSqlInterpolated(
                    $"INSERT INTO dbo.DimCurriculumHomeworkAndTest(CurriculumHomeworkAndTestID,CurriculumHomeworkAndTestName,FiveLayerMVCCategoryID) VALUES({id}, {name}, {cat})");
                if (i % 200 == 0) curriculumSelectionDWContext.SaveChanges(); // batch to avoid huge transaction
            }
            curriculumSelectionDWContext.SaveChanges();

            // Organization: generate 160 unique IDs in [1,160]
            int orgCount = 160;
            int[] orgIds = GenerateUniqueIds(orgCount, 1, 160, rng);
            for (int i = 0; i < orgCount; i++)
            {
                int id = orgIds[i];
                string name = "（人类学校←隐喻→人形机器人训练厂商）品牌名称" + i.ToString() + "（待修改更真实仿制）";
                string addr = "地址" + i.ToString() + "（待修改更真实仿制）";
                curriculumSelectionDWContext.Database.ExecuteSqlInterpolated(
                    $"INSERT INTO dbo.DimOrganization(OrganizationID,OrganizationName,OrganizationAddress) VALUES({id}, {name}, {addr})");
            }
            curriculumSelectionDWContext.SaveChanges();

            // Learner: generate 800 unique IDs in [1,800]
            int learnerCount = 800;
            int[] learnerIds = GenerateUniqueIds(learnerCount, 1, 800, rng);
            for (int i = 0; i < learnerCount; i++)
            {
                int id = learnerIds[i];
                string name = "（人类学习者←隐喻→人形机器人学习者）名称" + i.ToString() + "（待修改更真实仿制）";
                int organizationID = rng.Next(1, Math.Min(160, orgCount) + 1);
                curriculumSelectionDWContext.Database.ExecuteSqlInterpolated(
                    $"INSERT INTO dbo.DimLearner(LearnerID,Name,OrganizationID) VALUES({id}, {name}, {organizationID})");
            }
            curriculumSelectionDWContext.SaveChanges();

            // DimLearnerSourcePlace: generate 800 unique IDs in [1,800]
            int dimLearnerSourcePlaceCount = 800;
            int[] dimLearnerSourcePlaceIds = GenerateUniqueIds(dimLearnerSourcePlaceCount, 1, 800, rng);
            for (int i = 0; i < dimLearnerSourcePlaceCount; i++)
            {
                int id = dimLearnerSourcePlaceIds[i];
                string provice = "省份名称" + i.ToString() + "（待修改更真实仿制）";
                string city = "城市名称" + i.ToString() + "（待修改更真实仿制）"; ;
                curriculumSelectionDWContext.Database.ExecuteSqlInterpolated(
                    $"INSERT INTO dbo.DimLearnerSourcePlace(LearnerSourcePlaceID,Province,City) VALUES({id}, {provice}, {city})");
            }
            curriculumSelectionDWContext.SaveChanges();

            // Educator: generate 200 unique IDs in [1,200]
            int educatorCount = 200;
            int[] educatorIds = GenerateUniqueIds(educatorCount, 1, 200, rng);
            for (int i = 0; i < educatorCount; i++)
            {
                int id = educatorIds[i];
                string name = "（人类教育科研者←隐喻→人形机器人教育科研者）名称" + i.ToString() + "（待修改更真实仿制）";
                int curriculumID = rng.Next(1, curriculumCount + 1);
                int organizationID = rng.Next(1, Math.Min(160, orgCount) + 1);
                curriculumSelectionDWContext.Database.ExecuteSqlInterpolated(
                    $"INSERT INTO dbo.DimEducator(EducatorID,Name,OrganizationID,CurriculumID) VALUES({id}, {name}, {organizationID}, {curriculumID})");
            }
            curriculumSelectionDWContext.SaveChanges();

            // DimCurriculumSelectedTime: generate 64000 unique IDs in [1,64000]
            int dimCurriculumSelectedTimeCount = 64000;
            int[] dimCurriculumSelectedTimeIds = GenerateUniqueIds(dimCurriculumSelectedTimeCount, 1, 64000, rng);
            // curriculumSelectionDWContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.DimCurriculumSelectedTime ON;");//没起作用，只好放弃ID列的插入了，改为让数据库自动生成ID，也能保证1-64000？
            for (int i = 0; i < dimCurriculumSelectedTimeCount; i++)
            {
                int id = dimCurriculumSelectedTimeIds[i];
                int calendarYear = rng.Next(2010, 2025);
                int calendarSemester = rng.Next(1, 3);
                /**
                curriculumSelectionDWContext.Database.ExecuteSqlInterpolated(
                    $"INSERT INTO dbo.DimCurriculumSelectedTime(CurriculumSelectedTimeID,CalendarYear,CalendarSemester) VALUES({id}, {calendarYear}, {calendarSemester})");
                **/
                curriculumSelectionDWContext.Database.ExecuteSqlInterpolated(
                    $"INSERT INTO dbo.DimCurriculumSelectedTime(CalendarYear,CalendarSemester) VALUES({calendarYear}, {calendarSemester})");
                if (i % 500 == 0) curriculumSelectionDWContext.SaveChanges();
            }
            //curriculumSelectionDWContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.DimCurriculumSelectedTime OFF;");
            curriculumSelectionDWContext.SaveChanges();

            // DimCurriculumHomeworkAndTestSelectedTime: generate 80000 unique IDs in [1,80000]
            int dimCurriculumHomeworkAndTestSelectedTimeCount = 80000;
            int[] dimCurriculumHomeworkAndTestSelectedTimeIds = GenerateUniqueIds(dimCurriculumHomeworkAndTestSelectedTimeCount, 1, 80000, rng);
            for (int i = 0; i < dimCurriculumHomeworkAndTestSelectedTimeCount; i++)
            {
                int id = dimCurriculumHomeworkAndTestSelectedTimeIds[i];
                int calendarYear = rng.Next(2010, 2025);
                int calendarSemester = rng.Next(1, 3);
                curriculumSelectionDWContext.Database.ExecuteSqlInterpolated(
                    $"INSERT INTO dbo.DimCurriculumHomeworkAndTestSelectedTime(CalendarYear,CalendarSemester) VALUES({calendarYear}, {calendarSemester})");
                if (i % 500 == 0) curriculumSelectionDWContext.SaveChanges();
            }
            curriculumSelectionDWContext.SaveChanges();

            // ScoreOfSelectedCurriculumByLearner: generate 64000 unique IDs in [1,64000]
            int scoreCount = 64000;
            int[] scoreIds = GenerateUniqueIds(scoreCount, 1, 64000, rng);
            for (int i = 0; i < scoreCount; i++)
            {
                int id = scoreIds[i];
               // string note = "（“人类学习者所选课程成绩”←隐喻→“人形机器人学习者所选课程成绩”）的备注" + i.ToString() + "（待修改更真实仿制）";
                int curriculumID = rng.Next(1, curriculumCount + 1);
                int learnerID = rng.Next(1, learnerCount + 1);
                Int32 curriculumSelectedTimeID = rng.Next(1, dimCurriculumSelectedTimeCount + 1);
                double score = Math.Round(rng.NextDouble() * 100.0, 2);
                /**Original failing INSERT that included the ID:
                curriculumSelectionDbContext.Database.ExecuteSqlInterpolated(
                    $"INSERT INTO dbo.ScoreOfSelectedCurriculumByLearner(ScoreOfSelectedCurriculumByLearnerID,ScoreOfSelectedCurriculumByLearnerNote,CurriculumID,LearnerID,Score) VALUES({id}, {note}, {curriculumID}, {learnerID}, {score})");
                **/
                // Replace the failing INSERT that included the ID with one that omits it:
                curriculumSelectionDWContext.Database.ExecuteSqlInterpolated(
                    $"INSERT INTO dbo.MeasureScoreOfSelectedCurriculumByLearner(CurriculumID, LearnerID,CurriculumSelectedTimeID,Score) VALUES({curriculumID}, {learnerID}, {curriculumSelectedTimeID}, {score})");
                if (i % 500 == 0) curriculumSelectionDWContext.SaveChanges();
            }
            curriculumSelectionDWContext.SaveChanges();
            //

            //

            // ScoreOfSelectedCurriculumHomeworkAndTestByLearner: generate 80000 unique IDs in [1,80000]
            int scoreChtCount = 80000;
            int[] scoreChtIds = GenerateUniqueIds(scoreChtCount, 1, 80000, rng);
            for (int i = 0; i < scoreChtCount; i++)
            {
                int id = scoreChtIds[i];
               // string note = "（“人类学习者所选作业测验单选题答题成绩”←隐喻→“人形机器人学习者所选作业测验单选题答题成绩”）的备注" + i.ToString() + "（待修改更真实仿制）";
                int curriculumHomeworkAndTestID = rng.Next(1, chtCount + 1);
                int learnerID = rng.Next(1, learnerCount + 1);
                Int32 curriculumHomeworkAndTestSelectedTimeID = rng.Next(1, dimCurriculumHomeworkAndTestSelectedTimeCount + 1);
                //double score = Math.Round(rng.NextDouble() * 100.0, 2);
                int score= rng.Next(0,2);
                /**Original failing INSERT that included the ID:
               curriculumSelectionDbContext.Database.ExecuteSqlInterpolated(
                   $"INSERT INTO dbo.ScoreOfSelectedCurriculumHomeworkAndTestByLearner(ScoreOfSelectedCurriculumHomeworkAndTestByLearnerID,ScoreOfSelectedCurriculumHomeworkAndTestByLearnerNote,LearnerID,Score) VALUES({id}, {note}, {learnerID}, {score})");
                **/
                // Replace the failing INSERT that included the ID with one that omits it:
                curriculumSelectionDWContext.Database.ExecuteSqlInterpolated(
                    $"INSERT INTO dbo.MeasureScoreOfSelectedCurriculumHomeworkAndTestByLearner(CurriculumHomeworkAndTestID,LearnerID,CurriculumHomeworkAndTestSelectedTimeID,Score) VALUES({curriculumHomeworkAndTestID}, {learnerID}, {curriculumHomeworkAndTestSelectedTimeID}, {score})");
                if (i % 500 == 0) curriculumSelectionDWContext.SaveChanges();
            }
            curriculumSelectionDWContext.SaveChanges();
        }
    }
}