using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurriculumSelectionWithInheritance.Models
{
    public class CurriculumCategory : Object
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 CurriculumCategoryID { get; set; }
        public String CurriculumCategoryName { get; set; }
        public ICollection<Curriculum> CurriculumICollection { get; set; }
    }
}