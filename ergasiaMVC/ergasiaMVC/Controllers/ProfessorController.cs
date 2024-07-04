using Microsoft.AspNetCore.Mvc;
using ergasiaMVC.Models;
using System;
using ergasiaMVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace ergasiaMVC.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly MVC_Project_DbContext mVC_Project_DbContext;

        public ProfessorController(MVC_Project_DbContext mVC_Project_DbContext)
        {
            this.mVC_Project_DbContext = mVC_Project_DbContext;
        }
        [HttpGet]
        public async Task <IActionResult> ViewProfData(Professor professor)
        {
            List<course_has_students> gradedCourses = new List<course_has_students>();
            List<Course> allCourses= new List<Course>();
            List<gradedCoursesViewModel> viewGradedCourses = new List<gradedCoursesViewModel>();
            List<Course> profCourses = new List<Course>();
            allCourses = await mVC_Project_DbContext.Courses.ToListAsync();
            gradedCourses = await mVC_Project_DbContext.courses_have_students.ToListAsync();
            foreach (course_has_students course in gradedCourses ){
                foreach (Course thing in allCourses){
                    if(thing.PROFESSORS_AFM.Equals(professor.AFM)){
                        if(!profCourses.Contains(thing)){
                        profCourses.Add(thing);
                        }
                    if (thing.idCOURSE.Equals(course.COURSE_idCOURSE)){
                        viewGradedCourses.Add(new gradedCoursesViewModel(thing.CourseTitle,course.GradeCourseStudent,thing.CourseSemester,course.STUDENTS_RegistrationNumber));
                    }
                    }
                    }
                }
            ViewBag.professorData=professor;
            ViewBag.gradedCourses=viewGradedCourses;
            ViewBag.professorCourses=profCourses;
            ViewBag.userMessage=TempData["errorMessage"] as string;
            return View();
        }
        [HttpPost]
         public async Task <IActionResult> AddStudentGrade(CoursesToBeGradedViewModel newGradeData,IFormCollection form){
            string userMessage="";
            List<course_has_students>allGrades= new List<course_has_students>();
            allGrades= await mVC_Project_DbContext.courses_have_students.ToListAsync();
            if (allGrades.Any(x=>(x.COURSE_idCOURSE.Equals(newGradeData.course_id) && x.STUDENTS_RegistrationNumber.Equals(newGradeData.studentRegNum)))){
                            course_has_students newGrade = mVC_Project_DbContext.courses_have_students.SingleOrDefault(x=> (x.COURSE_idCOURSE.Equals(newGradeData.course_id)&&x.STUDENTS_RegistrationNumber.Equals(newGradeData.studentRegNum)));
                            newGrade.GradeCourseStudent=newGradeData.grade_value;
                            mVC_Project_DbContext.SaveChanges();
                            userMessage="Grade Added Successfully";
            }else{
            userMessage="No student found registered to that course";
            }
        int profAFM = Int32.Parse(form["Professor.AFM"]);
        string profUsername = form["Professor.USERS_username"].ToString();
        string profName = form["Professor.Name"].ToString();
        string profSurname = form["Professor.Surname"].ToString();
        string profDepartment = form["Professor.Department"].ToString();
        Professor globalProfessor = new Professor();
        globalProfessor.AFM=profAFM;
        globalProfessor.USERS_username=profUsername;
        globalProfessor.Name=profName;
        globalProfessor.Surname=profSurname;
        globalProfessor.Department=profDepartment;
        TempData["errorMessage"]=userMessage;
        return RedirectToAction("ViewProfData", "Professor", globalProfessor);
    }
    }
}
