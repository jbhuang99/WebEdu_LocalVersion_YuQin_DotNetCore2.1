using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurriculumSelection.Models
{
    public class Country : Object
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 CountryID { get; set; }
        public String CountryName { get; set; }
    }
}