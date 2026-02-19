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
    #region snippet
    public class StudentsHTMLController : Controller
    {
        private readonly CurriculumSelectionDbContext _schoolDBContext;

        public StudentsHTMLController(CurriculumSelectionDbContext schoolDBContext)
        {
            _schoolDBContext = schoolDBContext;
        }
        #endregion

        // GET: LearnerDbSet
        
        public async Task<IActionResult> Index()
        {
            //return View(await _schoolDBContext.LearnerDbSet.ToListAsync());
            return this.View(await _schoolDBContext.LearnerDbSet.FromSqlInterpolated($"SELECT * FROM dbo.Learner").ToListAsync());
        }
        // GET: LearnerDbSet/Details/5
        public async Task<IActionResult> Details(Int32? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }
            // Student student = await _schoolDBContext.LearnerDbSet.FirstOrDefaultAsync((Student student) => (student.ID == id));
           Learner student = await _schoolDBContext.LearnerDbSet.FromSqlRaw($"SELECT * FROM dbo.Learner WHERE LearnerID={id}").FirstOrDefaultAsync();
            //Student student = _schoolDBContext.Database.ExecuteSqlRaw($"SELECT * FROM dbo.Learner WHERE ID={id}").FirstOrDefaultAsync();
            if (student == null)
            {
                return this.NotFound();
            }

            return this.View(student);
        }
       
        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }
        

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LearnerID,Name")] Learner student)
        {
            if (ModelState.IsValid)
            {
               // _schoolDBContext.Add(student);
                _schoolDBContext.Database.ExecuteSqlRaw(
                       $"INSERT INTO dbo.Learner(LearnerID,Name)  VALUES('{student.Name}', '{student.Name}') ");
                await _schoolDBContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: LearnerDbSet/Edit/5
     
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

           // var student = await _schoolDBContext.LearnerDbSet.FindAsync(id);
            Learner student = await _schoolDBContext.LearnerDbSet.FromSqlRaw($"SELECT * FROM dbo.Learner WHERE LearnerID={id}").FirstOrDefaultAsync();

            if (student == null)
            {
                return this.NotFound();
            }
            return View(student);
        }
        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LearnerID,Name")] Learner student)
        {
            if (id != student.LearnerID)
            {
                return this.NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //  _schoolDBContext.Update(student);
                    //In some scenarios, it may be necessary to execute SQL which does not return any data, typically for modifying data in the database or calling a stored procedure which doesn't return any result sets. This can be done via ExecuteSql.This executes the provided SQL and returns the number of rows modified. ExecuteSql protects against SQL injection by using safe parameterization, just like FromSql, and ExecuteSqlRaw allows for dynamic construction of SQL queries, just like FromSqlRaw does for queries.
                    
                  Int32 updatedROW=_schoolDBContext.Database.ExecuteSqlRaw(
                       $"UPDATE dbo.Learner SET Name='{student.Name}' WHERE LearnerID={id}") ;
                   await _schoolDBContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearnerExists(student.LearnerID))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return this.RedirectToAction(nameof(Index));
            }
            return this.View(student);
        }

       
        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

           // Student student = await _schoolDBContext.LearnerDbSet.FirstOrDefaultAsync((Student student) => student.ID == id);
            Learner student = await _schoolDBContext.LearnerDbSet.FromSqlRaw($"SELECT * FROM dbo.Learner WHERE LearnerID={id}").FirstOrDefaultAsync();

            if (student == null)
            {
                return this.NotFound();
            }

            return this.View(student);
        }
       
        // POST: LearnerDbSetDbSet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //Student student = await _schoolDBContext.LearnerDbSet.FindAsync(id);
            // _schoolDBContext.LearnerDbSet.Remove(student);
            Int32 updatedROW = _schoolDBContext.Database.ExecuteSqlRaw(
                       $"DELETE FROM dbo.Learner  WHERE ID={id}");
            await _schoolDBContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private Boolean LearnerExists(Int32 id)
        {
            return _schoolDBContext.LearnerDbSet.Any(delegate(Learner learner) {return learner.LearnerID == id; });
        }
    }
}
