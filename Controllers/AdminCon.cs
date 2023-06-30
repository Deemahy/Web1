using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Web1.Models;

namespace BabyShiled.Controllers
{
    public class AdminCon : Controller
    {
        private readonly Gp2Context _context;

        public AdminCon(Gp2Context context)
        {
            _context = context;
        }
        public IActionResult AdminIndex()
        {
            var clinicList = _context.Clinics.ToList();
            var manegerList = _context.Manegeers.ToList();
            var patentList = _context.Users.ToList();
            var clinicManagerInfo = from clinic in clinicList
                                    join manager in manegerList on clinic.ClinicId equals manager.CliniccId
                                    join user in patentList on manager.Ueserss equals user.UserId
                                    select new JoinTable
                                    {
                                        JTclinic = clinic,
                                        JTmanegeer = manager,
                                        JTuser = user
                                    };
            return View(clinicManagerInfo);
        }
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult Add()
        {
            

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("ClinicId,ClinicName,ClinicAddress,ClinicPhone,ClinicLocation")] Clinic clinic , string clinicUsername , string clinicPassword,string FullName , string Phone ,string Email , string Username , string Password ,DateTime DateBearth, string Gender  )
        {
            if (ModelState.IsValid)
            {
                _context.Add(clinic);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(AdminIndex));
                //reseption
                User user = new User();
                user.FullName = clinic.ClinicName;
                user.Phone = clinic.ClinicPhone;
                _context.Add(user);
                await _context.SaveChangesAsync();
                Reseption reseption = new Reseption();
                reseption.Userssid = user.UserId;
                reseption.Clinicssid = clinic.ClinicId;
                _context.Add(reseption);
                await _context.SaveChangesAsync();
                LogIn logIn = new LogIn();
                logIn.Username = clinicUsername;
                logIn.Password = clinicPassword;    
                logIn.UersId = user.UserId;
                logIn.RoleId = 4;
                _context.Add(logIn);
                await _context.SaveChangesAsync();
                ///////////////////////////////////
                User umaneger = new User();
                umaneger.FullName = FullName;
                umaneger.Phone = Phone; 
                umaneger.Email = Email; 
                umaneger.Gender = Gender;
                umaneger.DateBearth = DateBearth;
                _context.Add(umaneger);
                await _context.SaveChangesAsync();
                /////////////////////////////////////
                LogIn logmaneger = new LogIn();
                logmaneger.Username= Username;  
                logmaneger.Password= Password;  
                logmaneger.UersId= umaneger.UserId;
                logmaneger.RoleId = 2;
                _context.Add(logmaneger);
                await _context.SaveChangesAsync();
                ///////////////////////////////////////
                Manegeer manegeer = new Manegeer();
                manegeer.CliniccId= clinic.ClinicId;
                manegeer.Ueserss = umaneger.UserId;
                _context.Add(manegeer);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(AdminIndex));
            }
            return View(clinic);
        }
    }
}
