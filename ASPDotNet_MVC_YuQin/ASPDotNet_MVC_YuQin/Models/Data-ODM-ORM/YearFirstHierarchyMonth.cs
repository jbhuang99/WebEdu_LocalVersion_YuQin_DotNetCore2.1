using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurriculumSelection.Models
{
    public class YearFirstHierarchyMonth : Object
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 YearFirstHierarchyMonthID { get; set; }
        public Int32 YearFirstHierarchyMonthName { get; set; }
        public Int32 YearID { get; set; }
        public Year Year { get; set; }
    }
}