﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using CurriculumSelection.Models;
using Microsoft.EntityFrameworkCore;

namespace CurriculumSelection.Data
{
    public class CurriculumSelectionDbContext : DbContext
    {
        public CurriculumSelectionDbContext(DbContextOptions<CurriculumSelectionDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Curriculum> CurriculumDbSet { get; set; }
        public DbSet<ScoreOfSelectedCurriculumByLearner> ScoreOfSelectedCurriculumByLearnerDbSet { get; set; }
        public DbSet<Learner> LearnerDbSet { get; set; }
        public DbSet<CurriculumCategory> CurriculumCategoryDbSet { get; set; }
        public DbSet<Educator> EducatorDbSet { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Curriculum>().ToTable("Curriculum"); //无法自动类型/线性二维数据表映射的，需要手动声明映射（又称为FluteAPI。FluteAPI将覆盖默认、数据注释特性,即，优先权最高）。
            modelBuilder.Entity<ScoreOfSelectedCurriculumByLearner>().ToTable("ScoreOfSelectedCurriculumByLearner");
           // modelBuilder.Entity<ScoreOfSelectedCurriculumByLearner>().Property(delegate(ScoreOfSelectedCurriculumByLearnerID scoreOfSelectedCurriculumByLearnerID) {return scoreOfSelectedCurriculumByLearnerID; }).ScoreOfSelectedCurriculumByLearnerID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);//ASP.NET 5.0已经移除？
            modelBuilder.Entity<Learner>().ToTable("Learner");
            modelBuilder.Entity<Educator>().ToTable("Educator");
            modelBuilder.Entity<CurriculumCategory>().ToTable("CurriculumCategory");
        }
    }
}