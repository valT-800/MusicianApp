using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicianApp.Models
{
    [BsonIgnoreExtraElements]

    public class ListPlaylistModel
    {
        public string PlaylistId { get; set; }
        public string PlaylistName { get; set; }
        public string? PlaylistImageUrl { get ; set; }

    }
}
