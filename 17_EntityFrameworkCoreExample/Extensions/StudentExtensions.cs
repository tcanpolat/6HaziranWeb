using _17_EntityFrameworkCoreExample.Models;

namespace _17_EntityFrameworkCoreExample.Extensions
{
    public static class StudentExtensions
    {
        public static IDictionary<string,List<Student>> GroupByAgeRange(this IEnumerable<Student> students)
        {
            return students.GroupBy
                            (s =>
                            {
                                if (s.Age < 18) return "18 yaş altı";
                                if (s.Age < 25) return "18-24 yaş arası";
                                if (s.Age < 35) return "25-34 yaş arası";
                                return "35 yaş üzeri";
                            }).ToDictionary(s => s.Key, s => s.ToList());
        }
    }
}
