namespace MusicianApp.Models
{
  public class BaseInternalAuditModel
  {
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Audit { get; set; }
    public BaseInternalAuditModel()
    {

    }

    public BaseInternalAuditModel(InternalAuditModel auditModel)
    {
      Id = auditModel.Id;
      Audit = auditModel.Audit;
    }
  }
}
