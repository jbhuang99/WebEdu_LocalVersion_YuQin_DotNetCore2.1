using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurriculumSelectionDW.Models
{
    public class DimLearnerSourcePlace : Object
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 LearnerSourcePlaceID { get; set; }//并非真正需求Int32,只是为了便于映射VS数据库
        public String? City { get; set; } 
        public String? Province { get; set; }   
    }
}
