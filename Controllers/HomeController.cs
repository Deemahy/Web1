using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Diagnostics;
using Web1.Models;

namespace NewEdition.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Gp2Context _context;

        
        public HomeController(ILogger<HomeController> logger , Gp2Context context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            
            

            return View();
        }
        public IActionResult Doctors(int ? id)
        {

            var doc = _context.Doctors.ToList(); 
            var userr= _context.Users.ToList();
            var fullDoctorData = from d in doc
                                 join u in userr
                                 on d.UserId equals u.UserId 
                                 select  new JoinTable {JTdoctor=d , JTuser=u };

            if (id == null || id == 0)
            {

                
                return View(fullDoctorData);
            }
            else
            {
                fullDoctorData = from d in doc
                                     join u in userr
                                     on d.UserId equals u.UserId where d.ClinicId == id
                                     select new JoinTable { JTdoctor = d, JTuser = u };
                                     return View(fullDoctorData);
                //ViewBag.CId = id;
                //var myDoctors = _context.Doctors.Where(x => x.ClinicId == id);
                //return View(myDoctors);
            }
        }
        public IActionResult Clinics()
        {
            
            var myClinic = _context.Clinics.ToList();
            return View(myClinic);
        }
        public IActionResult Appointment(int id)
        {
			var appointments = from app in _context.Appontments
							   join doc in _context.Doctors on app.DocId equals doc.DoctorId
							   join date in _context.AvailabiltyDates on app.AvailabiltyDateId equals date.AvailabiltyDateId
							   where app.DocId == id && app.PatId == null
							   select new JoinTable
							   {
								   JTappontment = app,
								   JTdoctor = doc,
								   JTavailabiltyDate = date
							   };

			return View(appointments);
		}

        public IActionResult allEmargineResponce()
        {
            
            var userList = _context.Users.ToList();
            var doctorList = _context.Doctors.ToList();
            var emargin = _context.Emergencies.ToList();
            var patientMessages = from e in emargin
                                  join d in doctorList on e.DId equals d.DoctorId
                                  join u in userList on d.UserId equals u.UserId
                                  where e.DId != null
                                  select new JoinTable
                                  {
                                      JTuser = u,
                                      JTemergencie = e,
                                      JTdoctor = d
                                  };


            return View(patientMessages);
        }

        public IActionResult AllappointmentInfo ()
        {
            var doctorList = _context.Doctors.ToList ();
			
			var userList = _context.Users.ToList();
            var appointList=_context.Appontments.ToList(); 
            var dateList = _context.AvailabiltyDates.ToList();
            var clinicList = _context.Clinics.ToList();
			var doctorAppointments = (from doctor in doctorList
									  join appointment in appointList on doctor.UserId equals appointment.DocId
                                      join date in dateList on appointment.AvailabiltyDateId equals date.AvailabiltyDateId
                                      join  user in userList on  doctor.UserId equals user.UserId
                                      join  clinic in clinicList on appointment.Ciliid equals clinic.ClinicId
                                      where appointment.PatId==null 
									  orderby date.Date 
									  select new JoinTable
									  {
										  JTappontment = appointment,   
                                          JTavailabiltyDate = date, 
                                          JTdoctor = doctor,    
                                          JTclinic = clinic,
                                          JTuser = user,    

									  }).ToList();

			return View(doctorAppointments);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}