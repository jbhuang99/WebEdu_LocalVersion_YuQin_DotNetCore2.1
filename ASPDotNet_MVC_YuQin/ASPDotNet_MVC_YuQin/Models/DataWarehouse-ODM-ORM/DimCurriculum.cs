using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurriculumSelectionDW.Models
{
    public class DimCurriculum : Object
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 CurriculumID { get; set; }
        public String CurriculumName { get; set; }
        public Int32 FiveLayerMVCCategoryID { get; set; }
        public DimFiveLayerMVCCategory DimFiveLayerMVCCategory { get; set; }
        public ICollection<DimEducator> DimEducatorICollection { get; set; }
        public ICollection<MeasureScoreOfSelectedCurriculumByLearner> MeasureScoreOfSelectedCurriculumByLearnerICollection { get; set; }
    }
}