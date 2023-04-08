
namespace MusicianApp.DataAccess
{
  public interface IInternalAuditData
  {
    Task CreateInternalAuditModel(InternalAuditModel internalAudit);
    Task<List<InternalAuditModel>> GetAllApprovedInternalModels();
    Task<List<InternalAuditModel>> GetAllInternalModels();
    Task<List<InternalAuditModel>> GetAllInternalModelWaitingForApproval();
    Task<InternalAuditModel> GetInternalAuditModel(string id);
    Task UpdateInternalAuditModel(InternalAuditModel internalAuditModel);
    Task UpvoteInternalAudit(string auditId, string userId);
  }
}