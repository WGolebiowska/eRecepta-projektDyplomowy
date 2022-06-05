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
    public class AppointmentService : BaseService, IAppointmentService
    {
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IEmailSender _mailService;


        public AppointmentService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger, IDoctorService doctorService, IPatientService patientService, IEmailSender mailService) : base(dbContext, mapper, logger)
        {
            _doctorService = doctorService;
            _patientService = patientService;
            _mailService = mailService;
        }

        public AppointmentVm AddOrUpdateAppointment(AddOrUpdateAppointmentVm addOrUpdateAppointmentVm)
        {
            try
            {
                if (addOrUpdateAppointmentVm == null)
                    throw new ArgumentNullException("View model parameter is null");
                var appointmentEntity = Mapper.Map<Appointment>(addOrUpdateAppointmentVm);
                if (!addOrUpdateAppointmentVm.AppointmentId.HasValue || addOrUpdateAppointmentVm.AppointmentId == 0)
                {
                    appointmentEntity.Status = "Niezatwierdzona";
                    DbContext.Appointments.Add(appointmentEntity);
                }
                else
                    DbContext.Appointments.Update(appointmentEntity);
                DbContext.SaveChanges();
                var appointmentVm = Mapper.Map<AppointmentVm>(appointmentEntity);

                var doctorEntity = _doctorService.GetDoctor(d => d.Id == appointmentVm.DoctorId);
                var patientEntity = _patientService.GetPatient(p => p.Id == appointmentVm.PatientId);

                appointmentVm.DoctorName = doctorEntity.Name;
                appointmentVm.DoctorSurname = doctorEntity.Surname;
                appointmentVm.Specialty = doctorEntity.Specialty;
                appointmentVm.FullTitle = doctorEntity.FullTitle;

                appointmentVm.PatientName = patientEntity.Name;
                appointmentVm.PatientSurname = patientEntity.Surname;
                appointmentVm.PatientFullName = patientEntity.FullName;

                if (!addOrUpdateAppointmentVm.AppointmentId.HasValue || addOrUpdateAppointmentVm.AppointmentId == 0)
                {
                    var subject = "eRecepta - zamówienie na eKonsultację";
                    var message = $"Witaj {doctorEntity.FullName},<br>W systemie eRecepta.pl pojawiło się zamówienie na nową eKonsultację:<br><br>";

                    message += $"<b>Data konsultacji: </b>{appointmentVm.AppointmentDate.ToString().Substring(0,16)}<br>";
                    message += $"<b>Typ konsultacji: </b>{appointmentVm.Type}<br>";
                    message += $"<b>Dolegliwość: </b>{addOrUpdateAppointmentVm.Illness}<br>";
                    message += $"<b>Pacjent: </b>{patientEntity.FullName}<br>";
                    message += $"<b>Data urodzenia: </b>{patientEntity.DateOfBirth.Date.ToString().Substring(0, 10)} (wiek: {patientEntity.Age} lat)<br>";
                    message += $"<b>PESEL: </b>{patientEntity.PESEL}<br>";
                    message += $"<b>Nr telefonu: </b>{patientEntity.PhoneNumber ?? "---"}<br>";
                    message += $"<b>Adres email: </b>{patientEntity.Email ?? "---"}<br><br>";
                    message += $"<b>Dodatkowe informacje od pacjenta: </b>{addOrUpdateAppointmentVm.AppointmentNotes ?? "---"}<br>";

                    message += $"<br>Aby zatwierdzić konsultację, zaloguj się w systemie eRecepta.pl, wybierz moduł 'eKonsultacja' i zmień status konsultacji. O zmianie statusu powiadomimy pacjenta wiadomością email.<br>";

                    var mail = _mailService.SendEmailAsync(doctorEntity.Email, subject, message);
                }
                else
                {
                    var subject = "eRecepta - zmiana statusu Twojej eKonsultacji";
                    var message = $"Witaj {patientEntity.FullName},<br>W systemie eRecepta.pl zmienił się status zamówionej eKonsultacji:<br><br>";

                    message += $"<b>Data konsultacji: </b>{appointmentVm.AppointmentDate.ToString().Substring(0, 16)}<br>";
                    message += $"<b>Typ konsultacji: </b>{appointmentVm.Type}<br>";
                    message += $"<b>Lekarz: </b>{doctorEntity.FullTitle}<br>";
                    if(addOrUpdateAppointmentVm.Status == "Odwołana")
                    {
                        message += $"<b>Status: </b><font color=\"red\">{addOrUpdateAppointmentVm.Status ?? "-- -"}</font><br>";
                    }
                    else
                    {
                        message += $"<b>Status: </b>{addOrUpdateAppointmentVm.Status ?? "---"}<br>";
                    }

                    var mail = _mailService.SendEmailAsync(patientEntity.Email, subject, message);
                }

                return appointmentVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public AppointmentVm GetAppointment(Expression<Func<Appointment, bool>> filterExpression)
        {
            try
            {
                if (filterExpression == null)
                    throw new ArgumentNullException("Filter expression parameter is null");
                var appointmentEntity = DbContext.Appointments.AsNoTracking().FirstOrDefault(filterExpression);
                var appointmentVm = Mapper.Map<AppointmentVm>(appointmentEntity);
                var doctorEntity = _doctorService.GetDoctor(d => d.Id == appointmentVm.DoctorId);
                var patientEntity = _patientService.GetPatient(p => p.Id == appointmentVm.PatientId);

                appointmentVm.DoctorName = doctorEntity.Name;
                appointmentVm.DoctorSurname = doctorEntity.Surname;
                appointmentVm.Specialty = doctorEntity.Specialty;
                appointmentVm.FullTitle = doctorEntity.FullTitle;

                appointmentVm.PatientName = patientEntity.Name;
                appointmentVm.PatientSurname = patientEntity.Surname;
                appointmentVm.PatientFullName = patientEntity.FullName;


                return appointmentVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<AppointmentVm> GetAppointments(Expression<Func<Appointment, bool>> filterExpression = null)
        {
            try
            {
                var appointmentsQuery = DbContext.Appointments.AsQueryable().AsNoTracking().OrderBy(a => a.AppointmentDate);
                if (filterExpression != null)
                    appointmentsQuery = (IOrderedQueryable<Appointment>)appointmentsQuery.Where(filterExpression);
                var appointmentsVms = Mapper.Map<IEnumerable<AppointmentVm>>(appointmentsQuery);
                foreach(AppointmentVm appointmentVm in appointmentsVms)
                {
                    var doctorEntity = _doctorService.GetDoctor(d => d.Id == appointmentVm.DoctorId);
                    var patientEntity = _patientService.GetPatient(p => p.Id == appointmentVm.PatientId);

                    appointmentVm.DoctorName = doctorEntity.Name;
                    appointmentVm.DoctorSurname = doctorEntity.Surname;
                    appointmentVm.Specialty = doctorEntity.Specialty;
                    appointmentVm.FullTitle = doctorEntity.FullTitle;

                    appointmentVm.PatientName = patientEntity.Name;
                    appointmentVm.PatientSurname = patientEntity.Surname;
                    appointmentVm.PatientFullName = patientEntity.FullName;

                }
                return appointmentsVms;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public bool DeleteAppointment(Expression<Func<Appointment, bool>> filterExpression)
        {
            try
            {
                if (filterExpression == null)
                    throw new ArgumentNullException("Filter expression parameter is null");
                var appointmentEntity = DbContext.Appointments.FirstOrDefault(filterExpression);
                DbContext.Remove(appointmentEntity);
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
