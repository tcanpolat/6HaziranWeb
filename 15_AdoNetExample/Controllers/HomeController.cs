using _15_AdoNetExample.DbServices.Abstract;
using _15_AdoNetExample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace _15_AdoNetExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDbService _dbService;

        public HomeController(IDbService dbService)
        {
            _dbService = dbService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddData()
        {
            string query = "INSERT INTO Students (FirstName,LastName,Age) values ('Ali','Demir',21)";
            _dbService.ExecuteNonQuery(query);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddDataSecure([FromForm] Student model)
        {
            string query = "INSERT INTO Students " +
                "(FirstName,LastName,Age) values " +
                "(@FirstName,@LastName,@Age)";
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@FirstName",model.FirstName),
                new SqlParameter("@LastName",model.LastName),
                new SqlParameter("@Age",model.Age)
            };

            _dbService.ExecuteNonQuery(query,parameters);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetData()
        {
            string query = "SELECT FirstName,LastName,Age FROM Students";
            List<Student> students = _dbService.ExecuteReader(query);
            return View(students);
        }

        [HttpGet]
        public IActionResult GetCount()
        {
            string query = "SELECT COUNT(*) FROM Students";
            object count = _dbService.ExecuteScalar(query);
            return View(count);
        }

    }
}
