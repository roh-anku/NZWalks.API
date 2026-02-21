using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Interfaces
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllRegionsAsync();
        Task<Region?> GetRegionById(Guid id);
        Task<Region> AddRegionAsync(Region region);
        Task<Region?> UpdateRegionAsync(Guid id, Region region);
        Task<Region?> DeleteAsync(Guid id);

    }
}
