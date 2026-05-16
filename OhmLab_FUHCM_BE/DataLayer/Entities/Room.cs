using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class Room
    {
        public Room()
        {
            Equipment = new HashSet<Equipment>();
            EquipmentTypeRooms = new HashSet<EquipmentTypeRoom>();
            KitTemplateRooms = new HashSet<KitTemplateRoom>();
            Kits = new HashSet<Kit>();
            RegistraionSchedules = new HashSet<RegistraionSchedule>();
            RoomLabs = new HashSet<RoomLab>();
        }

        public int RoomId { get; set; }
        public string RoomName { get; set; } = null!;
        public string RoomStatus { get; set; } = null!;

        public virtual ICollection<Equipment> Equipment { get; set; }
        public virtual ICollection<EquipmentTypeRoom> EquipmentTypeRooms { get; set; }
        public virtual ICollection<KitTemplateRoom> KitTemplateRooms { get; set; }
        public virtual ICollection<Kit> Kits { get; set; }
        public virtual ICollection<RegistraionSchedule> RegistraionSchedules { get; set; }
        public virtual ICollection<RoomLab> RoomLabs { get; set; }
    }
}
