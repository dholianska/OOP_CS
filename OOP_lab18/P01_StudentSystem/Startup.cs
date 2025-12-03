namespace P01_StudentSystem
{
    using P01_StudentSystem.Data;
    using P01_StudentSystem.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;

    public class Startup
    {
        public static void Main(string[] args)
        {
            using (var context = new StudentSystemContext())
            {
                context.Seed();
            } 

            Console.WriteLine("База даних 'StudentSystemDB' успішно заповнена даними.");
            Console.WriteLine("------------------------------------------");

            using (var readContext = new StudentSystemContext())
            {
                ReadCourseInformation(readContext);

                Console.WriteLine("------------------------------------------");

                ReadStudentInformation(readContext);
            }
        }

        private static void ReadCourseInformation(StudentSystemContext context)
        {
            Console.WriteLine("Інформація про Курси:");

            var coursesInfo = context.Courses
                .Include(c => c.Resources)
                .Include(c => c.StudentsEnrolled)
                .OrderBy(c => c.Name)
                .Select(c => new
                {
                    c.Name,
                    c.Price,
                    ResourcesCount = c.Resources.Count(),
                    StudentsCount = c.StudentsEnrolled.Count(),
                    Duration = c.EndDate.Subtract(c.StartDate).TotalDays
                })
                .ToList();

            foreach (var course in coursesInfo)
            {
                Console.WriteLine($"Курс: {course.Name} (Цiна: {course.Price} UAH)");
                Console.WriteLine($"  Тривалiсть: {course.Duration:F0} днiв.");
                Console.WriteLine($"  Студентiв: {course.StudentsCount}, Ресурсів: {course.ResourcesCount}");
            }
        }

        private static void ReadStudentInformation(StudentSystemContext context)
        {
            Console.WriteLine("Iнформація про Студентiв:");

            var studentsInfo = context.Students
                .Include(s => s.CourseEnrollments)
                .ThenInclude(sc => sc.Course)     
                .Include(s => s.HomeworkSubmissions)
                .OrderBy(s => s.Name)
                .Select(s => new
                {
                    s.Name,
                    s.RegisteredOn,
                    CourseCount = s.CourseEnrollments.Count(),
                    HomeworkCount = s.HomeworkSubmissions.Count()
                })
                .ToList();

            foreach (var student in studentsInfo)
            {
                Console.WriteLine($"Студент: {student.Name}");
                Console.WriteLine($"  Дата реєстрацiї: {student.RegisteredOn.ToShortDateString()}");
                Console.WriteLine($"  Кiлькiсть курсiв: {student.CourseCount}, Домашніх завдань: {student.HomeworkCount}");
            }
        }
    }
}