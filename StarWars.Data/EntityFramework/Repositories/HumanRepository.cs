using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StarWars.Core.Data;
using StarWars.Core.Models;

namespace StarWars.Data.EntityFramework.Repositories
{
    public class HumanRepository : BaseRepository<Human, int>, IHumanRepository
    {
        public HumanRepository()
        {
        }

        public HumanRepository(StarWarsContext db, ILogger<HumanRepository> logger) : base(db, logger)
        {
        }
    }
}