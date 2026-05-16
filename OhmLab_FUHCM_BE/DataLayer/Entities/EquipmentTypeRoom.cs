using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class EquipmentTypeRoom
    {
        public int EquipmentTypeRoomId { get; set; }
        public string EquipmentTypeId { get; set; } = null!;
        public int RoomId { get; set; }
        public int EquipmentTypeRoomQuantity { get; set; }
        public string KitTemplateStatus { get; set; } = null!;

        public virtual EquipmentType EquipmentType { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
    }
}
