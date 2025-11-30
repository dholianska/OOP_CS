namespace P01_StudentSystem.Data
{
    using P01_StudentSystem.Data.Models;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public static class StudentSystemContextExtensions
    {
        public static void Seed(this StudentSystemContext context)
        {
            context.Database.EnsureCreated();

            if (context.Students.Any() || context.Courses.Any())
            {
                return;
            }

            var random = new Random();

            var students = CreateStudents();
            var courses = CreateCourses();

            var resources = CreateResources(random, courses);
            var homeworks = CreateHomeworks(random, students, courses);
            var studentCourses = CreateStudentCourses(random, students, courses);


            context.Students.AddRange(students);
            context.Courses.AddRange(courses);
            context.Resources.AddRange(resources);
            context.HomeworkSubmissions.AddRange(homeworks);
            context.StudentCourses.AddRange(studentCourses);

            context.SaveChanges();
        }

        private static List<Student> CreateStudents()
        {
            return new List<Student>
            {
                new Student { Name = "Олександр Коваленко", PhoneNumber = "0671234567", RegisteredOn = new DateTime(2023, 09, 01), Birthday = new DateTime(2000, 05, 15) },
                new Student { Name = "Марія Петришин", PhoneNumber = "0509876543", RegisteredOn = new DateTime(2024, 01, 10) },
                new Student { Name = "Віктор Савчук", PhoneNumber = "0931112233", RegisteredOn = new DateTime(2023, 11, 20), Birthday = new DateTime(2001, 12, 01) },
                new Student { Name = "Ірина Мельник", PhoneNumber = "0685554433", RegisteredOn = new DateTime(2024, 02, 05) },
                new Student { Name = "Богдан Шевчук", PhoneNumber = "0956667788", RegisteredOn = new DateTime(2023, 08, 15), Birthday = new DateTime(1999, 03, 22) }
            };
        }

        private static List<Course> CreateCourses()
        {
            return new List<Course>
            {
                new Course { Name = "Основи C#", Description = "Вступ до програмування на C#.", Price = 499.99m, StartDate = new DateTime(2024, 09, 01), EndDate = new DateTime(2024, 12, 31) },
                new Course { Name = "Web Development", Description = "Створення динамічних веб-сайтів.", Price = 799.50m, StartDate = new DateTime(2024, 10, 01), EndDate = new DateTime(2025, 01, 31) },
                new Course { Name = "Бази даних EF Core", Description = "Робота з базами даних за допомогою Entity Framework Core.", Price = 550.00m, StartDate = new DateTime(2024, 08, 15), EndDate = new DateTime(2024, 11, 15) }
            };
        }

        private static List<Resource> CreateResources(Random random, List<Course> courses)
        {
            var resourceNames = new[] { "Вступне відео", "Презентація", "Код прикладу", "Документація API" };
            var resources = new List<Resource>();

            for (int i = 0; i < 8; i++)
            {
                resources.Add(new Resource
                {
                    Name = resourceNames[random.Next(resourceNames.Length)] + $" #{i + 1}",
                    Url = $"https://example.com/resource/{Guid.NewGuid()}",
                    ResourceType = (ResourceType)random.Next(0, 4),
                    Course = courses[random.Next(courses.Count)]
                });
            }
            return resources;
        }

        private static List<Homework> CreateHomeworks(Random random, List<Student> students, List<Course> courses)
        {
            var homeworks = new List<Homework>();
            var contentTypes = Enum.GetValues(typeof(ContentType));

            for (int i = 0; i < 10; i++)
            {
                homeworks.Add(new Homework
                {
                    Content = $"C:\\Users\\Submit\\HW_{i + 1}.zip",
                    ContentType = (ContentType)contentTypes.GetValue(random.Next(contentTypes.Length)),
                    SubmissionTime = DateTime.Now.AddDays(-random.Next(1, 30)),
                    Student = students[random.Next(students.Count)],
                    Course = courses[random.Next(courses.Count)]
                });
            }
            return homeworks;
        }

        private static List<StudentCourse> CreateStudentCourses(Random random, List<Student> students, List<Course> courses)
        {
            var studentCourses = new List<StudentCourse>();

            foreach (var student in students)
            {
                for (int i = 0; i < random.Next(1, 3); i++)
                {
                    var course = courses[random.Next(courses.Count)];

                    if (!studentCourses.Any(sc => sc.StudentId == student.StudentId && sc.CourseId == course.CourseId))
                    {
                        studentCourses.Add(new StudentCourse
                        {
                            Student = student,
                            Course = course
                        });
                    }
                }
            }
            return studentCourses;
        }
    }
}