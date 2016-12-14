using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using System.Web;
using JinStore.Models;

namespace JinStore.Controllers
{
    public class StudentsController : Controller
    {
        
        //Get:student
        public ActionResult Index(int? updatedID)
        {
            ViewBag.UpdatedId = updatedID;
            if (students.Count == 0)
            {
                students.Add(new Student { ID = 1, FirstName = "Jimmy", LastName = "Ellis" });
                students.Add(new Student { ID = 2, FirstName = "Jin", LastName = "Xiao" });
                students.Add(new Student { ID = 3, FirstName = "Tessas", LastName = "Konkol" });
                students.Add(new Student { ID = 4, FirstName = "Serkan", LastName = "Nizam" });
                students.Add(new Student { ID = 5, FirstName = "Jerry", LastName = "Bony" });
            }
            return View(students);
        }
        private static List<Student> students = new List<Student>();

        //GET:students/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id.HasValue && students.Any(x => x.ID == id.Value))
            {
                return View(students.Single(x => x.ID == id.Value));
            }

            return HttpNotFound("No student found");
        }
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            Student s = students.First(x => x.ID == student.ID);
            s.DateOfBirth = student.DateOfBirth;
            s.FirstName = student.FirstName;
            s.LastName = student.LastName;
            return RedirectToAction("Index",new { UpdatedId=s.ID});
        }
    }
}
