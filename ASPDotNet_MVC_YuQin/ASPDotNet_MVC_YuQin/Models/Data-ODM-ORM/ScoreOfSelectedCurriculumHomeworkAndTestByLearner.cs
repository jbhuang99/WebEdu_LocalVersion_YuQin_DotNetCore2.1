using System;
namespace CurriculumSelection.Models
{
    public class ScoreOfSelectedCurriculumHomeworkAndTestByLearner : Object
    {
        public Int32 ScoreOfSelectedCurriculumHomeworkAndTestByLearnerID { get; set; }
        public Single Score { get; set; }
        public Int32 CurriculumHomeworkAndTestID { get; set; }
        public Int32 LearnerID { get; set; }
       
        public DateTime? CurriculumHomeworkAndTestSelectedTime { get; set; }
        //public Grade? Grade { get; set; }
        public CurriculumHomeworkAndTest CurriculumHomeworkAndTest { get; set; }
        public Learner Learner { get; set; }
       
    }
}