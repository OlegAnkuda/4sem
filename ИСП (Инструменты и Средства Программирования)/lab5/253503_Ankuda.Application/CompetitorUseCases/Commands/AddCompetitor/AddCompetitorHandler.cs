using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Ankuda.Applicationn.CompetitorUseCases.Commands.AddCompetitor
{
    internal class AddTaskHandler : IRequestHandler<AddCompetitorRequest, CompetitorEntity>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddTaskHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CompetitorEntity> Handle(AddCompetitorRequest request, CancellationToken cancellationToken)
        {
            CompetitorEntity newCompetitor = new CompetitorEntity(request.Name, request.Description);
            if (request.TeamId != null) { newCompetitor.AddToTeam(request.TeamId); }
            await _unitOfWork.CompetitorRepository.AddAsync(newCompetitor, cancellationToken);
            return newCompetitor;
        }
    }
}
