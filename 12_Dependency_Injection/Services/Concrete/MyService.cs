using _12_Dependency_Injection.Models;
using _12_Dependency_Injection.Services.Abstract;

namespace _12_Dependency_Injection.Services.Concrete
{
    public class MyService : IMyService
    {
        public List<Student> GetStudents()
        {
            return new List<Student>()
            {
                new Student() {Id=1,Name = "Alice",Age =20},
                new Student() {Id=2,Name = "Bob",Age =22},
                new Student() {Id=3,Name = "Charlie",Age =21}
            };
        }
    }
}
