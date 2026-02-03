using System;
namespace CurriculumSelection.Models
{
    public class DimCurriculumSelectedTime : Object
    {
        public Int32 ScoreOfSelectedCurriculumByLearnerID { get; set; }
        public String ScoreOfSelectedCurriculumByLearnerNote { get; set; }
        public Single Score { get; set; }
        public Int32 CurriculumID { get; set; }
        public Int32 LearnerID { get; set; }
       
        public DateTime? CurriculumSelectedTime { get; set; }
        //public Grade? Grade { get; set; }
        public Curriculum Curriculum { get; set; }
        public Learner Learner { get; set; }
       
    }
}