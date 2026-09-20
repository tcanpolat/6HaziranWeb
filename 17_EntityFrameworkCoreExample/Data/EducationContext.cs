using _17_EntityFrameworkCoreExample.Models;
using Microsoft.EntityFrameworkCore;

namespace _17_EntityFrameworkCoreExample.Data
{
    public class EducationContext : DbContext
    {
        public EducationContext(DbContextOptions<EducationContext> options) : base(options)
        {
        }

        // Veritabanı tablolarına denk gelir.
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        // onmodelcreating metodu ile ilişkileri ve diğer konfigürasyonları belirleyebiliriz.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Öğrenci ve Kurs arasındaki ilişkiyi yapılandırma.
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Student) // Her kursun bir öğrencisi vardır.
                .WithMany(s => s.Courses) // Her öğrencinin birden fazla kursu olabilir.
                .HasForeignKey(c => c.StudentId); // Yabancı anahtar StudentId'dir.

        }
    }
}
