using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Threading.Channels;
using System;
using Web1.Models;
using System.Data.Entity;

namespace Web1.Controllers
{
    public class DoctorCon : Controller
    {
        private readonly Gp2Context _context;

        public DoctorCon(Gp2Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var idNumber = (int)HttpContext.Session.GetInt32("doctor");
            var doctor = _context.Doctors.Where(x => x.UserId == idNumber).SingleOrDefault();

            if (doctor != null)
            {
                var doctorId = doctor.DoctorId;

                var appointments = _context.Appontments.ToList();
                var dates = _context.AvailabiltyDates.ToList();

                var appointmentDetails = from date in dates
                                         join appointment in appointments on date.AvailabiltyDateId equals appointment.AvailabiltyDateId into appointmentGroup
                                         from appointment in appointmentGroup.DefaultIfEmpty()
                                         where appointment?.DocId == doctorId && appointment?.PatId == null
                                         select new JoinTable
                                         {
                                             JTavailabiltyDate = date,
                                             JTappontment = appointment,
                                         };


                return View(appointmentDetails);
            }
            else
            {
                
                return RedirectToAction("NotFound", "Error");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FormDay([Bind("AvailabiltyDateId,Doctorid,Date")] AvailabiltyDate availabiltyDate)
        {
            var idNumber = (int)HttpContext.Session.GetInt32("doctor");
            var doct = _context.Doctors.Where(x => x.UserId == idNumber).SingleOrDefault();
            var IDdoctor = (int)doct.DoctorId;
            availabiltyDate.Doctorid = IDdoctor;
            _context.Add(availabiltyDate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(addTime));
        }
        public IActionResult addTime()
        {
            var idNumber = (int)HttpContext.Session.GetInt32("doctor");
            var doct = _context.Doctors.Where(x => x.UserId == idNumber).SingleOrDefault();
            var IDdoctor = (int)doct.DoctorId;
            var availabilityDates = _context.AvailabiltyDates
      .Where(a => a.Doctorid == IDdoctor)
      .Select(a => new SelectListItem
      {
          Value = a.AvailabiltyDateId.ToString(),
          Text = a.Date.Value.ToString("yyyy-MM-dd")
      })
      .ToList();

            ViewBag.AvailabilityDates = availabilityDates;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> addTime([Bind("AppontmentId,DocId,PatId,AvailabiltyDateId,StartTime,EndTime,Medesine,MidCase")] Appontment appontment)
        {
            var idNumber = (int)HttpContext.Session.GetInt32("doctor");
            var doctor = _context.Doctors.SingleOrDefault(x => x.UserId == idNumber);
            var doctorId = doctor?.DoctorId ?? 0;
            appontment.DocId = doctorId;
            appontment.Ciliid = doctor.ClinicId;
                var existingAppointments = _context.Appontments
                    .Where(a => a.DocId == appontment.DocId && a.AvailabiltyDateId == appontment.AvailabiltyDateId)
                    .ToList();

                bool hasConflict = existingAppointments.Any(a =>
                    (appontment.StartTime >= a.StartTime && appontment.StartTime < a.EndTime) ||
                    (appontment.EndTime > a.StartTime && appontment.EndTime <= a.EndTime) ||
                    (appontment.StartTime <= a.StartTime && appontment.EndTime >= a.EndTime)
                );

                if (hasConflict)
                {
                    ModelState.AddModelError("StartTime", "There is a time conflict with an existing appointment.");
                    return View(appontment);
                }

                _context.Add(appontment);
                await _context.SaveChangesAsync();
           return RedirectToAction(nameof(Index));
        
    }
        ////////////////
        [HttpPost]
        public IActionResult DeleteApp(int id)
        {
           
            var appointment = _context.Appontments.Find(id);

            if (appointment != null)
            {
               
                _context.Appontments.Remove(appointment);
                _context.SaveChanges();
            }

           
            return RedirectToAction("Index");
        }
        public IActionResult viewappointment()
        {
            var idNumber = (int)HttpContext.Session.GetInt32("doctor");
            var doctor = _context.Doctors.FirstOrDefault(d => d.UserId == idNumber);

            if (doctor == null)
            {
                
                return NotFound();
            }

            var appointments = (from appointment in _context.Appontments
                                join date in _context.AvailabiltyDates on appointment.AvailabiltyDateId equals date.AvailabiltyDateId
                                join patient in _context.Patients on appointment.PatId equals patient.PatientId
                                where appointment.DocId == doctor.DoctorId && appointment.PatId != null
                                select new JoinTable
                                {
                                    JTappontment = appointment,
                                    JTavailabiltyDate = date,
                                    JTuser = patient.PatientNavigation,
                                   
                                }).ToList();

            return View(appointments);
        }
        [HttpPost]
        public IActionResult Update(Appontment appointment)
        {
            var existingAppointment = _context.Appontments.Find(appointment.AppontmentId);

            if (existingAppointment == null)
            {
                
                return NotFound();
            }

            existingAppointment.Medesine = appointment.Medesine;
            existingAppointment.MidCase = appointment.MidCase;

            _context.SaveChanges();


            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var existingAppointment = _context.Appontments.Find(id);
            if (existingAppointment == null)
            {
                return NotFound();
            }

            return View(existingAppointment);
        }

        
        public IActionResult AddPatientVaccine()
        {
            var vaccines = _context.Vaccsines.ToList();
            ViewBag.VaccineList = new SelectList(vaccines, "VaccsineId", "VName");
            return View();
        }

        [HttpPost]
        public IActionResult AddPatientVaccine(PatentVacc patientVaccine)
        {
            if (ModelState.IsValid)
            {
               
                _context.PatentVaccs.Add(patientVaccine);
                _context.SaveChanges();

                
                return RedirectToAction("Index");
            }

            var vaccines = _context.Vaccsines.ToList();
            ViewBag.VaccineList = new SelectList(vaccines, "VaccsineId", "VName");

            return View(patientVaccine);
        }


        public IActionResult viewvac(int id)
        {

            var patientVaccines = _context.PatentVaccs.ToList();
            var userList = _context.Users.ToList();
            var vaccsineList = _context.Vaccsines.ToList();

            var patientVaccineData = from pv in _context.PatentVaccs
                                     join v in _context.Vaccsines on pv.VaccId equals v.VaccsineId 
                                     where pv.PatientId == id  
                                     select new JoinTable
                                     {
                                         JTpatentVacc = pv, 
                                         JTvaccsine = v,

                                     };

            return View(patientVaccineData);
        }

        public IActionResult addvac(int id)
        {
            ViewBag.VName = new SelectList(_context.Vaccsines, "VaccsineId", "VName");
            ViewBag.Vaccines = _context.Vaccsines.ToList();
            return View();  
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> addvac(int id, [Bind("PatientVacId,PatientId,VaccId,VacDay,Width,Headcircumference,Hight")] PatentVacc patentVacc)
        {
            patentVacc.PatientId = id;
            patentVacc.VacDay = DateTime.Now;
                _context.Add(patentVacc);
                await _context.SaveChangesAsync();
            return RedirectToAction("viewvac");
        }
        public IActionResult Emargincase(int id)
        {
           
            var emargin = _context.Emergencies.ToList();
            var patientMessages = from e in emargin.Where(e=>e.DId==null).ToList() select e ;
                                 


            return View(patientMessages);
        }
        
        public IActionResult allEmargincase(int id)
        {
            var emargin = _context.Emergencies.ToList();
            var patientMessages = from e in emargin.Where(e => e.DId != null).ToList() select e;


            return View(patientMessages);
        }

        public IActionResult myEmargincase(int id)
        {
            var idNumber = (int)HttpContext.Session.GetInt32("doctor");
            var doct = _context.Doctors.Where(x => x.UserId == idNumber).SingleOrDefault();
            var IDdoctor = (int)doct.DoctorId;
            var emargin = _context.Emergencies.ToList();
            var patientMessages = from e in emargin.Where(e => e.DId == IDdoctor).ToList() select e;


            return View(patientMessages);
        }
        public IActionResult nightShift(int id)
        {
            ViewBag.VName = new SelectList(_context.Vaccsines, "VaccsineId", "VName");
            ViewBag.Vaccines = _context.Vaccsines.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Answer(int emergencyId, string answer)
        {
            var idNumber = (int)HttpContext.Session.GetInt32("doctor");
            var doct = _context.Doctors.Where(x => x.UserId == idNumber).SingleOrDefault();
            var IDdoctor = (int)doct.DoctorId;
            var emergency = _context.Emergencies.Find(emergencyId);
            if (emergency != null)
            {
                emergency.DId = IDdoctor;   
                emergency.DoctorResponse = answer;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    








    ///////////////////////////////////////////

}
}
