using MongoDB.Bson.Serialization.IdGenerators;

namespace MusicianApp.Models
{
    [BsonIgnoreExtraElements]
    public class ListUserModel
    {
        public string UserId { get; set; }
        public string UserDisplayName { get; set; }

        public ListUserModel(string userId, string userDisplayName)
        {
            UserId = userId;
            UserDisplayName = userDisplayName;
        }
    }
}
