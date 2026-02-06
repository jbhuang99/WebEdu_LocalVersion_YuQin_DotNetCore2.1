using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurriculumSelectionDW.Models
{
    public class DimLearner : Object
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 LearnerID { get; set; }//并非真正需求Int32,只是为了便于映射VS数据库
        public String? PassportNumber { get; set; } //护照编号
        public String? IndentityCordNumber { get; set; } //身份证ID编号
        public String? TelephoneNumber { get; set; }
        public String? Email { get; set; }
        public String Name { get; set; }
        public Boolean? Gender { get; set; } //true代表男，false代表女。
        public Int32? LearnerSourcePlaceID { get; set; }
        public ICollection<MeasureScoreOfSelectedCurriculumByLearner> MeasureScoreOfSelectedCurriculumByLearnerICollection { get; set; } //Relationships(one-to-many) between two object models Based-on EF. these properties are called "navigations".
        public String? Fav { get; set; }
        public Int32 OrganizationID { get; set; }
        public DimOrganization DimOrganization { get; set; }
        public DimLearnerSourcePlace DimLearnerSourcePlace { get; set; }
    }
}
