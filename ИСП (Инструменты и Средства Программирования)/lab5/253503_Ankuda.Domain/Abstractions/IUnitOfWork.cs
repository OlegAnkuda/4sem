using _253503_Ankuda.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Ankuda.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        IRepository<TeamEntity> TeamRepository { get; }
        IRepository<CompetitorEntity> CompetitorRepository { get; }
        public Task SaveAllAsync();
        public Task DeleteDataBaseAsync();
        public Task CreateDataBaseAsync();
    }
}
