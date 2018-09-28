using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StarWars.Core.Data;
using StarWars.Core.Models;

namespace StarWars.Data.EntityFramework.Repositories
{
    public class PlanetRepository : BaseRepository<Planet, int>, IPlanetRepository
    {
        public PlanetRepository()
        {
        }

        public PlanetRepository(StarWarsContext db, ILogger<PlanetRepository> logger) : base(db, logger)
        {
        }
    }
}