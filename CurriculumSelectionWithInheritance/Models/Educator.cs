using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurriculumSelectionWithInheritance.Models
{
    public class Educator:Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 EducatorID { get; set; }
        public DateTime? HireDateTime { get; set; }
        public Int32 CurriculumID { get; set; }
        public Curriculum Curriculum { get; set; }
       
    }
}