using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurriculumSelectionDW.Models
{
    public class DimFiveLayerMVCCategory : Object
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 FiveLayerMVCCategoryID { get; set; }
        public String FiveLayerMVCCategoryName { get; set; }
        public ICollection<DimCurriculum> DimCurriculumICollection { get; set; }
    }
}