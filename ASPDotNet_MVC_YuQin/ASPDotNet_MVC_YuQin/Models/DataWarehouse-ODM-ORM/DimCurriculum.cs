using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurriculumSelection.Models
{
    public class DimCurriculum : Object
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 CurriculumID { get; set; }
        public String CurriculumName { get; set; }
        public Int32 FiveLayerMVCCategoryID { get; set; }
        public FiveLayerMVCCategory FiveLayerMVCCategory { get; set; }
        public ICollection<Educator> EducatorICollection { get; set; }
        public ICollection<MeasureScoreOfSelectedCurriculumByLearner> ScoreOfSelectedCurriculumByLearnerICollection { get; set; }
    }
}