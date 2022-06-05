using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static eRecepta_projektDyplomowy.Models.Appointment;

namespace eRecepta_projektDyplomowy.ViewModels
{
    public class AddOrUpdateMedicineVm
    {
        public int? MedicineId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Form { get; set; }
        [Required]
        public string Dosage { get; set; }
        [Required]
        public int ReceiptValidPeriod { get; set; }
        [Required]
        public int IllnessId { get; set; }
    }
}
