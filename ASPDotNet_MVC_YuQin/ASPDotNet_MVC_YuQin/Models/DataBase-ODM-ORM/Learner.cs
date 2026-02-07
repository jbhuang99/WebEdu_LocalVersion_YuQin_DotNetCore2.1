using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurriculumSelection.DB.Models
{
    public class Learner:Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 LearnerID { get; set; }
        public DateTime? EnrollmentDateTime { get; set; }
        public ICollection<ScoreOfSelectedCurriculumByLearner> ScoreOfSelectedCurriculumByLearnerICollection { get; set; } //Relationships(one-to-many) between two object models Based-on EF. these properties are called "navigations".
    }
}