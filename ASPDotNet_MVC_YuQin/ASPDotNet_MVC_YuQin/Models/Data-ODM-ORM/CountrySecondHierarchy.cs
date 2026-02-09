using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurriculumSelection.Models
{
    public class CountrySecondHierarchy : Object
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 CountrySecondHierarchyID { get; set; }
        public String CountrySecondHierarchyName { get; set; }
        public Int32 CountryFirstHierarchyID { get; set; }
        public CountryFirstHierarchy CountryFirstHierarchy { get; set; }
    }
}