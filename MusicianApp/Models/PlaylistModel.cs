using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicianApp.Models
{
    [BsonIgnoreExtraElements]
    public class PlaylistModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int TracksQuantity { get; set; }
        public string CreatorId { get; set; }
        public string CreatorDisplayName { get; set; }
        public string Status { get; set; }
        public string? ImageUrl { get; set; }
        public List<ListTrackModel>? Tracks { get; set; }

    }
}
