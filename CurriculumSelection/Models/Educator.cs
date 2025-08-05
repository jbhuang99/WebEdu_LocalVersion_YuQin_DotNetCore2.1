﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurriculumSelection.Models
{
    public class Educator : Object
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 EducatorID { get; set; }
        public String? PassportNumber { get; set; } //护照编号
        public String? IndentityCordNumber { get; set; } //身份证ID编号
        public String? TelephoneNumber { get; set; }
        public String? Email { get; set; }
        public String Name { get; set; }
        public Int32 CurriculumID { get; set; }
        public Curriculum Curriculum { get; set; }
       
    }
}