
namespace MusicianApp.DataAccess
{
  public interface IRoomData
  {
    Task CreateRoom(RoomModel room);
    Task DeleteRoom(string id);
    Task UpdateRoom(RoomModel roomModel);
    Task<List<RoomModel>> GetUserRooms(string userId);
    Task<RoomModel> GetRoomById(string id);

    }
}