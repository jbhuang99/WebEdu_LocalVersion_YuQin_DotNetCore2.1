using System;
using System.ComponentModel.DataAnnotations.Schema;
using CurriculumSelectionDW.Models;
using Microsoft.EntityFrameworkCore;

namespace CurriculumSelectionDW.Data
{
    public class CurriculumSelectionDWContext : DbContext
    {
        public CurriculumSelectionDWContext(DbContextOptions<CurriculumSelectionDWContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<DimCurriculum> DimCurriculumDbSet { get; set; }
        public DbSet<MeasureScoreOfSelectedCurriculumByLearner> MeasureScoreOfSelectedCurriculumByLearnerDbSet { get; set; }
        public DbSet<DimLearner> DimLearnerDbSet { get; set; }
        public DbSet<DimFiveLayerMVCCategory> DimFiveLayerMVCCategoryDbSet { get; set; }
        public DbSet<DimEducator> DimEducatorDbSet { get; set; }
        public DbSet<DimCurriculumHomeworkAndTest> DimCurriculumHomeworkAndTestDbSet { get; set; }
        public DbSet<DimCurriculumHomeworkAndTestSelectedTime> DimCurriculumHomeworkAndTestSelectedTimeDbSet { get; set; }

        public DbSet<DimOrganization> DimOrganizationDbSet { get; set; }
        public DbSet<MeasureScoreOfSelectedCurriculumHomeworkAndTestByLearner> MeasureScoreOfSelectedCurriculumHomeworkAndTestByLearnerDbSet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define primary keys for DW dimension and measure entities.
            // At minimum define a key for DimCurriculum to fix the exception.
            modelBuilder.Entity<DimCurriculum>().HasKey(dc => dc.CurriculumID);
            modelBuilder.Entity<MeasureScoreOfSelectedCurriculumByLearner>().HasKey(m => m.ScoreOfSelectedCurriculumByLearnerID);
            modelBuilder.Entity<DimLearner>().HasKey(d => d.LearnerID);
            modelBuilder.Entity<DimEducator>().HasKey(d => d.EducatorID);
            modelBuilder.Entity<DimFiveLayerMVCCategory>().HasKey(d => d.FiveLayerMVCCategoryID);
            modelBuilder.Entity<DimOrganization>().HasKey(d => d.OrganizationID);
            modelBuilder.Entity<DimCurriculumHomeworkAndTest>().HasKey(d => d.CurriculumHomeworkAndTestID);
            modelBuilder.Entity<DimCurriculumHomeworkAndTestSelectedTime>().HasKey(d => d.CurriculumHomeworkAndTestSelectedTimeID);
            modelBuilder.Entity<MeasureScoreOfSelectedCurriculumHomeworkAndTestByLearner>().HasKey(m => m.ScoreOfSelectedCurriculumHomeworkAndTestByLearnerID);
            modelBuilder.Entity<DimCurriculumSelectedTime>().HasKey(d => d.CurriculumSelectedTimeID);
            modelBuilder.Entity<DimLearnerSourcePlace>().HasKey(d => d.LearnerSourcePlaceID);

            modelBuilder.Entity<DimCurriculum>().ToTable("DimCurriculum"); //无法自动类型/线性二维数据表映射的，需要手动声明映射（又称为FluteAPI。FluteAPI将覆盖默认、数据注释特性,即，优先权最高）。
            modelBuilder.Entity<DimCurriculumSelectedTime>().ToTable("DimCurriculumSelectedTime");
            modelBuilder.Entity<MeasureScoreOfSelectedCurriculumByLearner>().ToTable("MeasureScoreOfSelectedCurriculumByLearner");
           // modelBuilder.Entity<ScoreOfSelectedCurriculumByLearner>().Property(delegate(ScoreOfSelectedCurriculumByLearnerID scoreOfSelectedCurriculumByLearnerID) {return scoreOfSelectedCurriculumByLearnerID; }).ScoreOfSelectedCurriculumByLearnerID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);//ASP.NET 5.0已经移除？
            modelBuilder.Entity<DimLearner>().ToTable("DimLearner");
            modelBuilder.Entity<DimLearnerSourcePlace>().ToTable("DimLearnerSourcePlace");
            modelBuilder.Entity<DimEducator>().ToTable("DimEducator");
            modelBuilder.Entity<DimFiveLayerMVCCategory>().ToTable("DimFiveLayerMVCCategory");
            modelBuilder.Entity<DimOrganization>().ToTable("DimOrganization");
            modelBuilder.Entity<DimCurriculumHomeworkAndTest>().ToTable("DimCurriculumHomeworkAndTest");
            modelBuilder.Entity<DimCurriculumHomeworkAndTestSelectedTime>().ToTable("DimCurriculumHomeworkAndTestSelectedTime");
            modelBuilder.Entity<MeasureScoreOfSelectedCurriculumHomeworkAndTestByLearner>().ToTable("MeasureScoreOfSelectedCurriculumHomeworkAndTestByLearner");
        }
    }
}