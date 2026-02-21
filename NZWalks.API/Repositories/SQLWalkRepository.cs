using Microsoft.EntityFrameworkCore;
using NZWalks.API.Database;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext _dbContext;
        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Walk> Create(Walk walk)
        {
            await _dbContext.Walks.AddAsync(walk);
            await _dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> Delete(Guid id)
        {
            var walkExists = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (walkExists == null)
                return null;

            _dbContext.Walks.Remove(walkExists);
            await _dbContext.SaveChangesAsync();
            return walkExists;
        }

        public async Task<List<Walk>> GetAllWalks(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool? isAscending = null,
            int pageNo = 1, int pageSize = 1000)
        {
            var walks = _dbContext.Walks.Include("Region").Include("Difficulty").AsQueryable();

            //filtering
            if ((!string.IsNullOrWhiteSpace(filterOn)) && (!string.IsNullOrWhiteSpace(filterQuery)))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            //sorting
            if ((string.IsNullOrWhiteSpace(sortBy) == false) && (isAscending != null))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending == true ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
            }

            //pagination
            var walksSkips = (pageNo - 1) * pageSize;

            return await walks.Skip(walksSkips).Take(pageSize).ToListAsync();

            //return await _dbContext.Walks.Include("Region").Include("Difficulty").ToListAsync();
        }

        public async Task<Walk?> GetWalkById(Guid id)
        {
            var walkDomain = await _dbContext.Walks.Include("Region").Include("Difficulty").FirstOrDefaultAsync(x => x.Id == id);

            if (walkDomain == null)
                return null;

            return walkDomain;
        }

        public async Task<Walk?> Update(Guid id, Walk walk)
        {
            var walkDomainExist = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (walkDomainExist == null)
                return null;

            walkDomainExist.Name = walk.Name;
            walkDomainExist.Description = walk.Description;
            walkDomainExist.LengthInKm = walk.LengthInKm;
            walkDomainExist.WalkImageUrl = walk.WalkImageUrl;
            walkDomainExist.RegionId = walk.RegionId;
            walkDomainExist.DifficultyId = walk.DifficultyId;

            await _dbContext.SaveChangesAsync();
            return walkDomainExist;
        }
    }
}
