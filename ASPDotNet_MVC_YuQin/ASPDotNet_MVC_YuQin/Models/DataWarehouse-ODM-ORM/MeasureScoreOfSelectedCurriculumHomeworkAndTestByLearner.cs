using System;
namespace CurriculumSelection.Models
{
    public class MeasureScoreOfSelectedCurriculumHomeworkAndTestByLearner : Object
    {
        public Int32 ScoreOfSelectedCurriculumHomeworkAndTestByLearnerID { get; set; }
        public String ScoreOfSelectedCurriculumHomeworkAndTestByLearnerNote { get; set; }

        public Int32 Score { get; set; } //单选题只需0/1分。
        //public Int32 CurriculumHomeworkAndTestID { get; set; }
        public Int32 LearnerID { get; set; }
       
        public DateTime? CurriculumHomeworkAndTestSelectedTime { get; set; }
        //public Grade? Grade { get; set; }
        public CurriculumHomeworkAndTest CurriculumHomeworkAndTest { get; set; }
        public Learner Learner { get; set; }
       
    }
}