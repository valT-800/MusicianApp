using MongoDB.Bson.Serialization.IdGenerators;

namespace MusicianApp.Models
{
    [BsonIgnoreExtraElements]
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ObjectIdentifier { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }

        public List<BaseInternalAuditModel> InternalAuditModels { get; set; } = new();
        public List<BaseInternalAuditModel> VotedOnAuditModels { get; set; } = new();

        public Favorites? Favorites { get; set; }

        public PlaylistsList? UserPlaylists { get; set; }

        public RoomsList? UserRooms { get; set; }

        public FriendsList? UserFriends { get; set; }

    }
    public class Favorites
    {
        public int TracksQuantity { get; set; } = 0;
        public List<ListTrackModel>? Tracks { get; set; }

    }
    public class PlaylistsList
    {
        public int PlaylistsQuantity { get; set; } = 0;
        public List<ListPlaylistModel>? Playlists { get; set; }

    }
    public class RoomsList
    {
        public int RoomsQuantity { get; set; } = 0;
        public List<ListRoomModel>? Rooms { get; set; }
    }

    public class FriendsList
    {
        public int FriendsQuantity { get; set; } = 0;
        public List<ListUserModel> Friends { get; set; }
    }

}
