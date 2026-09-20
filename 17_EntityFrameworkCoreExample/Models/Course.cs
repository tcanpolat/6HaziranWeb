namespace _17_EntityFrameworkCoreExample.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        // Foreign key property
        public int StudentId { get; set; }
        // Navigation property
        public Student Student { get; set; }
    }
}
