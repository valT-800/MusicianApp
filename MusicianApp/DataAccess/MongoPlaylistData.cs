using Microsoft.Extensions.Caching.Memory;

namespace MusicianApp.DataAccess
{
    public class MongoPlaylistData : IPlaylistData
    {
        private readonly IMongoCollection<PlaylistModel> _playlists;
        private readonly IMemoryCache _cache;
        private const string CacheName = "PLaylistData";

        public MongoPlaylistData(IDbConnection db, IMemoryCache cache)
        {
            _cache = cache;
            _playlists = db.PlaylistCollection;
        }
        public async Task<List<PlaylistModel>> GetAllPlaylists()
        {

            var output = _cache.Get<List<PlaylistModel>>(CacheName);
            if (output is null)
            {
                var results = await _playlists.FindAsync(_ => true);
                output = results.ToList();

                _cache.Set(CacheName, output, TimeSpan.FromMilliseconds(1));
            }
            return output;
        }
        public async Task<List<PlaylistModel>> GetUserPlaylists(string userId)
        {

            var output = _cache.Get<List<PlaylistModel>>(userId);
            if (output is null)
            {
                var results = await _playlists.FindAsync(room => room.CreatorId == userId);
                output = results.ToList();
                _cache.Set(output, userId, TimeSpan.FromMinutes(1));
            }
            return output;
        }
        public Task CreatePlaylist(PlaylistModel Playlist)
        {
            return _playlists.InsertOneAsync(Playlist);
        }

        public Task DeletePlaylist(string id)
        {
            var deleteFilter = Builders<PlaylistModel>.Filter.Eq(Playlist => Playlist.Id, id);
            return _playlists.DeleteOneAsync(deleteFilter);
        }

        public async Task UpdatePlaylist(PlaylistModel playlistModel)
        {
            var filter = Builders<PlaylistModel>.Filter.Eq(playlist => playlist.Id, playlistModel.Id);
            await _playlists.ReplaceOneAsync(filter, playlistModel);
            _cache.Remove(CacheName);
        }




    }
}
