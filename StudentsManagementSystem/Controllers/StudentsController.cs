using System.Linq;
using System.Web.Mvc;
using StudentsManagementSystem.Models.Students;
using StudentsManagementSystem.StudentsManagementService;

namespace StudentsManagementSystem.Controllers
{
    public class StudentsController : Controller
    {
        public ActionResult List()
        {
            ExecutorServiceClient client = new ExecutorServiceClient();
            var students = client.ReadStudent(new StudentListQuery());

            var model = new StudentModel();
            model.StudentModels = model.Prepare(students).ToList();

            return View(model.StudentModels);
        }

        public ActionResult Details(int id)
        {
            ExecutorServiceClient client = new ExecutorServiceClient();
            //Need To Refactor, we should add it into StudentDetailsQuery
            var studentCourse = client.GetCourseByStudentId(id).ToList();

            var details = client.StudentDetails(new StudentDetailsQuery()
            {
                Id = id
            });
            var model = new StudentModel();
            var studentDetail = model.Prepare(details,studentCourse);
            return View(studentDetail);
        }

        public ActionResult Create()
        {
            ExecutorServiceClient client = new ExecutorServiceClient();
            var courses = client.ReadCourses(new CourseListQuery());
            var student = new CreateStudentCommand();

            return View(student);
        }

        [HttpPost]
        public ActionResult Create(CreateStudentCommand command)
        {
            try
            {
                ExecutorServiceClient client = new ExecutorServiceClient();
                client.CreateStudent(command);

                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            ExecutorServiceClient client = new ExecutorServiceClient();
            var student = client.ReadStudent(new StudentListQuery()
            {
                Id = id
            }).FirstOrDefault();
            
            var model = new UpdateStudentCommand()
            {
                Id = student.Id,
                LastName = student.LastName,
                FirstName = student.FirstName,
                BirthDate = student.BirthDate,
                Gpi = student.Gpi
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(UpdateStudentCommand command)
        {
            try
            {
                ExecutorServiceClient client = new ExecutorServiceClient();
                client.UpdateStudent(command);

                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            ExecutorServiceClient client = new ExecutorServiceClient();
            client.DeleteStudent(new DeleteStudentCommand()
            {
                Id = id
            });

            return RedirectToAction("List");
        }
    }
}
