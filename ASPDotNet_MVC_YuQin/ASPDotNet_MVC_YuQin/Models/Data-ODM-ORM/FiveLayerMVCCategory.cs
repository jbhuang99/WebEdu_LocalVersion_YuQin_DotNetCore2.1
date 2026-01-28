using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurriculumSelection.Models
{
    public class FiveLayerMVCCategory : Object
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 FiveLayerMVCCategoryID { get; set; }
        public String FiveLayerMVCCategoryName { get; set; }
        public ICollection<Curriculum> CurriculumICollection { get; set; }
    }
}