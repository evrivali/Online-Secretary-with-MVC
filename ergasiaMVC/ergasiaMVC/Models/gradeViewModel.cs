namespace ergasiaMVC.Models
{
    public class gradeViewModel
    {
        public string course_title {get;set;}
        public int grade_value {get;set;}
        public string semester {get;set;}
    public gradeViewModel(string course_title,int grade_value,string semester)
    {
        this.course_title=course_title;
        this.grade_value=grade_value;
        this.semester=semester;
    }
}
}
