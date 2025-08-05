using System;
using System.ComponentModel.DataAnnotations.Schema;
using CurriculumSelectionWithInheritance.Models;
using Microsoft.EntityFrameworkCore;

namespace CurriculumSelectionWithInheritance.Data
{
    public class CurriculumSelectionWithInheritanceDbContext : DbContext
    {
        public CurriculumSelectionWithInheritanceDbContext(DbContextOptions<CurriculumSelectionWithInheritanceDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<CurriculumCategory> CurriculumCategoryDbSet { get; set; }
        public DbSet<Curriculum> CurriculumDbSet { get; set; }
        public DbSet<Person> PersonDbSet { get; set; }
        public DbSet<Learner> LearnerDbSet { get; set; }
        public DbSet<Educator> EducatorDbSet { get; set; }
        public DbSet<ScoreOfSelectedCurriculumByLearner> ScoreOfSelectedCurriculumByLearnerDbSet { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurriculumCategory>().ToTable("CurriculumCategory");
            modelBuilder.Entity<Curriculum>().ToTable("Curriculum"); //无法自动类型/线性二维数据表映射的，需要手动声明映射（又成为Fluent API。Fluent API将覆盖默认、数据注释特性,即，优先权最高）。
            modelBuilder.Entity<Person>().UseTptMappingStrategy();//难道EF版本需要6.0以上？
           // modelBuilder.Entity<Person>().UseTphMappingStrategy();//难道EF版本需要6.0以上？
           // modelBuilder.Entity<Person>().UseTpcMappingStrategy();//难道EF版本需要6.0以上？
           // modelBuilder.Entity<Person>().HasNoKey();
            modelBuilder.Entity<Person>().ToTable("Person");           
            modelBuilder.Entity<Learner>().ToTable("Learner");
            //modelBuilder.Entity<Learner>().HasKey((Learner learner)=>learner.LearnerID);
            //modelBuilder.Entity<Learner>().HasAlternateKey((Learner learner) => learner.LearnerID);
            modelBuilder.Entity<Educator>().ToTable("Educator");
           // modelBuilder.Entity<Educator>().HasKey((Educator educator) => educator.EducatorID);
          //  modelBuilder.Entity<Educator>().HasAlternateKey((Educator educator) => educator.EducatorID);
            modelBuilder.Entity<ScoreOfSelectedCurriculumByLearner>().ToTable("ScoreOfSelectedCurriculumByLearner");
            // modelBuilder.Entity<ScoreOfSelectedCurriculumByLearner>().Property(delegate(ScoreOfSelectedCurriculumByLearnerID scoreOfSelectedCurriculumByLearnerID) {return scoreOfSelectedCurriculumByLearnerID; }).ScoreOfSelectedCurriculumByLearnerID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);//ASP.NET 5.0已经移除？
        }
    }
}