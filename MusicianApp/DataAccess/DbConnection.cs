using Microsoft.Extensions.Configuration;
namespace MusicianApp.DataAccess
{

  public class DbConnection : IDbConnection
  {
    private readonly IConfiguration _config;
    private readonly IMongoDatabase _db;
    private string _connectionId = "MongoDB";
    public string DbName { get; private set; }
    public string TrackCollectionName { get; private set; } = "tracks";
    public string PlaylistCollectionName { get; private set; } = "playlists";
    public string RoomCollectionName { get; private set; } = "rooms";
    public string UserCollectionName { get; private set; } = "users";
    public string StatusCollectionName { get; private set; } = "statuses";
    public string InternalAuditCollectionName { get; private set; } = "internal_audits";
        
    public MongoClient Client { get; private set; }

    public IMongoCollection<TrackModel> TrackCollection { get; private set; }
    public IMongoCollection<PlaylistModel> PlaylistCollection { get; private set; }
    public IMongoCollection<RoomModel> RoomCollection { get; private set; }
    public IMongoCollection<UserModel> UserCollection { get; private set; }
    public IMongoCollection<StatusModel> StatusCollection { get; private set; }
    public IMongoCollection<InternalAuditModel> InternalAuditCollection { get; private set; }


    // Singleton for connection service
    public DbConnection(IConfiguration config)
    {
      _config = config;
      Client = new MongoClient(_config.GetConnectionString(_connectionId));
      DbName = _config["DatabaseName"];
      _db = Client.GetDatabase(DbName);

      // Init Collections for mongo
      TrackCollection = _db.GetCollection<TrackModel>(TrackCollectionName);
      PlaylistCollection = _db.GetCollection<PlaylistModel>(PlaylistCollectionName);
      RoomCollection = _db.GetCollection<RoomModel>(RoomCollectionName);
      UserCollection = _db.GetCollection<UserModel>(UserCollectionName);
      StatusCollection = _db.GetCollection<StatusModel>(StatusCollectionName);
      InternalAuditCollection = _db.GetCollection<InternalAuditModel>(InternalAuditCollectionName);
    }
  }
}
