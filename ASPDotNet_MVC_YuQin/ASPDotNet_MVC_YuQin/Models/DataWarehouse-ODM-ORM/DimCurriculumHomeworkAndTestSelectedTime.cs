using System;
namespace CurriculumSelectionDW.Models
{
    public class DimCurriculumHomeworkAndTestSelectedTime : Object
    {
        public Int32 CurriculumHomeworkAndTestSelectedTimeID { get; set; }
        public Int32? CalendarYear { get; set; } //例如2023、2024、2025等
        public Int32? CalendarSemester { get; set; }  //例如1表示上学期，2表示下学期    

    }
}