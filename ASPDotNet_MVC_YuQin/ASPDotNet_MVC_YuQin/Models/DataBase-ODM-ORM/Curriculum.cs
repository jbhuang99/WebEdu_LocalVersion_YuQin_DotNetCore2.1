using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurriculumSelection.DB.Models
{
    public class Curriculum : Object
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 CurriculumID { get; set; }
        public String CurriculumName { get; set; }
        public Int32 FiveLayerMVCCategoryID { get; set; }
        public FiveLayerMVCCategory FiveLayerMVCCategory { get; set; }
        public ICollection<Educator> EducatorICollection { get; set; }
        public ICollection<ScoreOfSelectedCurriculumByLearner> ScoreOfSelectedCurriculumByLearnerICollection { get; set; }
    }
}