using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class Lab
    {
        public Lab()
        {
            GradeDescriptions = new HashSet<GradeDescription>();
            LabEquipmentTypes = new HashSet<LabEquipmentType>();
            LabKitTemplates = new HashSet<LabKitTemplate>();
            RegistraionSchedules = new HashSet<RegistraionSchedule>();
            RoomLabs = new HashSet<RoomLab>();
        }

        public int LabId { get; set; }
        public int SubjectId { get; set; }
        public string LabName { get; set; } = null!;
        public int LabNumberOfPractice { get; set; }
        public string LabRequest { get; set; } = null!;
        public string LabTarget { get; set; } = null!;
        public string LabStatus { get; set; } = null!;

        public virtual Subject Subject { get; set; } = null!;
        public virtual ICollection<GradeDescription> GradeDescriptions { get; set; }
        public virtual ICollection<LabEquipmentType> LabEquipmentTypes { get; set; }
        public virtual ICollection<LabKitTemplate> LabKitTemplates { get; set; }
        public virtual ICollection<RegistraionSchedule> RegistraionSchedules { get; set; }
        public virtual ICollection<RoomLab> RoomLabs { get; set; }
    }
}
