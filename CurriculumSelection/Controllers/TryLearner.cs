using System;
using CurriculumSelection.Data;
using CurriculumSelection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CurriculumSelection.Controllers
{

    public class TryLearnerController : Controller {
       
        public IActionResult TryLearner()
        {
            Learner myLearner1 = new Learner();//Learner对象类型新创一个myLearner1学习者实例。

            Learner myLearner2 = new Learner();////Learner对象类型新创一个myLearner2学习者实例。

            myLearner1.LearnerID=256; //

            myLearner1.Gender = true;

            myLearner2.LearnerID = 2;

            myLearner2.Gender = false;

            return Content(myLearner1.LearnerID.ToString()+";"+ myLearner2.LearnerID.ToString() + ";" + myLearner1.Gender +";"+ myLearner2.Gender);
        }
       
    }
}
