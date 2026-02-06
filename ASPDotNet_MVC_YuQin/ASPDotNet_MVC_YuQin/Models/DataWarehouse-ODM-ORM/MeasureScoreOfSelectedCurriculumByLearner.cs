using System;
namespace CurriculumSelectionDW.Models
{
    public class MeasureScoreOfSelectedCurriculumByLearner : Object
    {
        public Int32 ScoreOfSelectedCurriculumByLearnerID { get; set; }
        //public String ScoreOfSelectedCurriculumByLearnerNote { get; set; }
        public Single Score { get; set; }
        public Int32 CurriculumID { get; set; }
        public Int32 LearnerID { get; set; }       
        public Int32 CurriculumSelectedTimeID { get; set; }
        //public Grade? Grade { get; set; }
        public DimCurriculum Curriculum { get; set; }
        public DimLearner Learner { get; set; }
        public DimCurriculumSelectedTime CurriculumSelectedTime { get; set; }

    }
}