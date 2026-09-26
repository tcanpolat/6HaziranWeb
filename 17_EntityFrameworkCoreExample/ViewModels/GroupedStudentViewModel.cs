using _17_EntityFrameworkCoreExample.Models;

namespace _17_EntityFrameworkCoreExample.ViewModels
{
    public class GroupedStudentViewModel
    {
        public string Department { get; set; }
        public List<Student> Students { get; set; }
    }
}
