using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using StudentsManagementSystem.StudentsManagementService;

namespace StudentsManagementSystem.Models.Students
{
    public class StudentModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public double Gpi { get; set; }

        public IEnumerable<SelectListItem> CourseChoices { get; set; }
        public List<StudentCourseModel> CourseModel { get; set; }

        public List<StudentModel> StudentModels { get; set; }

        public StudentModel()
        {
            StudentModels = new List<StudentModel>();
            CourseModel = new List<StudentCourseModel>();
        }

        public StudentModel Prepare(StudentDetailsDto details, List<Course> courses)
        {
            var model = new StudentModel();
            Id = details.StudentId;
            FirstName = details.FirstName;
            LastName = details.LastName;
            BirthDate = details.BirthDate;
            if(courses?.Any() ?? true)
            {
                model.CourseModel = courses.Select(course => new StudentCourseModel()
                {
                    CourseName = course.Name
                }).ToList();
            }
            return model;
        }

        public IEnumerable<StudentModel> Prepare(IEnumerable<Student> students)
        {
            return students.Select(student => new StudentModel()
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                BirthDate = student.BirthDate,
                Gpi = student.Gpi,
                CourseModel = student.Courses.Select(course => new StudentCourseModel()
                {
                    CourseName = course.Name
                }).ToList()
            });
        }
    }

    public class StudentCourseModel
    {
        public string CourseName { get; set; }
    }
}