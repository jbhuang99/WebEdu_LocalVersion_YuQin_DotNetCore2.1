using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurriculumSelection.Models
{
    public class Year : Object
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 YearID { get; set; }
        public Int32 YearName { get; set; }
    }
}