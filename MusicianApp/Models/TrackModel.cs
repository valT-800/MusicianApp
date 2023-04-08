using MongoDB.Bson.Serialization.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicianApp.Models
{
    [BsonIgnoreExtraElements]
    public class TrackModel
    { 
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Lyrics { get; set; }
        public string Chords { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string? OriginalTrackId { get; set; }
        public string? ImageUrl { get; set; }
        public List<ListUserModel> Artists { get; set; }

    }
}
