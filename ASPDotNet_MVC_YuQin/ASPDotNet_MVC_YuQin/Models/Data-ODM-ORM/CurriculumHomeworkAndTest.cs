using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurriculumSelection.Models
{
    public class CurriculumHomeworkAndTest : Object
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 CurriculumHomeworkAndTestID { get; set; }
        public String CurriculumHomeworkAndTestName { get; set; }
        /**引起多级级联错误
        public Int32 CurriculumID { get; set; }
        public Curriculum Curriculum { get; set; }
        **/
        public Int32 FiveLayerMVCCategoryID { get; set; }
        public FiveLayerMVCCategory FiveLayerMVCCategory { get; set; }
        public ICollection<Educator> EducatorICollection { get; set; }
        public ICollection<ScoreOfSelectedCurriculumHomeworkAndTestByLearner> ScoreOfSelectedCurriculumHomeworkAndTestByLearnerICollection { get; set; }
    }
}