using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurriculumSelection.DB.Models
{
    public abstract class Person : Object
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 PersonID { get; set; }
        public String? PassportNumber { get; set; } //护照编号
        public String? IndentityCordNumber { get; set; } //身份证ID编号
        public String? TelephoneNumber { get; set; }
        public String? Email { get; set; }
        public String Name { get; set; }
        public Boolean? Gender { get; set; }

        public DateTime? BirthDay { get; set; }
        public String? SorcePlace { get; set; }
}
}