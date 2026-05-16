using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class RoomLab
    {
        public int RoomLabId { get; set; }
        public int LabId { get; set; }
        public int RoomId { get; set; }
        public string RoomLabIdStatus { get; set; } = null!;

        public virtual Lab Lab { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
    }
}
