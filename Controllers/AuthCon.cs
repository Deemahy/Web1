
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Web1.Models;

namespace BabyShiled.Controllers
{
    public class AuthCon : Controller
    {
        //2 change to class name 

        private readonly Gp2Context _context;

        public AuthCon(Gp2Context context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return _context.Users != null ?
                        View(await _context.Users.ToListAsync()) :
                        Problem("Entity set 'Gp2Context.Users'  is null.");
        }

        //show form 
        public IActionResult SignIn()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //(1)change to class name
        //add obj from database 
        //take data 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> /*Create*/ SignIn([Bind("UserId,FullName,Phone,Email,Gender,DateBearth")] User user , string Username , string Password)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                LogIn logIn = new LogIn();
                logIn.Username = Username;
                logIn.Password = Password;
                logIn.UersId = user.UserId;
                logIn.RoleId =5;
                _context.Add(logIn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(SignUp));
            }
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp([Bind("Password,Username")] LogIn logIn)
        {
            // if (ModelState.IsValid) { }
            var usertry = _context.LogIns.Where(x => x.Username == logIn.Username && x.Password == logIn.Password).SingleOrDefault();
           
            if (usertry != null)
            {
                switch (usertry.RoleId) {

                    case 1: HttpContext.Session.SetInt32("admin", (int)usertry.UersId);
                        return RedirectToAction("AdminIndex", "AdminCon");
                    case 2:
                        HttpContext.Session.SetInt32("manegeer", (int)usertry.UersId);
                        return RedirectToAction("Index"     , "ManegerCon");
                    case 3:
                        HttpContext.Session.SetInt32("doctor", (int)usertry.UersId);
                        return RedirectToAction("Index", "doctorCon");
                    case 4:
                        HttpContext.Session.SetInt32("reseption", (int)usertry.UersId);
                        return RedirectToAction("Index"      , "ReseptionCon");
                    case 5:
                        HttpContext.Session.SetInt32("pateint", (int)usertry.UersId);
                        return RedirectToAction("Index", "pateintCon");

                }
            }
            return View();
        }
    }
}
