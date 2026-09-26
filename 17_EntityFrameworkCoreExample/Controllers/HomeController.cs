using _17_EntityFrameworkCoreExample.Data;
using _17_EntityFrameworkCoreExample.Extensions;
using _17_EntityFrameworkCoreExample.Models;
using _17_EntityFrameworkCoreExample.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace _17_EntityFrameworkCoreExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly EducationContext _context;

        public HomeController(EducationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Linq kullanarak veritabanýndan tüm öðrencileri çekiyoruz.
            List<Student> students = _context.Students.ToList(); // select * from Students
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // CSRF saldýrýlarýna karþý koruma saðlar. Csrf => Cross Site Request Forgery
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }
        public IActionResult Details(int id)
        {
            // Dbde id ye göre öðrenci çekiyoruz.
            Student student = _context.Students.Find(id); // select * from Students where Id = id
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        public IActionResult Edit(int id)
        {
            Student student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    // Öðrenci güncelleniyor.
                    _context.Update(student);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException err)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw; //Hata fýrlatýlýr ve üst seviye hata yakalayýcýya iletilir.
                    }

                }
                return RedirectToAction("Index");

            }
            return View(student);
        }

        public bool StudentExists(int id)
        {
            // any() metodu, belirtilen koþulu saðlayan herhangi bir öðe olup olmadýðýný kontrol eder. Eðer varsa true, yoksa false döner.
            return _context.Students.Any(e => e.Id == id);
        }

        // Belirli bir öðrenciyi silme sayfasýna yönlendirme
        public IActionResult Delete(int id)
        {
            Student student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Student student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            // bulduðun öðrenciyi silme iþlemi
            _context.Students.Remove(student); //delete from Students where Id = id
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        public IActionResult QuerySyntax()
        {
            // Linq Query Syntax kullanarak 18 yaþýndan büyük öðrencileri çekiyoruz.
            // Query Syntax: from ... in ... where ... select ... Söz dizimi kullanýr.
            List<Student> students = (from s in _context.Students
                                      where s.Age > 18
                                      select s).ToList();
            return View("Index", students);
        }
        public IActionResult MethodSyntax()
        {
            // Linq Query Syntax kullanarak 18 yaþýndan küçük öðrencileri çekiyoruz.
            List<Student> students = _context.Students.Where(s => s.Age < 18).ToList(); // Select * from Students where Age < 18 Method Syntax: .Where(...) Söz dizimi kullanýr.
            return View("Index", students);
        }

        public IActionResult Join()
        {
            // Öðrenciler ve kurslar arasýnda join iþlemi yapýyoruz.

            // Query Syntax kullanarak join iþlemi yapýyoruz.
            //var studentCourses = from s in _context.Students
            //                     join c in _context.Courses on s.Id equals c.StudentId
            //                     select new
            //                     {
            //                         StudentName = s.Name,
            //                         CourseName = c.Title
            //                     };

            // Method Syntax kullanarak join iþlemi yapýyoruz.
            var studentCourses = _context.Students
                                .Join(_context.Courses,
                                s => s.Id,
                                c => c.StudentId,
                                (s, c) => new
                                {
                                    StudentName = s.Name,
                                    CourseName = c.Title
                                }).ToList();
            return View(studentCourses);
        }

        public IActionResult GroupByDepartment()
        {
            var groupedStudents = _context.Students
                .GroupBy(s => s.Department)
                .Select(g => new GroupedStudentViewModel
                {
                    Department = g.Key,
                    Students = g.ToList()
                }).ToList();

            return View(groupedStudents);

        }

        // Bazý DB iþleri için özel yardýmcý methodlar gerekebilir. Bunlara custom extension methodlar denir. 
        // Öðrencileri yaþ aralýklarýna göre gruplamak için bir extension method yazalým.

        public IActionResult CustomExtensionMethod()
        {
            List<Student> students = _context.Students.ToList();
            var groupedStudentsByAge = students.GroupByAgeRange(); // Extension method çaðrýsý
            return View(groupedStudentsByAge);
        }

        public IActionResult GetStudentByDepartment()
        {
            ViewData["Students"] = new List<Student>();
            return View();
        }

        [HttpPost]
        public IActionResult GetStudentByDepartment(string department)
        {
            var students = _context.Students.FromSqlInterpolated($"Exec GetStudentsByDepartment {department}");
            ViewData["Students"] = students;
            return View();
        }
    }
}
