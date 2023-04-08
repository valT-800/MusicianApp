using MongoDB.Driver;

namespace MusicianApp.DataAccess
{
  public interface IDbConnection
  {
    IMongoCollection<TrackModel> TrackCollection { get; }
    string TrackCollectionName { get; }
    MongoClient Client { get; }
    string DbName { get; }
    IMongoCollection<PlaylistModel> PlaylistCollection { get; }
    string PlaylistCollectionName { get; }

    IMongoCollection<RoomModel> RoomCollection { get; }
    string RoomCollectionName { get; }
    IMongoCollection<UserModel> UserCollection { get; }
    string UserCollectionName { get; }
    IMongoCollection<StatusModel> StatusCollection { get; }
    string StatusCollectionName { get; }
    IMongoCollection<InternalAuditModel> InternalAuditCollection { get; }
    string InternalAuditCollectionName { get; }
    

    }
}