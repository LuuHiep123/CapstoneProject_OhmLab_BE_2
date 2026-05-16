using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModel.Room
{
    public class UpdateRoomRequestModel
    {
        public string RoomName { get; set; } = null!;
        public string RoomStatus { get; set; } = null!;
    }
}
