
namespace MusicianApp.DataAccess
{
  public interface IPlaylistData
  {
    Task CreatePlaylist(PlaylistModel playlist);
    Task DeletePlaylist(string id);
    Task UpdatePlaylist(PlaylistModel playlistModel);
    Task<List<PlaylistModel>> GetAllPlaylists();
    Task<List<PlaylistModel>> GetUserPlaylists(string userId);
  }
}