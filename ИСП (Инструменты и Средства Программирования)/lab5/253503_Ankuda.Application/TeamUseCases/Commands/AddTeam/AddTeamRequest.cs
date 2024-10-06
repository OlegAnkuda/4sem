using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Ankuda.Applicationn.TeamUseCases.Commands.AddTeam
{
    public sealed record AddTeamRequest(string Name, string Creator) : IRequest<TeamEntity>
    { }
}
