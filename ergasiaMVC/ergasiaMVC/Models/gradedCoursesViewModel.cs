namespace ergasiaMVC.Models
{
    public class gradedCoursesViewModel
    {
        public string course_title {get;set;}
        public int grade_value {get;set;}
        public string semester {get;set;}
        public int studentRegNum{get;set;}
    public gradedCoursesViewModel(string course_title,int grade_value,string semester,int studentRegNum)
    {
        this.studentRegNum= studentRegNum;
        this.course_title=course_title;
        this.grade_value=grade_value;
        this.semester=semester;
    }
}
}
