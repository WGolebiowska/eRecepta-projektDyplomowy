using AutoMapper;
using eRecepta_projektDyplomowy.Controllers.Services.Interfaces;
using eRecepta_projektDyplomowy.Data;
using eRecepta_projektDyplomowy.Models;
using eRecepta_projektDyplomowy.ViewModels;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace eRecepta_projektDyplomowy.Controllers.Services
{
    public class PrescriptionService : BaseService, IPrescriptionService
    {
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IMedicineService _medicineService;
        private readonly IEmailSender _mailService;

        private static Random random = new Random();
        public static string GeneratePin()
        {
            int length = 4;
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public PrescriptionService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger, IDoctorService doctorService, IPatientService patientService, IMedicineService medicineService, IEmailSender mailService) : base(dbContext, mapper, logger)
        {
            _doctorService = doctorService;
            _patientService = patientService;
            _medicineService = medicineService;
            _mailService = mailService;
        }

        public PrescriptionVm AddOrUpdatePrescription(AddOrUpdatePrescriptionVm addOrUpdatePrescriptionVm)
        {
            try
            {
                if (addOrUpdatePrescriptionVm == null)
                    throw new ArgumentNullException("View model parameter is null");
                var prescriptionEntity = Mapper.Map<Prescription>(addOrUpdatePrescriptionVm);
                if (addOrUpdatePrescriptionVm.PrescriptionId == null || addOrUpdatePrescriptionVm.PrescriptionId == 0)
                {
                    prescriptionEntity.PinCode = GeneratePin();

                    DbContext.Prescriptions.Add(prescriptionEntity);
                }
                else
                    DbContext.Prescriptions.Update(prescriptionEntity);
                DbContext.SaveChanges();

                prescriptionEntity.Medicine = Mapper.Map<Medicine>(_medicineService.GetMedicine(m => m.MedicineId == prescriptionEntity.MedicineId));
                var prescriptionVm = Mapper.Map<PrescriptionVm>(prescriptionEntity);

                var patientVm = _patientService.GetPatient(p => p.Id == addOrUpdatePrescriptionVm.PatientId);
                prescriptionVm.Patient = patientVm;
                var doctorVm = _doctorService.GetDoctor(d => d.Id == addOrUpdatePrescriptionVm.DoctorId);
                prescriptionVm.Doctor = doctorVm;

                var subject = "eRecepta - wystawiono nową receptę";
                var message = $"Witaj {patientVm.FullName}<br>Dla Twojego konta w systemie eRecepta.pl została wystawiona nowa eRecepta:<br><br>";
                message += $"<b>Wystawiona przez: </b>{doctorVm.FullTitle}<br>";
                message += $"<b>Dla: </b>{patientVm.FullName}, <b>PESEL</b>: {patientVm.PESEL}<br>";
                message += $"<b>Data wystawienia: </b>{prescriptionVm.IssueDate.Date.ToString().Substring(0,10)}<br>";
                message += $"<b>Ważna do: </b>{prescriptionVm.ExpiryDate.Date.ToString().Substring(0, 10)} ({prescriptionVm.ValidPeriod} dni)<br>";
                message += $"<b>Lek: </b>{prescriptionVm.Medicine.Name} - {prescriptionVm.Medicine.Form}<br>";
                message += $"<b>Dawkowanie: </b>{addOrUpdatePrescriptionVm.PrescribedDosage}<br>";
                message += $"<b>Dodatkowe informacje od lekarza: </b>{prescriptionVm.PrescriptionNotes ?? "---"}<br>";
                message += $"<b>Kod PIN do odbioru eRecepty: </b>{prescriptionVm.PinCode}<br>";
                message += $"<br>Aby wykupić eReceptę, wybierz się do dowolnej apteki, podaj farmaceucie swój numer PESEL i kod PIN. Obecnie każda apteka w Polsce ma obowiązek obsłużenia eRecepty.<br>";

                var mail = _mailService.SendEmailAsync(prescriptionVm.Patient.Email, subject, message);
                return prescriptionVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public PrescriptionVm GetPrescription(Expression<Func<Prescription, bool>> filterExpression)
        {
            try
            {
                if (filterExpression == null)
                    throw new ArgumentNullException("Filter expression parameter is null");
                var prescriptionEntity = DbContext.Prescriptions.AsNoTracking().FirstOrDefault(filterExpression);
                prescriptionEntity.Medicine = Mapper.Map<Medicine>(_medicineService.GetMedicine(m => m.MedicineId == prescriptionEntity.MedicineId));
                var doctorVm = _doctorService.GetDoctor(d => d.Id == prescriptionEntity.DoctorId);
                var patientVm = _patientService.GetPatient(p => p.Id == prescriptionEntity.PatientId);

                var prescriptionVm = Mapper.Map<PrescriptionVm>(prescriptionEntity);
                prescriptionVm.Doctor = doctorVm;
                prescriptionVm.Patient = patientVm;
                prescriptionVm.ValidPeriod = prescriptionVm.Medicine.ReceiptValidPeriod;
                prescriptionVm.ExpiryDate = prescriptionVm.IssueDate.AddDays(prescriptionVm.ValidPeriod);

                return prescriptionVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<PrescriptionVm> GetPrescriptions(Expression<Func<Prescription, bool>> filterExpression = null)
        {
            try
            {
                var prescriptionsQuery = DbContext.Prescriptions.AsQueryable().AsNoTracking();
                if (filterExpression != null)
                    prescriptionsQuery = (IOrderedQueryable<Prescription>)prescriptionsQuery.Where(filterExpression);

                foreach(Prescription prescription in prescriptionsQuery)
                {
                    prescription.Medicine = Mapper.Map<Medicine>(_medicineService.GetMedicine(m => m.MedicineId == prescription.MedicineId));
                }

                var prescriptionsVms = Mapper.Map<IEnumerable<PrescriptionVm>>(prescriptionsQuery);
                
                foreach(PrescriptionVm prescriptionVm in prescriptionsVms)
                {
                    var doctorVm = _doctorService.GetDoctor(d => d.Id == prescriptionVm.DoctorId);
                    var patientVm = _patientService.GetPatient(d => d.Id == prescriptionVm.PatientId);
                    var medicineVm = _medicineService.GetMedicine(d => d.MedicineId == prescriptionVm.MedicineId);
                    prescriptionVm.Doctor = doctorVm;
                    prescriptionVm.Patient = patientVm;
                    prescriptionVm.Medicine = medicineVm;
                    prescriptionVm.ValidPeriod = medicineVm.ReceiptValidPeriod;
                    prescriptionVm.ExpiryDate = prescriptionVm.IssueDate.AddDays(prescriptionVm.ValidPeriod);

                }
                var ordered = prescriptionsVms.OrderBy(p => p.ExpiryDate);
                return ordered;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public bool DeletePrescription(Expression<Func<Prescription, bool>> filterExpression)
        {
            try
            {
                if (filterExpression == null)
                    throw new ArgumentNullException("Filter expression parameter is null");
                var prescriptionEntity = DbContext.Prescriptions.FirstOrDefault(filterExpression);
                DbContext.Remove(prescriptionEntity);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
