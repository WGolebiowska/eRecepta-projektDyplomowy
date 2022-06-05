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
using System.Linq;
using System.Linq.Expressions;

namespace eRecepta_projektDyplomowy.Controllers.Services
{
    public class PrescriptionFormService : BaseService, IPrescriptionFormService
    {
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IIllnessService _illnessService;
        private readonly IMedicineService _medicineService;
        private readonly IEmailSender _mailService;

        public PrescriptionFormService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger, IDoctorService doctorService, IPatientService patientService, IEmailSender mailService, IMedicineService medicineService, IIllnessService illnessService) : base(dbContext, mapper, logger)
        {
            _doctorService = doctorService;
            _patientService = patientService;
            _medicineService = medicineService;
            _illnessService = illnessService;

            _mailService = mailService;
        }

        public PrescriptionFormVm AddOrUpdatePrescriptionForm(AddOrUpdatePrescriptionFormVm addOrUpdatePrescriptionFormVm)
        {
            try
            {
                if (addOrUpdatePrescriptionFormVm == null)
                    throw new ArgumentNullException("View model parameter is null");
                var prescriptionFormEntity = Mapper.Map<PrescriptionForm>(addOrUpdatePrescriptionFormVm);
                if (addOrUpdatePrescriptionFormVm.PrescriptionFormId == null || addOrUpdatePrescriptionFormVm.PrescriptionFormId == 0)
                {
                    DbContext.PrescriptionForms.Add(prescriptionFormEntity);
                }
                else
                    DbContext.PrescriptionForms.Update(prescriptionFormEntity);
                DbContext.SaveChanges();
                var prescriptionFormVm = Mapper.Map<PrescriptionFormVm>(prescriptionFormEntity);

                var patientVm = _patientService.GetPatient(p => p.Id == addOrUpdatePrescriptionFormVm.PatientId);
                prescriptionFormVm.Patient = patientVm;
                var illnessVm = _illnessService.GetIllness(i => i.IllnessId == addOrUpdatePrescriptionFormVm.IllnessId);
                prescriptionFormVm.Illness = illnessVm;
                if(prescriptionFormVm.MedicineId != null || prescriptionFormVm.MedicineId != 0)
                {
                    var medicineVm = _medicineService.GetMedicine(m => m.MedicineId == addOrUpdatePrescriptionFormVm.MedicineId);
                    prescriptionFormVm.Medicine = medicineVm;
                }
                var doctorVms = _doctorService.GetDoctors();
                
                foreach(DoctorVm doctor in doctorVms)
                {
                    var subject = "eRecepta - zamówienie na nową receptę";
                    var message = $"Witaj {doctor.FullName},<br>W systemie eRecepta.pl pojawiło się zamówienie na nową eReceptę:<br><br>";

                    message += $"<b>Pacjent: </b>{patientVm.FullName}<br>";
                    message += $"<b>Płeć: </b>{prescriptionFormVm.Gender}<br>";
                    message += $"<b>Data urodzenia: </b>{patientVm.DateOfBirth.Date.ToString().Substring(0, 10)} (wiek: {patientVm.Age} lat)<br>";
                    message += $"<b>PESEL: </b>{patientVm.PESEL}<br>";
                    message += $"<b>Nr telefonu: </b>{patientVm.PhoneNumber ?? "---"}<br>";
                    message += $"<b>Adres email: </b>{patientVm.Email?? "---"}<br><br>";

                    message += $"<b>Dolegliwość: </b>{prescriptionFormVm.Illness.Name}<br>";
                    if(prescriptionFormVm.Medicine == null)
                    {
                        message += $"<b>Zamówiony lek: </b>Pacjent poprosił lekarza o wybór leku<br>";
                    }
                    else
                    {
                        message += $"<b>Zamówiony lek: </b>{prescriptionFormVm.Medicine.Name} - {prescriptionFormVm.Medicine.Form}<br>";
                        message += $"<b>Sugerowane dawkowanie: </b>{prescriptionFormVm.Medicine.Dosage} - {prescriptionFormVm.Medicine.Form}<br>";
                    }
                    message += $"<br><b>Informacje o stanie zdrowia pacjenta:</b><br><br>";
                    message += $"<b>Data przeprowadzenia ankiety: </b>{prescriptionFormVm.OrderDate}<br>";
                    message += $"<b>Aktualna temperatura ciała: </b>{prescriptionFormVm.BodyTemp} st. C<br>";
                    message += $"<b>Wzrost: </b>{prescriptionFormVm.Height}<br>";
                    message += $"<b>Waga: </b>{prescriptionFormVm.Weight}<br>";
                    if (prescriptionFormVm.IsPregnant)
                    {
                        message += $"<b>Jest w ciąży: </b>TAK<b><br>";
                    }
                    message += $"<b>Używki: </b>{prescriptionFormVm.Addictions ?? "---"}<br>";
                    message += $"<b>Uczulenia: </b>{prescriptionFormVm.Allergies ?? "---"}<br>";
                    message += $"<b>Leki przyjmowane na stałe: </b>{prescriptionFormVm.PermMedicines ?? "---"}<br>";
                    message += $"<b>Choroby przewlekłe: </b>{prescriptionFormVm.ChronicIllnesses ?? "---"}<br>";
                    message += $"<b>Dodatkowe informacje od pacjenta: </b>{prescriptionFormVm.AdditionalInfo ?? "---"}<br>";

                    message += $"<br>Aby wystawić eReceptę, zaloguj się w systemie eRecepta.pl, wybierz moduł 'eRecepta' i wprowadź niezbędne dane. O wystawieniu eRecepty powiadomimy pacjenta wiadomością email.<br>";

                var mail = _mailService.SendEmailAsync(doctor.Email, subject, message);
                }

                return prescriptionFormVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
        
        public PrescriptionFormVm GetPrescriptionForm(Expression<Func<PrescriptionForm, bool>> filterExpression)
        {
            try
            {
                if (filterExpression == null)
                    throw new ArgumentNullException("Filter expression parameter is null");
                var prescriptionFormEntity = DbContext.PrescriptionForms.AsNoTracking().FirstOrDefault(filterExpression);
                var patientVm = _patientService.GetPatient(p => p.Id == prescriptionFormEntity.PatientId);
                var illnessVm = _illnessService.GetIllness(i => i.IllnessId == prescriptionFormEntity.MedicineId);
                var medicineVm = _medicineService.GetMedicine(m => m.MedicineId == prescriptionFormEntity.MedicineId);

                var prescriptionFormVm = Mapper.Map<PrescriptionFormVm>(prescriptionFormEntity);
                prescriptionFormVm.Patient = patientVm;
                prescriptionFormVm.Illness = illnessVm;
                prescriptionFormVm.Medicine = medicineVm;

                return prescriptionFormVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<PrescriptionFormVm> GetPrescriptionForms(Expression<Func<PrescriptionForm, bool>> filterExpression = null)
        {
            try
            {
                var prescriptionFormFormsQuery = DbContext.PrescriptionForms.AsQueryable().AsNoTracking();
                if (filterExpression != null)
                    prescriptionFormFormsQuery = prescriptionFormFormsQuery.Where(filterExpression);
                var prescriptionFormFormsVms = Mapper.Map<IEnumerable<PrescriptionFormVm>>(prescriptionFormFormsQuery).OrderBy(p => p.OrderDate);

                foreach (PrescriptionFormVm prescriptionFormVm in prescriptionFormFormsVms)
                {
                    var prescriptionFormEntity = Mapper.Map<PrescriptionForm>(prescriptionFormVm);
                    var patientVm = _patientService.GetPatient(p => p.Id == prescriptionFormEntity.PatientId);
                    var illnessVm = _illnessService.GetIllness(i => i.IllnessId == prescriptionFormEntity.MedicineId);
                    var medicineVm = _medicineService.GetMedicine(m => m.MedicineId == prescriptionFormEntity.MedicineId);
                    prescriptionFormVm.Patient = patientVm;
                    prescriptionFormVm.Illness = illnessVm;
                    prescriptionFormVm.Medicine = medicineVm;
                }
                return prescriptionFormFormsVms;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public bool DeletePrescriptionForm(Expression<Func<PrescriptionForm, bool>> filterExpression)
        {
            try
            {
                if (filterExpression == null)
                    throw new ArgumentNullException("Filter expression parameter is null");
                var prescriptionFormEntity = DbContext.PrescriptionForms.FirstOrDefault(filterExpression);
                DbContext.Remove(prescriptionFormEntity);
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
