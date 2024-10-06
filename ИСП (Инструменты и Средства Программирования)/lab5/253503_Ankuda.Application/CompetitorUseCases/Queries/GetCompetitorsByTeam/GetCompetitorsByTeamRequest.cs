using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Ankuda.Applicationn.CompetitorUseCases.Queries.GetCompetitorsByTeam
{
    public sealed record GetCompetitorsByTeamRequest(int Id) : IRequest<IEnumerable<CompetitorEntity>>
    { }
}
