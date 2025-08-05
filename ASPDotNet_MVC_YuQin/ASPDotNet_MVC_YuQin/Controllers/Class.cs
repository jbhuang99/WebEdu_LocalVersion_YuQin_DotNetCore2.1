using System;

namespace WebEdu_LocalVersion_YuQin_DotNetCore21
{
    public class SchoolPerson
    {
        public String ID { set; get; }
        public String Name { set; get; }
        public Boolean Gender { set; get; }
    }
    public class Student_JYJS : SchoolPerson//(默认是System.Object)
    {
        
        Int32 Score {  set; get; }

      //  Student_JYJS student_JYJS1 = new Student_JYJS();
       /**
        * student_JYJS1.ID="201210000";
        student_JYJS1.Name="谭";
        student_JYJS1.Gender=true;
        Student_JYJS student_JYJS2 = new Student_JYJS();
        student_JYJS1.ID="201210001";
        student_JYJS1.Name="huang";
        student_JYJS1.Gender=false;
       **/
    }
    public class Teacher_JYJS : SchoolPerson//(默认是System.Object)
    {
        String Curriculum { set; get; }
        //Student_JYJS student_JYJS1 = new Student_JYJS();
        /**
         * student_JYJS1.ID="201210000";
         student_JYJS1.Name="谭";
         student_JYJS1.Gender=true;
         Student_JYJS student_JYJS2 = new Student_JYJS();
         student_JYJS1.ID="201210001";
         student_JYJS1.Name="huang";
         student_JYJS1.Gender=false;
        **/
    }


}