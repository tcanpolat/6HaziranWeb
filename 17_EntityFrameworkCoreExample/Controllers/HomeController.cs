using _17_EntityFrameworkCoreExample.Data;
using _17_EntityFrameworkCoreExample.Extensions;
using _17_EntityFrameworkCoreExample.Models;
using _17_EntityFrameworkCoreExample.ViewModels;
using EFCore.BulkExtensions;
using ExcelDataReader;
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

        // Sql sorgularýný direkt olarak entity framework core aracýlýðýyla sql servere yollama
        public IActionResult RawSql()
        {
            List<Student> students =  _context.Students.FromSqlRaw("select * from Students where Age > 25").ToList();
            
            return View("Index",students);
        }

        // Javascript tarafýnda Ajax isteði ile çaðýrýlacak Transaction örneði
        // Transaction: Bir dizi iþlemin tek bir iþlem olarak ele alýnmasýdýr.
        // Eðer iþlemlerden biri baþarýsýz olursa, tüm iþlemler geri alýnýr (rollback).
        // Bu, veri bütünlüðünü korumak için önemlidir.
        [HttpPost]
        public IActionResult AddStudentsByTransaction([FromBody] List<Student> students)
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Students.AddRange(students);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                return StatusCode(500, "Öðrenciler eklenirken bir hata oluþtu");
            }

            return Ok("Öðrenciler baþarýyla eklendi");
        }

        public IActionResult BulkInsert()
        {
            List<Student> students = new List<Student>()
            {
                new Student { Name = "Ali", Age = 20, Department = "Computer Science" },
                new Student { Name = "Ayþe", Age = 22, Department = "Mathematics" },
                new Student { Name = "Mehmet", Age = 21, Department = "Physics" },
            };

            _context.BulkInsert(students);

           return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ExcelBulkInsert(IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("Dosya Seçilmedi.");

            var students = new List<Student>();

            // Excel dosyasýný okumak için Stream açýyoruz.
            using (var stream = file.OpenReadStream())
            {
                // Özellikle türkçe karakterler için Encoding.UTF8 kullanýyoruz.
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // ilk satýrýn baþlýk olduðunu varsayýyoruz ve atlýyoruz.
                    reader.Read();
                    while (reader.Read())
                    {
                        students.Add(new Student
                        {
                            Name = reader.GetValue(0)?.ToString(), // A sütun: Name
                            Age = int.Parse(reader.GetValue(1).ToString()), // B sütun: Age
                            Department = reader.GetValue(2).ToString() // C sütun: Department

                        });
                    }

                };
            }

            // Veritabanýna toplu ekleme iþlemi
            if (students.Any())
            {
                _context.BulkInsert(students);
            }
           
            List<Student> allStudents = _context.Students.ToList();

            return View("Index", allStudents);
        }

    }
}
