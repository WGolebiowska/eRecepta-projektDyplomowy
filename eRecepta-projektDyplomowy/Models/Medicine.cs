using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRecepta_projektDyplomowy.Models
{
    public class Medicine
    {
        public int MedicineId { get; set; }
        public string Name { get; set; }
        public virtual List<MedicineIllness> MedicinesIllnesses { get; set; }
        public virtual List<PrescriptionEntry> PrescriptionEntries { get; set; }
    }
}
