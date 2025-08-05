using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurriculumSelection.Models
{
    public class Curriculum : Object
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 CurriculumID { get; set; }
        public String CurriculumName { get; set; }
        public Int32 CurriculumCategoryID { get; set; }
        public CurriculumCategory CurriculumCategory { get; set; }
        public ICollection<Educator> EducatorICollection { get; set; }
        public ICollection<ScoreOfSelectedCurriculumByLearner> ScoreOfSelectedCurriculumByLearnerICollection { get; set; }
    }
}