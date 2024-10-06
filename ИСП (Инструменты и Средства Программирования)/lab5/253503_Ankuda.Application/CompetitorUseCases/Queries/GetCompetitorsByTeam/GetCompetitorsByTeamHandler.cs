using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Ankuda.Applicationn.CompetitorUseCases.Queries.GetCompetitorsByTeam
{
    internal class GetTaskByProjectHandler(IUnitOfWork unitOfWork) :
        IRequestHandler<GetCompetitorsByTeamRequest, IEnumerable<CompetitorEntity>>
    {
        public async Task<IEnumerable<CompetitorEntity>> Handle(GetCompetitorsByTeamRequest request, CancellationToken cancellationToken)
        {
            return await unitOfWork.CompetitorRepository.ListAsync(t => t.TeamId.Equals(request.Id), cancellationToken);

        }
    }
}
