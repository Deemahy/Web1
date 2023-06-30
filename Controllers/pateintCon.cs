using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web1.Models;

namespace Web1.Controllers
{
    public class pateintCon : Controller
    {
        private readonly Gp2Context _context;

        public pateintCon(Gp2Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult emargencyCase()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> emargencyCase([Bind("EmergencyId,PId,DId,PatientMassages,DoctorResponse")] Emergency emergency)
        {
            var idNumber = (int)HttpContext.Session.GetInt32("pateint");
            var pateint = _context.Patients.Where(x => x.Users == idNumber).SingleOrDefault();
            emergency.PId = pateint.PatientId;
            _context.Add(emergency);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }
        public IActionResult Doctors(int? id)
        {

            var doc = _context.Doctors.ToList();
            var userr = _context.Users.ToList();
            var fullDoctorData = from d in doc
                                 join u in userr
                                 on d.UserId equals u.UserId
                                 select new JoinTable { JTdoctor = d, JTuser = u };

            if (id == null || id == 0)
            {


                return View(fullDoctorData);
            }
            else
            {
                fullDoctorData = from d in doc
                                 join u in userr
                                 on d.UserId equals u.UserId
                                 where d.ClinicId == id
                                 select new JoinTable { JTdoctor = d, JTuser = u };
                return View(fullDoctorData);
                
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
        public IActionResult Vaccsine()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Book(int appointmentId)
        {

            var appointment = _context.Appontments.Find(appointmentId);
            var idNumber = (int)HttpContext.Session.GetInt32("pateint");
            var patientin = _context.Patients.Where(x => x.Users == idNumber).SingleOrDefault();
            var patient = patientin.PatientId;

            if (appointment != null && patientin != null)
            {

                appointment.PatId = patient;
                _context.SaveChanges();


                return RedirectToAction("Index");
            }
            else
            {

                return RedirectToAction("Error");
            }

        }


        public IActionResult MyBooks()
        {
            var idNumber = HttpContext.Session.GetInt32("pateint");
            var patient = _context.Patients.FirstOrDefault(x => x.Users == idNumber)?.PatientId;

            if (patient != null)
            {
                var clinicList = _context.Clinics.ToList();
                var doctorList = _context.Doctors.ToList();
                var appointmentList = _context.Appontments.ToList();
                var availabilityList = _context.AvailabiltyDates.ToList();
                var userList = _context.Users.ToList(); 
                var appointments = from app in appointmentList
                                   join doc in doctorList on app.DocId equals doc.DoctorId
                                   join date in availabilityList on app.AvailabiltyDateId equals date.AvailabiltyDateId
                                   join clinic in clinicList on doc.ClinicId equals clinic.ClinicId
                                   join user in userList on doc.UserId equals user.UserId
                                   where app.PatId == patient && (app.MidCase == null && app.Medesine == null)
                                   select new JoinTable
                                   {
                                       JTdoctor = doc,
                                       JTappontment = app,
                                       JTavailabiltyDate = date,
                                       JTclinic = clinic,
                                       JTuser = user
                                   };

                return View(appointments.ToList());
            }

            return RedirectToAction("Error");
        }
        public IActionResult Myvac()
        {
            var idNumber = (int)HttpContext.Session.GetInt32("pateint");
            var patientin = _context.Patients.Where(x => x.Users == idNumber).SingleOrDefault();
            var patient = patientin.PatientId;

            var patientVaccinations = from pv in _context.PatentVaccs
                                      join vaccine in _context.Vaccsines on pv.VaccId equals vaccine.VaccsineId
                                      where pv.PatientId == patient
                                      select new JoinTable
                                      {
                                          JTpatentVacc = pv,
                                          JTvaccsine = vaccine
                                      };

            return View(patientVaccinations);
        }

        public IActionResult DoctorResponce()
        {
            var idNumber = (int)HttpContext.Session.GetInt32("pateint");
            var patientin = _context.Patients.Where(x => x.Users == idNumber).SingleOrDefault();
            var patient = patientin.PatientId;

            var appointments = from app in _context.Appontments
                               join doc in _context.Doctors on app.DocId equals doc.DoctorId
                               join date in _context.AvailabiltyDates on app.AvailabiltyDateId equals date.AvailabiltyDateId
                               join clinic in _context.Clinics on doc.ClinicId equals clinic.ClinicId
                               join user in _context.Users on doc.UserId equals user.UserId
                               where app.PatId == patient &&( app.MidCase != null || app.Medesine!=null)
                               select new JoinTable
                               {
                                   JTappontment = app,
                                   JTdoctor = doc,
                                   JTavailabiltyDate = date,
                                   JTclinic = clinic,
                                   JTuser = user
                               };

            return View(appointments.ToList());

        }

        public IActionResult upCommingAppointment()
        {
            var idNumber = (int)HttpContext.Session.GetInt32("pateint");
            var patientin = _context.Patients.Where(x => x.Users == idNumber).SingleOrDefault();
            var patient = patientin.PatientId;

            var appointments = from app in _context.Appontments
                               join doc in _context.Doctors on app.DocId equals doc.DoctorId
                               join date in _context.AvailabiltyDates on app.AvailabiltyDateId equals date.AvailabiltyDateId
                               join clinic in _context.Clinics on doc.ClinicId equals clinic.ClinicId
                               join user in _context.Users on doc.UserId equals user.UserId
                               where app.PatId == patient && (app.MidCase == null && app.Medesine == null)
                               select new JoinTable
                               {
                                   JTappontment = app,
                                   JTdoctor = doc,
                                   JTavailabiltyDate = date,
                                   JTclinic = clinic,
                                   JTuser = user
                               };

            return View(appointments.ToList());

        }

        public IActionResult DeleteAppointment(int appointmentId)
        {
            var appointment = _context.Appontments.Find(appointmentId);

            if (appointment != null)
            {
                // Set the patient ID to null
                appointment.PatId = null;
                _context.SaveChanges();
            }

            return RedirectToAction("MyBooks");
        }

        public IActionResult showEmargine()
        {
            var idNumber = (int)HttpContext.Session.GetInt32("pateint");
            var patientin = _context.Patients.Where(x => x.Users == idNumber).SingleOrDefault();
            var patient = patientin.PatientId;

            var patientMessages = _context.Emergencies
                       .Where(e => e.PId == patient && e.DId ==null)
                       
                       .ToList();

            
            return View(patientMessages);
        }

        public IActionResult showEmargineResponce()
        {
            var idNumber = (int)HttpContext.Session.GetInt32("pateint");
            var patientin = _context.Patients.Where(x => x.Users == idNumber).SingleOrDefault();
            var patient = patientin.PatientId;
            var userList = _context.Users.ToList(); 
            var doctorList=_context.Doctors.ToList();
            var emargin= _context.Emergencies.ToList();
            var patientMessages = from e in emargin
                                  join d in doctorList on e.DId equals d.DoctorId
                                  join u in userList on d.UserId equals u.UserId
                                  where e.PId == patient && e.DId != null
                                  select new JoinTable
                                  {
                                      JTuser = u,
                                      JTemergencie = e,
                                      JTdoctor = d
                                  };


            return View(patientMessages);
        }

        public IActionResult allEmargineResponce()
        {
            //var idNumber = (int)HttpContext.Session.GetInt32("pateint");
            //var patientin = _context.Patients.Where(x => x.Users == idNumber).SingleOrDefault();
            //var patient = patientin.PatientId;
            var userList = _context.Users.ToList();
            var doctorList = _context.Doctors.ToList();
            var emargin = _context.Emergencies.ToList();
            var patientMessages = from e in emargin
                                  join d in doctorList on e.DId equals d.DoctorId
                                  join u in userList on d.UserId equals u.UserId
                                  where  e.DId != null
                                  select new JoinTable
                                  {
                                      JTuser = u,
                                      JTemergencie = e,
                                      JTdoctor = d
                                  };


            return View(patientMessages);
        }



        ////////////////////////////////////////////////////////////////////////
    }


}

