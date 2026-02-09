using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurriculumSelection.Models
{
    public class CountryFirstHierarchy : Object
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 CountryFirstHierarchyID { get; set; }
        public String CountryFirstHierarchyName { get; set; }
        public Int32 CountryID { get; set; }
        public Country Country { get; set; }
    }
}