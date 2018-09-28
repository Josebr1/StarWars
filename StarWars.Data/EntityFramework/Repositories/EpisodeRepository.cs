using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StarWars.Core.Data;
using StarWars.Core.Models;

namespace StarWars.Data.EntityFramework.Repositories
{
    public class EpisodeRepository : BaseRepository<Episode, int>, IEpisodeRepository
    {
        public EpisodeRepository()
        {
        }

        public EpisodeRepository(StarWarsContext db, ILogger<EpisodeRepository> logger) : base(db, logger)
        {
        }
    }
}