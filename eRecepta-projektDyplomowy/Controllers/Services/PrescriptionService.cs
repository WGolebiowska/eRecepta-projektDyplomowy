using AutoMapper;
using eRecepta_projektDyplomowy.Controllers.Services.Interfaces;
using eRecepta_projektDyplomowy.Data;
using eRecepta_projektDyplomowy.Models;
using eRecepta_projektDyplomowy.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace eRecepta_projektDyplomowy.Controllers.Services
{
    public class PrescriptionService : BaseService, IPrescriptionService
    {
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;

        private static Random random = new Random();
        public static string GeneratePin()
        {
            int length = 4;
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public PrescriptionService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger, IDoctorService doctorService, IPatientService patientService) : base(dbContext, mapper, logger)
        {
            _doctorService = doctorService;
            _patientService = patientService;
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
                var prescriptionVm = Mapper.Map<PrescriptionVm>(prescriptionEntity);

                var patientVm = _patientService.GetPatient(p => p.Id == addOrUpdatePrescriptionVm.PatientId);
                prescriptionVm.Patient = patientVm;
                var doctorVm = _doctorService.GetDoctor(d => d.Id == addOrUpdatePrescriptionVm.DoctorId);
                prescriptionVm.Doctor = doctorVm;

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
                var prescriptionEntity = DbContext.Prescriptions.FirstOrDefault(filterExpression);
                var doctorVm = _doctorService.GetDoctor(d => d.Id == prescriptionEntity.DoctorId);
                var patientVm = _patientService.GetPatient(p => p.Id == prescriptionEntity.PatientId);

                var prescriptionVm = Mapper.Map<PrescriptionVm>(prescriptionEntity);
                prescriptionVm.Doctor = doctorVm;
                prescriptionVm.Patient = patientVm;


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
                var prescriptionsQuery = DbContext.Prescriptions.AsQueryable();
                if (filterExpression != null)
                    prescriptionsQuery = prescriptionsQuery.Where(filterExpression);
                var prescriptionsVms = Mapper.Map<IEnumerable<PrescriptionVm>>(prescriptionsQuery);
                
                foreach(PrescriptionVm prescriptionVm in prescriptionsVms)
                {
                    var prescriptionEntity = Mapper.Map<Prescription>(prescriptionVm);
                    var doctorVm = _doctorService.GetDoctor(d => d.Id == prescriptionEntity.DoctorId);
                    var patientVm = _patientService.GetPatient(p => p.Id == prescriptionEntity.PatientId);
                    prescriptionVm.Doctor = doctorVm;
                    prescriptionVm.Patient = patientVm;
                }
                return prescriptionsVms;
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
