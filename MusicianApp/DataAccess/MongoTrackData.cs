using Microsoft.Extensions.Caching.Memory;

namespace MusicianApp.DataAccess
{
    public class MongoTrackData : ITrackData
    {
        private readonly IMongoCollection<TrackModel> _tracks;
        private readonly IMemoryCache _cache;
        private const string CacheName = "TrackData";

        public MongoTrackData(IDbConnection db, IMemoryCache cache)
        {
            _cache = cache;
            _tracks = db.TrackCollection;
        }
        public async Task<List<TrackModel>> GetAllTracks()
        {

            var output = _cache.Get<List<TrackModel>>(CacheName);
            if (output is null)
            {
                var results = await _tracks.FindAsync(_ => true);
                output = results.ToList();

                _cache.Set(CacheName, output, TimeSpan.FromMilliseconds(1));
            }
            return output;
        }

        /*public async Task<List<TrackModel>> GetArtistTracks(string userId)
        {
          var output = _cache.Get<List<TrackModel>>(userId);
          if(output is null)
          {

            var results = await _tracks.FindAsync(s => s.Artists.Any(artist => artist.Id == userId));

            output = results.ToList();
            _cache.Set(output, userId, TimeSpan.FromMinutes(1));
          }
          return output;
        }*/
        public Task CreateTrack(TrackModel Track)
        {
            return _tracks.InsertOneAsync(Track);
        }

        public Task DeleteTrack(string id)
        {
            var deleteFilter = Builders<TrackModel>.Filter.Eq(Track => Track.Id, id);
            return _tracks.DeleteOneAsync(deleteFilter);
        }

        public async Task UpdateTrack(TrackModel trackModel)
        {
            var filter = Builders<TrackModel>.Filter.Eq(track => track.Id, trackModel.Id);
            await _tracks.ReplaceOneAsync(filter, trackModel);
            _cache.Remove(CacheName);
        }

        public async Task<TrackModel> GetTrackById(string id)
        {
            var result = await _tracks.FindAsync(t => t.Id == id);
            return result.FirstOrDefault();
        }

    }
}
