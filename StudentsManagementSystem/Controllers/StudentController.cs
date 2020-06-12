using System;
using System.Web.Mvc;
using StudentsManagementSystem.StudentsManagementService;

namespace StudentsManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateStudent()
        {
            ExecutorServiceClient client = new ExecutorServiceClient();

            var command = new CreateStudentCommand()
            {
                FirstName = "Saxeli",
                LastName = "Gvari",
                Gpi = 1.32,
                BirthDate = DateTime.Now
            };

            var result = client.CreateStudent(command);

            return RedirectToAction("Index");
        }

        public ActionResult ReadStudents()
        {
            ExecutorServiceClient client = new ExecutorServiceClient();
            var command = new StudentListQuery()
            {
                FirstName = "Bidz"
            };

            var result = client.ReadStudent(command);

            return RedirectToAction("Index");
        }

        public ActionResult UpdateStudent()
        {
            ExecutorServiceClient client = new ExecutorServiceClient();
            var command = new UpdateStudentCommand()
            {
                Id = 3,
                LastName = "Gvari",
                FirstName = "Saxeli",
                BirthDate = DateTime.Now
            };
           
            var result = client.UpdateStudent(command);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteStudent()
        {
            ExecutorServiceClient client = new ExecutorServiceClient();
            var command = new DeleteStudentCommand()
            {
                Id = 3
            };

            var result = client.DeleteStudent(command);

            return Index();
        }
    }
}