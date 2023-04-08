using Microsoft.Extensions.Caching.Memory;
namespace MusicianApp.DataAccess
{
  public class MongoInternalAuditData : IInternalAuditData
  {
    private readonly IDbConnection _db;
    private readonly IUserData _userData;
    private readonly IMemoryCache _cache;
    private readonly IMongoCollection<InternalAuditModel> _internalAuditModels;
    private const string CacheName = "InternalAuditData";

    public MongoInternalAuditData(IDbConnection db, IUserData userData, IMemoryCache cache)
    {
      _db = db;
      _userData = userData;
      _cache = cache;
      _internalAuditModels = db.InternalAuditCollection;
    }
    public async Task<List<InternalAuditModel>> GetAllInternalModels()
    {
      var output = _cache.Get<List<InternalAuditModel>>(CacheName);
      if (output is null)
      {
        var results = await _internalAuditModels.FindAsync(m => m.Archived == false);
        output = results.ToList();
        _cache.Set(CacheName, output, TimeSpan.FromMinutes(1));
      }
      return output;
    }
    public async Task<List<InternalAuditModel>> GetAllApprovedInternalModels()
    {
      var output = await GetAllInternalModels();
      return output.Where(x => x.ApprovedForRelease).ToList();
    }
    public async Task<InternalAuditModel> GetInternalAuditModel(string id)
    {
      var results = await _internalAuditModels.FindAsync(m => m.Id == id);
      return results.FirstOrDefault();
    }
    public async Task<List<InternalAuditModel>> GetAllInternalModelWaitingForApproval()
    {
      var output = await GetAllInternalModels();
      return output.Where(x => x.ApprovedForRelease == false && x.Rejected == false).ToList();
    }
    public async Task<List<InternalAuditModel>> GetUsersAuditModels(string userId)
    {
      var output = _cache.Get<List<InternalAuditModel>>(userId);
      if(output is null)
      {
        var results = await _internalAuditModels.FindAsync(s => s.Author.Id == userId);
        output = results.ToList();
        _cache.Set(output, userId, TimeSpan.FromMinutes(1));
      }
      return output;
    }
    public async Task UpdateInternalAuditModel(InternalAuditModel internalAuditModel)
    {
      await _internalAuditModels.ReplaceOneAsync(m => m.Id == internalAuditModel.Id, internalAuditModel);
      _cache.Remove(CacheName);
    }
    public async Task UpvoteInternalAudit(string auditId, string userId)
    {
      var client = _db.Client;
      using var session = await client.StartSessionAsync();
      session.StartTransaction();
      try
      {
        var db = client.GetDatabase(_db.DbName);
        var internalAuditTransaction = db.GetCollection<InternalAuditModel>(_db.InternalAuditCollectionName);
        var internalAudit = (await internalAuditTransaction.FindAsync(m => m.Id == auditId)).First();
        bool isUpvote = internalAudit.UserVotes.Add(userId);
        if (isUpvote == false)
        {
          internalAudit.UserVotes.Remove(userId);
        }
        await internalAuditTransaction.ReplaceOneAsync(m => m.Id == auditId, internalAudit);
        var usersInTransactions = db.GetCollection<UserModel>(_db.UserCollectionName);
        var user = await _userData.GetUserByIdAsync(userId);

        if (isUpvote)
        {
          user.VotedOnAuditModels.Add(new BaseInternalAuditModel(internalAudit));
        }
        else
        {
          var votedToRemove = user.VotedOnAuditModels.Where(m => m.Id == auditId).First();
          user.VotedOnAuditModels.Remove(votedToRemove);
        }
        await usersInTransactions.ReplaceOneAsync(m => m.Id == userId, user);
        await session.CommitTransactionAsync();
        _cache.Remove(CacheName);
      }
      catch (Exception ex)
      {
        await session.AbortTransactionAsync();
        throw;
      }
    }
    public async Task CreateInternalAuditModel(InternalAuditModel internalAudit)
    {
      var client = _db.Client;
      using var session = await client.StartSessionAsync();
      session.StartTransaction();
      try
      {
        var db = client.GetDatabase(_db.DbName);
        var internalAuditTransaction = db.GetCollection<InternalAuditModel>(_db.InternalAuditCollectionName);
        await internalAuditTransaction.InsertOneAsync(internalAudit);

        var usersInTransactions = db.GetCollection<UserModel>(_db.UserCollectionName);
        var user = await _userData.GetUserByIdAsync(internalAudit.Author.Id);

        // TODO fix function unknow stuff 
        // https://www.youtube.com/watch?v=J5xlZaiENKw
        user.VotedOnAuditModels.Add(new BaseInternalAuditModel(internalAudit));
        await usersInTransactions.ReplaceOneAsync(m => m.Id == user.Id, user); ;

        await session.CommitTransactionAsync();


      }
      catch (Exception)
      {
        await session.AbortTransactionAsync();
        throw;
      }
    }

  }
}
