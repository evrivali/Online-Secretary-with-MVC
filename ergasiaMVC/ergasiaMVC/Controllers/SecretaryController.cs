using ergasiaMVC.Data;
using ergasiaMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ergasiaMVC.Controllers
{
    public class SecretaryController : Controller
    {
        private readonly MVC_Project_DbContext mVC_Project_DbContext;

        public SecretaryController(MVC_Project_DbContext mVC_Project_DbContext)
        {
            this.mVC_Project_DbContext = mVC_Project_DbContext;
        }


        [HttpGet]
        public async Task<IActionResult> Index(Secretary secretary)
        {
            ViewBag.Secretary = secretary;
            //ViewBag.Courses = mVC_Project_DbContext.Courses;
            ViewBag.CoursesDep = mVC_Project_DbContext.Courses.Where((course) => course.professor.Department.Equals(secretary.Department)).ToList();
            //ViewBag.CoursesNot = mVC_Project_DbContext.Courses.Where((course) => course.professor == null).ToList();
            ViewBag.Professors = mVC_Project_DbContext.Professors.Where((pro) => pro.Department.Equals(secretary.Department)).ToList();
            return View(secretary);
        }
  

        [HttpGet]
        public async Task<IActionResult> goBack(string username)
        {
            Secretary sec = mVC_Project_DbContext.Secretaries?.ToList().Find((sec) => sec.USERS_username.Equals(username));
            return RedirectToAction("Index",sec);
        }

        [HttpGet]
        public async Task<IActionResult> AddProfessorView(string department,string username)
        {
            AddProfessorViewModel model = new AddProfessorViewModel();
            model.professor = new Professor();
            model.professor.Department = department;
            model.secUsername = username;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewProfessor(AddProfessorViewModel addProfessorViewModel)
        {
            try
            {
                Professor toBeAdded = new Professor();
                toBeAdded.user = new User();
                toBeAdded.user.Username = addProfessorViewModel.professor.USERS_username;
                toBeAdded.user.Password = addProfessorViewModel.password;
                toBeAdded.user.Role = "professor";

                toBeAdded.USERS_username = addProfessorViewModel.professor.USERS_username;
                toBeAdded.Name = addProfessorViewModel.professor.Name;
                toBeAdded.Surname = addProfessorViewModel.professor.Surname;
                toBeAdded.Department = addProfessorViewModel.professor.Department;
                toBeAdded.AFM = addProfessorViewModel.professor.AFM;

                mVC_Project_DbContext.Add(toBeAdded);
                mVC_Project_DbContext.SaveChanges();

                Secretary sec = mVC_Project_DbContext.Secretaries?.ToList().Find((sec) => sec.USERS_username.Equals(addProfessorViewModel.secUsername));
                return RedirectToAction("Index", sec);
            }
            catch (Exception ex)
            {
                addProfessorViewModel.message = ex.Message;
                return View("AddProfessorView", addProfessorViewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddStudentView(string department,string username)
        {
            AddStudentViewModel model = new AddStudentViewModel();
            model.student = new Student();
            model.student.Department = department;
            model.secUsername = username;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewStudent(AddStudentViewModel addStudentViewModel)
        {
            try
            {
                Student toBeAdded = new Student();
                toBeAdded.user = new User();
                toBeAdded.user.Username = addStudentViewModel.student.USERS_username;
                toBeAdded.user.Password = addStudentViewModel.password;
                toBeAdded.user.Role = "student";

                toBeAdded.USERS_username = addStudentViewModel.student.USERS_username;
                toBeAdded.Name = addStudentViewModel.student.Name;
                toBeAdded.Surname = addStudentViewModel.student.Surname;
                toBeAdded.Department = addStudentViewModel.student.Department;
                toBeAdded.RegistrationNumber = addStudentViewModel.student.RegistrationNumber;

                mVC_Project_DbContext.Add(toBeAdded);
                mVC_Project_DbContext.SaveChanges();

                Secretary sec = mVC_Project_DbContext.Secretaries?.ToList().Find((sec) => sec.USERS_username.Equals(addStudentViewModel.secUsername));
                return RedirectToAction("Index", sec);
            }
            catch(Exception ex)
            {
                addStudentViewModel.message= ex.Message;
                return View("AddStudentView", addStudentViewModel);
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> AddCourseView(string department, string username)
        {
            AddCourseViewModel model = new AddCourseViewModel();
            model.course = new Course();
            model.secUsername = username;
            model.Department = department;
            ViewBag.department = department;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCourse(AddCourseViewModel addCourseViewModel)
        {
            try
            {
                Course toBeAdded = new Course();
                toBeAdded.CourseTitle = addCourseViewModel.course.CourseTitle;
                toBeAdded.CourseSemester = addCourseViewModel.course.CourseSemester;
                toBeAdded.PROFESSORS_AFM = addCourseViewModel.afm;

                Professor selected = mVC_Project_DbContext.Professors.ToList().Find((pro) => pro.AFM.Equals(toBeAdded.PROFESSORS_AFM));
                if (selected == null) {
                    throw new Exception("Professor doesnt exist");
                }
                if(selected.Department != addCourseViewModel.Department)
                {
                    throw new Exception("Professor doesnt belong to this department");
                }


                mVC_Project_DbContext.Add(toBeAdded);
                mVC_Project_DbContext.SaveChanges();

                Secretary sec = mVC_Project_DbContext.Secretaries?.ToList().Find((sec) => sec.USERS_username.Equals(addCourseViewModel.secUsername));
                return RedirectToAction("Index", sec);
            }
            catch (Exception ex)
            {
                addCourseViewModel.message = ex.Message;
                return View("AddCourseView", addCourseViewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> AssignCourseView(string department, string username)
        {
            Console.WriteLine(department);
            AssignCourseViewModel model = new AssignCourseViewModel();
            model.secUsername = username;
            model.Department = department;
            ViewBag.department = department;
            return View("AssignCourseView",model);
        }

        [HttpPost]
        public async Task<IActionResult> AssignNewCourse(AssignCourseViewModel assignCourseViewModel)
        {
            try
            {
                course_has_students tobeadded = new course_has_students();
                tobeadded.COURSE_idCOURSE = assignCourseViewModel.course;
                tobeadded.STUDENTS_RegistrationNumber = assignCourseViewModel.student;
                tobeadded.GradeCourseStudent=-1;

                Course? course = mVC_Project_DbContext.Courses.ToList().Find((c) => c.idCOURSE == tobeadded.COURSE_idCOURSE);
                if(course == null)
                {
                    throw new Exception("Course doesnt exist");
                }
                int? profAFM = course.PROFESSORS_AFM;
                Professor professor = mVC_Project_DbContext.Professors.ToList().Find((p)=>p.AFM == profAFM);
                if(professor.Department != assignCourseViewModel.Department)
                {
                    throw new Exception("Course doesnt belong to this department!");
                }

                Student? student = mVC_Project_DbContext.Students.ToList().Find((s) => s.RegistrationNumber == tobeadded.STUDENTS_RegistrationNumber);
                if(student == null)
                {
                    throw new Exception("Student does not exist");
                }
                if(student.Department != assignCourseViewModel.Department)
                {
                    throw new Exception("Student does not belong to this department!");
                }

                course_has_students? c = mVC_Project_DbContext.courses_have_students.ToList().Find((c) => c.STUDENTS_RegistrationNumber == tobeadded.STUDENTS_RegistrationNumber && c.COURSE_idCOURSE == tobeadded.COURSE_idCOURSE);
                if(c !=null)
                {
                    throw new Exception("Student already assigned to this course!");
                }
                mVC_Project_DbContext.Add(tobeadded);
                mVC_Project_DbContext.SaveChanges();

                assignCourseViewModel.messageS = "Success";
                assignCourseViewModel.message = "";
                return View("AssignCourseView", assignCourseViewModel);
            }
            catch (Exception ex)
            {
                assignCourseViewModel.message = ex.Message;
                assignCourseViewModel.messageS = "";
                return View("AssignCourseView", assignCourseViewModel);
            }
        }
    }
}
