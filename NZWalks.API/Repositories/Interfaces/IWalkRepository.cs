using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Interfaces
{
    public interface IWalkRepository
    {
        Task<Walk> Create(Walk walk);
        Task<List<Walk>> GetAllWalks(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool? isAscending = null,
            int pageNo = 1, int pageSize = 1000);
        Task<Walk> GetWalkById(Guid id);
        Task<Walk> Update(Guid id, Walk walk);
        Task<Walk> Delete(Guid id);
    }
}
