namespace MusicianApp.Models
{
  public class InternalAuditModel
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Description { get; set; }
    public string Audit { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public TrackModel Track { get; set; }
    public BaseUserModel Author { get; set; }
    public HashSet<string> UserVotes { get; set; } = new();
    public StatusModel CurrentStatus { get; set; }
    public string OwnerNotes { get; set; }
    public bool ApprovedForRelease { get; set; } = false;
    public bool Archived { get; set; } = false;
    public bool Rejected { get; set; } = false;
  }
}
