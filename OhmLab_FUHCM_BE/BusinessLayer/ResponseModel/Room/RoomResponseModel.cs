using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ResponseModel.Room
{
    public class RoomResponseModel
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; } = null!;
        public string RoomStatus { get; set; } = null!;
    }
}
