using ergasiaMVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ergasiaMVC.Models;

namespace ergasiaMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly MVC_Project_DbContext mVC_Project_DbContext;

        public UserController(MVC_Project_DbContext mVC_Project_DbContext) {
            this.mVC_Project_DbContext = mVC_Project_DbContext;
        }

        [HttpGet]
        public async Task<IActionResult> LogIn(ErrorModel error)
        {
            if(error != null)ViewBag.error = error.ErrorMessage;
            return View("AuthenticateView");
        }

        [HttpPost]   
        public async Task<IActionResult> Authenticate(UserLoginViewModel userdata)
        {
            List<User> userstoBeAthenticated = new List<User>();
            List<Student> students = new List<Student>();
            List<Professor>professors= new List<Professor>();
            List<Secretary> secretaries = new List<Secretary>();

            userstoBeAthenticated= await mVC_Project_DbContext.Users.ToListAsync();
            students = await mVC_Project_DbContext.Students.ToListAsync();
            professors = await mVC_Project_DbContext.Professors.ToListAsync();  
            secretaries = await mVC_Project_DbContext.Secretaries.ToListAsync();


            foreach (User item in userstoBeAthenticated)
            {
                if (item.Username.Equals(userdata.username) && item.Password.Equals(userdata.password))
                {
                    if (item.Role.Equals("student"))
                    {
                        foreach (Student thing in students)
                        {
                            if (thing.USERS_username.Equals(item.Username))
                            {
                                Student LoggedIn = new Student();
                                LoggedIn = thing;
                                return RedirectToAction("ViewData", "Student", LoggedIn);
                            }
                        }
                    }
                    else if (item.Role.Equals("professor"))
                    {
                        foreach (Professor prof in professors)
                        {
                            if (prof.USERS_username.Equals(item.Username))
                            {
                                Professor LoggedIn = new Professor();
                                LoggedIn = prof;
                                return RedirectToAction("ViewProfData", "Professor", LoggedIn);
                            }
                        }
                    }
                    else if (item.Role.Equals("secretary"))
                    {
                        foreach (Secretary sec in secretaries)
                        {
                            if (sec.USERS_username.Equals(item.Username))
                            {
                                Secretary LoggedIn = new Secretary();
                                LoggedIn = sec;
                                return RedirectToAction("Index", "Secretary", LoggedIn);
                            }
                        }
                    }
                }
                               
            }

            // If user not found
            ErrorModel error = new ErrorModel();
            error.ErrorMessage = "No user found!";
            return RedirectToAction("LogIn", "User", error);
        }
    }
}
