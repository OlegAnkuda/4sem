using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Ankuda.Applicationn.TeamUseCases.Queries.GetAllTeams
{
    public sealed record GetAllTeamsRequest() : IRequest<IEnumerable<TeamEntity>>
    { }
}
