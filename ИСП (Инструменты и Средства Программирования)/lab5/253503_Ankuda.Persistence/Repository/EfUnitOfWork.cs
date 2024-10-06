using _253503_Ankuda.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Ankuda.Persistence.Repository
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly Lazy<IRepository<TeamEntity>> _teamRepository;
        private readonly Lazy<IRepository<CompetitorEntity>> _competitorRepository;

        public EfUnitOfWork(AppDbContext context)
        {
            _context = context;
            _teamRepository = new Lazy<IRepository<TeamEntity>>(() =>
            new EfRepository<TeamEntity>(context));
            _competitorRepository = new Lazy<IRepository<CompetitorEntity>>(() =>
            new EfRepository<CompetitorEntity>(context));
        }
        public IRepository<TeamEntity> TeamRepository
        => _teamRepository.Value;
        public IRepository<CompetitorEntity> CompetitorRepository
         => _competitorRepository.Value;
        public async Task CreateDataBaseAsync() =>
         await _context.Database.EnsureCreatedAsync();
        public async Task DeleteDataBaseAsync() =>
         await _context.Database.EnsureDeletedAsync();
        public async Task SaveAllAsync() =>
         await _context.SaveChangesAsync();
    }
}
