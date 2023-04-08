using Microsoft.Extensions.Caching.Memory;

namespace MusicianApp.DataAccess
{
    public class MongoRoomData : IRoomData
    {
        private readonly IMongoCollection<RoomModel> _rooms;
        private readonly IMemoryCache _cache;
        private const string CacheName = "RoomData";

        public MongoRoomData(IDbConnection db, IMemoryCache cache)
        {
            _cache = cache;
            _rooms = db.RoomCollection;
        }
        public async Task<List<RoomModel>> GetUserRooms(string userId)
        {

            var output = _cache.Get<List<RoomModel>>(CacheName);
            if (output is null)
            {
                var results = await _rooms.FindAsync(room => room.CreatorId == userId);
                output = results.ToList();

                _cache.Set(CacheName, output, TimeSpan.FromMilliseconds(1));
            }
            return output;
        }
        public Task CreateRoom(RoomModel Room)
        {
            return _rooms.InsertOneAsync(Room);
        }

        public Task DeleteRoom(string id)
        {
            var deleteFilter = Builders<RoomModel>.Filter.Eq(Room => Room.Id, id);
            return _rooms.DeleteOneAsync(deleteFilter);
        }

        public async Task UpdateRoom(RoomModel roomModel)
        {
            var filter = Builders<RoomModel>.Filter.Eq(room => room.Id, roomModel.Id);
            await _rooms.ReplaceOneAsync(filter, roomModel);
            _cache.Remove(CacheName);
        }

        public async Task<RoomModel> GetRoomById(string id)
        {
            var result = await _rooms.FindAsync(room => room.Id == id);
            return result.FirstOrDefault();
        }


    }
}
