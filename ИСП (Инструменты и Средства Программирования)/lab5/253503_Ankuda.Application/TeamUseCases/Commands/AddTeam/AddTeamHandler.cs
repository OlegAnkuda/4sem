using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Ankuda.Applicationn.TeamUseCases.Commands.AddTeam
{
    internal class AddTeamHandler : IRequestHandler<AddTeamRequest, TeamEntity>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddTeamHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<TeamEntity> Handle(AddTeamRequest request, CancellationToken cancellationToken)
        {
            TeamEntity newTeam = new TeamEntity(request.Name, request.Creator);
            await _unitOfWork.TeamRepository.AddAsync(newTeam, cancellationToken);
            return newTeam;
        }
    }
}
