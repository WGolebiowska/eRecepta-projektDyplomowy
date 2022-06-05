using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static eRecepta_projektDyplomowy.Models.Appointment;

namespace eRecepta_projektDyplomowy.ViewModels
{
    public class AddOrUpdateIllnessVm
    {
        public int? IllnessId { get; set; }
        [Required]
        public string Name { get; set; }
        //public List<MedicineVm> Medicines { get; set; }
    }
}
