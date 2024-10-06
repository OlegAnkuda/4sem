using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Ankuda.Applicationn.CompetitorUseCases.Commands.UpdateCompetitor
{
    internal class UpdateCompetitorHandler : IRequestHandler<UpdateCompetitorRequest, CompetitorEntity>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCompetitorHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CompetitorEntity> Handle(UpdateCompetitorRequest request, CancellationToken cancellationToken)
        {
            var competitor = await _unitOfWork.CompetitorRepository.GetByIdAsync(request.UpdatedTask.Id, cancellationToken);

            competitor.Name = request.UpdatedTask.Name;
            competitor.Description = request.UpdatedTask.Description;
            competitor.Score = request.UpdatedTask.Score;

            await _unitOfWork.CompetitorRepository.UpdateAsync(competitor, cancellationToken);
            await _unitOfWork.SaveAllAsync();

            return competitor;
        }
    }
}
