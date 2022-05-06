using System.Collections.Generic;

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