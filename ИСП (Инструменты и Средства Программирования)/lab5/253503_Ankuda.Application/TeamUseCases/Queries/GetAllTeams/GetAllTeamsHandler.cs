using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Ankuda.Applicationn.TeamUseCases.Queries.GetAllTeams
{
    internal class GetAllTeamsHandler(IUnitOfWork unitOfWork) :
        IRequestHandler<GetAllTeamsRequest, IEnumerable<TeamEntity>>
    {
        public async Task<IEnumerable<TeamEntity>> Handle(GetAllTeamsRequest request, CancellationToken cancellationToken)
        {
            return await unitOfWork.TeamRepository.ListAllAsync(cancellationToken);
        }
    }
}
