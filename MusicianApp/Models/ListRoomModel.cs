using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicianApp.Models
{
    [BsonIgnoreExtraElements]
    public class ListRoomModel
    {
        public string RoomId { get; set; }
        public string RoomName { get; set; }
    }
}
