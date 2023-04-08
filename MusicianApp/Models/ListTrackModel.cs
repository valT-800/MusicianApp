using MongoDB.Bson.Serialization.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicianApp.Models
{
    [BsonIgnoreExtraElements]

    public class ListTrackModel
    { 
        public string TrackId { get; set; }
        public string TrackName { get; set; }
        public string? TrackImageUrl { get; set; }
        public List<ListUserModel> TrackArtists { get; set; }
    }
}
