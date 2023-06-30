using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Web1.Models;

namespace NewEdition.Controllers
{
    public class ReseptionCon : Controller
    {
        private readonly Gp2Context _context;
        public /*UsersController-->*/ReseptionCon(Gp2Context context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var idNumber = (int)HttpContext.Session.GetInt32("reseption");
            var res = _context.Reseptions.Where(x => x.Userssid == idNumber).SingleOrDefault();
            var IDclin = (int)res.Clinicssid;
            ViewBag.ID = IDclin;
            var doc = _context.Doctors.ToList();
            var pat = _context.Patients.ToList();
            var userr = _context.Users.ToList();
            var appointment = _context.Appontments.ToList();
            var avalable = _context.AvailabiltyDates.ToList();
            var fullInfo = from d in doc
                           join u in userr on d.UserId equals u.UserId
                           join a in avalable on d.DoctorId equals a.Doctorid
                           join app in appointment on d.DoctorId equals app.DocId
                           join p in pat on app.PatId equals p.PatientId
                           where d.ClinicId == IDclin
                           select new JoinTable
                           {
                               JTdoctor = d,
                               JTuser = u,
                               JTappontment = app,
                               JTpatient = p
                           };



            return View(fullInfo);
        }
    }
}
