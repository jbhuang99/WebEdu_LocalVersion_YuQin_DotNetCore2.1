using System;

namespace CurriculumSelection.Models
{
    public class ErrorViewModelForDW : Object
    {
        public String RequestId { get; set; }

        //public Boolean ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public Boolean ShowRequestId { 
            get { return !String.IsNullOrEmpty(RequestId); }
            set {; } 
        }
    }
}
