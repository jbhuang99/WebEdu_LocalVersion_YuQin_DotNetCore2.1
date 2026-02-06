using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurriculumSelectionDW.Models
{
    public class DimCurriculumHomeworkAndTest : Object
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 CurriculumHomeworkAndTestID { get; set; }
        public String CurriculumHomeworkAndTestName { get; set; }
        /**引起多级级联错误
        public Int32 CurriculumID { get; set; }
        public Curriculum Curriculum { get; set; }
        **/
        public Int32 FiveLayerMVCCategoryID { get; set; }
        public DimFiveLayerMVCCategory DimFiveLayerMVCCategory { get; set; }
        public ICollection<DimEducator> DimEducatorICollection { get; set; }
        public ICollection<MeasureScoreOfSelectedCurriculumHomeworkAndTestByLearner> MeasureScoreOfSelectedCurriculumHomeworkAndTestByLearnerICollection { get; set; }
    }
}