using System;
namespace CurriculumSelection.DB.Models
{
    public class ScoreOfSelectedCurriculumByLearner : Object
    {
        public Int32 ScoreOfSelectedCurriculumByLearnerID { get; set; }
        public Single Score { get; set; }
        public Int32 CurriculumID { get; set; }
        //public Int32 LearnerID { get; set; }//Foreign key to Learner初始化数据时老是出错，暂时调换成为PersonID
        public Int32 PersonID { get; set; }

        public DateTime? CurriculumSelectedTime { get; set; }
        //public Grade? Grade { get; set; }
        public Curriculum Curriculum { get; set; }
        //public Learner Learner { get; set; }////Foreign key to Learner初始化数据时老是出错，暂时调换成为Person
        public Person Person { get; set; }

    }
}