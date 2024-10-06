using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Ankuda.Applicationn.CompetitorUseCases.Commands.AddCompetitor
{
    public sealed record AddCompetitorRequest(string Name, string Description, int? TeamId) : IRequest<CompetitorEntity>
    { }
}
