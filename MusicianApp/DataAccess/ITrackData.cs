
namespace MusicianApp.DataAccess
{
  public interface ITrackData
  {
    Task CreateTrack(TrackModel track);
    Task DeleteTrack(string id);
    Task UpdateTrack(TrackModel trackModel);
    Task<List<TrackModel>> GetAllTracks();
    Task<TrackModel> GetTrackById(string id);

    //Task<List<TrackModel>> GetArtistTracks(string id);
  }
}