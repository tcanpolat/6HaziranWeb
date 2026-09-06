using _12_Dependency_Injection.Models;

namespace _12_Dependency_Injection.Services.Abstract
{
    public interface IMyService
    {
        // Öğrenci listesini döndürecek gövdesiz method
        List<Student> GetStudents();
    }
}
