using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Ankuda.Applicationn.TeamUseCases.Queries.GetTeamsByCreator
{
    internal class GetProjectsByCreatorHandler(IUnitOfWork unitOfWork) :
        IRequestHandler<GetTeamsByCreatorRequest, IEnumerable<TeamEntity>>
    {
        public async Task<IEnumerable<TeamEntity>> Handle(GetTeamsByCreatorRequest request, CancellationToken cancellationToken)
        {
            return await unitOfWork.TeamRepository.ListAsync(t => t.Creator.Equals(request.creator), cancellationToken);
        }
    }
}
