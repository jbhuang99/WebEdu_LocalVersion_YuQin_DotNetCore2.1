using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurriculumSelectionDW.Models
{
    public class DimOrganization : Object
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 OrganizationID { get; set; }
        public String OrganizationName { get; set; }
        public String OrganizationAddress { get; set; }
    }
}