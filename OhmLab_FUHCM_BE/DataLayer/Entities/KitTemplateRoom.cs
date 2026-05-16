using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class KitTemplateRoom
    {
        public int KitTemplateRoomId { get; set; }
        public string KitTemplateId { get; set; } = null!;
        public int RoomId { get; set; }
        public int KitTemplateRoomQuantity { get; set; }
        public string KitTemplateStatus { get; set; } = null!;

        public virtual KitTemplate KitTemplate { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
    }
}
