using Microsoft.AspNetCore.Mvc;
using ergasiaMVC.Models;
using System;
using ergasiaMVC.Data;
using Microsoft.EntityFrameworkCore;

public class StudentController : Controller
{
    private readonly MVC_Project_DbContext mVC_Project_DbContext;

        public StudentController(MVC_Project_DbContext mVC_Project_DbContext) {
            this.mVC_Project_DbContext = mVC_Project_DbContext;
        }
    [HttpGet]
    public async Task <IActionResult> ViewData(Student student)
    {
            List<course_has_students> courses = new List<course_has_students>();
            List <course_has_students> studentsallgrades = new List<course_has_students>();
            List <Course> all_courses= new List<Course>();
            List<gradeViewModel> viewGrades = new List<gradeViewModel>();
            Dictionary <string,double> semesterAverages = new Dictionary<string,double>();
            Dictionary <string,List<gradeViewModel>> gradeBySemester = new Dictionary<string, List<gradeViewModel>>();
            double average=0; 
            courses = await mVC_Project_DbContext.courses_have_students.ToListAsync();
            all_courses= await mVC_Project_DbContext.Courses.ToListAsync();

            foreach (course_has_students grade in courses){
                if (grade.STUDENTS_RegistrationNumber.Equals(student.RegistrationNumber)){
                    studentsallgrades.Add(grade);
                }
            }

            foreach (course_has_students course in studentsallgrades){
                foreach (Course thing in all_courses){
                    if (thing.idCOURSE.Equals(course.COURSE_idCOURSE)&& course.GradeCourseStudent!=-1){
                        viewGrades.Add(new gradeViewModel(thing.CourseTitle,course.GradeCourseStudent,thing.CourseSemester));
                    }
                }
            } 
            foreach (gradeViewModel grade in viewGrades){
                if(!gradeBySemester.ContainsKey(grade.semester)){
                gradeBySemester.Add(grade.semester,new List<gradeViewModel>());
                }
                average+=(int)grade.grade_value;
                if (!semesterAverages.ContainsKey(grade.semester)){
                    semesterAverages.Add(grade.semester,calculateSemesterAverage(grade.semester,viewGrades));
                }
            }
            foreach (KeyValuePair<string, List<gradeViewModel>> item in gradeBySemester){
                foreach (gradeViewModel grade in viewGrades){
                    if (!gradeBySemester[grade.semester].Contains(grade))
                    gradeBySemester[grade.semester].Add(grade);
                }
            }
            Dictionary <string,List<gradeViewModel>> sortedDict = new Dictionary<string,List<gradeViewModel>>();
            sortedDict = gradeBySemester.OrderBy(x => x.Key).ToDictionary(pair => pair.Key, pair => pair.Value);
            ViewBag.SemesterAverages=semesterAverages;
            ViewBag.GradeBySemester= sortedDict;
            average=average/viewGrades.Count();
            ViewBag.student=student;
            ViewBag.studentAverage=Math.Round(average,1);
            return View(viewGrades);
        }
    public double calculateSemesterAverage(String semester, List<gradeViewModel> grades){
        double semesterAverage=0;
        int count=0;
        foreach (gradeViewModel grade in grades){
            if (grade.semester.Equals(semester)){
                semesterAverage+=(int)grade.grade_value;
                count+=1;
            }
        }
        semesterAverage=semesterAverage/count;
        return Math.Round(semesterAverage,1);
    }
    }
