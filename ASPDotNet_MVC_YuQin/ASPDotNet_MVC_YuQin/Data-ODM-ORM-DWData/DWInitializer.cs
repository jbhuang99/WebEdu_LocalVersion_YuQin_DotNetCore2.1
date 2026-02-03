using CurriculumSelection.Models;
using Microsoft.EntityFrameworkCore;
using OpenXmlPowerTools;
using System;
using System.Linq;
using System.Collections.Generic;

namespace CurriculumSelection.Data
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

            // ScoreOfSelectedCurriculumByLearner: generate 6400 unique IDs in [1,6400]
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

            // ScoreOfSelectedCurriculumHomeworkAndTestByLearner: generate 8000 unique IDs in [1,8000]
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