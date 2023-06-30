using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Web1.Models;

namespace BabyShiled.Controllers
{
    public class ManegerCon : Controller
    {

        private readonly Gp2Context _context;

        public ManegerCon(Gp2Context context)
        {
            _context = context;
        }

 
        public IActionResult Index()
        {

            var idNumber = (int)HttpContext.Session.GetInt32("manegeer");
            var res = _context.Manegeers.Where(x => x.Ueserss == idNumber).SingleOrDefault();
            var IDclin = (int)res.CliniccId;

            var joinQuery = from app in _context.Appontments
                            join doc in _context.Doctors on app.DocId equals doc.DoctorId
                            join date in _context.AvailabiltyDates on app.AvailabiltyDateId equals date.AvailabiltyDateId
                            join clinic in _context.Clinics on doc.ClinicId equals clinic.ClinicId
                            join user in _context.Users on doc.UserId equals user.UserId
                            where clinic.ClinicId == IDclin && app.PatId !=null 
                            select new JoinTable
                            {
                                JTappontment = app,
                                JTdoctor = doc,
                                JTavailabiltyDate = date,
                                JTclinic = clinic,
                                JTuser = user
                            };

            var joinResult = joinQuery.ToList();

            return View(joinResult);

           
        }

        public IActionResult doctor()
        {
            var idNumber = (int)HttpContext.Session.GetInt32("manegeer");
            var res = _context.Manegeers.Where(x => x.Ueserss == idNumber).SingleOrDefault();
            var IDclin = (int)res.CliniccId;
            var doc = _context.Doctors.ToList();
            var userr = _context.Users.ToList();
            
            var fullDoctorData = from d in doc
                                 join u in userr
                                 on d.UserId equals u.UserId
                                 where d.ClinicId == IDclin
                                 select new JoinTable { JTdoctor = d, JTuser = u };
            return View(fullDoctorData);
        }
        public IActionResult addDoctor()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>  addDoctor([Bind("UserId,FullName,Phone,Email,Gender,DateBearth")] User user, string Username, string Password,string Speclized , string OtherInfo)
        {

            
            
                var idNumber = (int)HttpContext.Session.GetInt32("manegeer");
                var man = _context.Manegeers.Where(x => x.Ueserss == idNumber).SingleOrDefault();
                var  IDclin = (int)man.CliniccId;
                
            
            
           
                _context.Add(user);
                await _context.SaveChangesAsync();
                LogIn logIn = new LogIn();
                logIn.Username = Username;
                logIn.Password = Password;
                logIn.UersId = user.UserId;
                logIn.RoleId = 3;
                _context.Add(logIn);
                await _context.SaveChangesAsync();
                Doctor doctor = new Doctor();
                doctor.UserId = user.UserId;
                doctor.Speclized = Speclized;
                doctor.OtherInfo = OtherInfo;
                doctor.ClinicId = IDclin;
                _context.Add(doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
          
            return View(user);
        }
        public IActionResult profile()
        {
            return View();
        }

        public IActionResult deleteDoc(int doctorId)
        {
            var doctor = _context.Doctors.Find(doctorId);

            if (doctor != null)
            {
                 doctor = _context.Doctors.Find(doctorId);
                var user = _context.Users.SingleOrDefault(u => u.UserId == doctor.UserId);
                var login = _context.LogIns.SingleOrDefault(l => l.UersId == user.UserId);

                
                _context.LogIns.Remove(login);
                var appointments = _context.Appontments.Where(a => a.DocId == doctorId && a.MidCase == null && a.Medesine == null).ToList();

                foreach (var appointment in appointments)
                {
                    _context.Appontments.Remove(appointment);
                }
                
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));



        }


        public IActionResult upCome()
        {

            var idNumber = (int)HttpContext.Session.GetInt32("manegeer");
            var res = _context.Manegeers.Where(x => x.Ueserss == idNumber).SingleOrDefault();
            var IDclin = (int)res.CliniccId;

            var joinQuery = from app in _context.Appontments
                            join doc in _context.Doctors on app.DocId equals doc.DoctorId
                            join date in _context.AvailabiltyDates on app.AvailabiltyDateId equals date.AvailabiltyDateId
                            join clinic in _context.Clinics on doc.ClinicId equals clinic.ClinicId
                            join user in _context.Users on doc.UserId equals user.UserId
                            where clinic.ClinicId == IDclin && app.PatId == null
                            select new JoinTable
                            {
                                JTappontment = app,
                                JTdoctor = doc,
                                JTavailabiltyDate = date,
                                JTclinic = clinic,
                                JTuser = user
                            };

            var joinResult = joinQuery.ToList();

            return View(joinResult);


        }

        public IActionResult addNightshift()
        {
            var idNumber = (int)HttpContext.Session.GetInt32("manegeer");
            var mymaneger = _context.Manegeers.Where(x => x.Ueserss == idNumber).SingleOrDefault();
            var IDclin = (int)mymaneger.CliniccId;
            var doctorNames = from doctor in _context.Doctors
                              join user in _context.Users on doctor.UserId equals user.UserId
                              where doctor.ClinicId == IDclin
                              select new JoinTable
                              {
                                  JTuser = user,
                                  JTdoctor = doctor,
                              };
            
            return View(doctorNames);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult addNightshift([Bind(" Date, StartTime, EndTime")] NightShift nightShift, int uid)
        {
            var idNumber = (int)HttpContext.Session.GetInt32("manegeer");
            var mymaneger = _context.Manegeers.Where(x => x.Ueserss == idNumber).SingleOrDefault();
            var IDclin = (int)mymaneger.CliniccId;
            var doc = _context.Doctors.Where(x => x.UserId ==uid).SingleOrDefault();
           
                nightShift.DocIdd = doc.DoctorId;
                nightShift.ClinIdd = IDclin;    
                _context.NightShifts.Add(nightShift);
                _context.SaveChanges();

                return RedirectToAction("nightShift");
          
        }

        public IActionResult nightShift()
        {
            var idNumber = (int)HttpContext.Session.GetInt32("manegeer");
            var mymaneger = _context.Manegeers.Where(x => x.Ueserss == idNumber).SingleOrDefault();
            var IDclin = (int)mymaneger.CliniccId;
            var doctorNames = from doctor in _context.Doctors
                              join user in _context.Users on doctor.UserId equals user.UserId
                              join n in _context.NightShifts on doctor.DoctorId equals n.DocIdd
                              where doctor.ClinicId == IDclin
                              select new JoinTable
                              {
                                  JTuser = user,
                                  JTdoctor = doctor,
                                  JTnightShift=n
                              };

            return View(doctorNames);
        }

        public IActionResult deleteShift(int nightShiftId)
        {
            var nightShift = _context.NightShifts.Find(nightShiftId);

            if (nightShift != null)
            {
                
                _context.NightShifts.Remove(nightShift);
                _context.SaveChanges();
            }

            
            return RedirectToAction("index");
        }
    





        /////////////////////////////////
    }
}
