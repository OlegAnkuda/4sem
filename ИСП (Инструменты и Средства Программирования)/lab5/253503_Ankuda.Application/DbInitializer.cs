using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Ankuda.Applicationn
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider services)
        {
            var unitOfWork = services.GetRequiredService<IUnitOfWork>();
            await unitOfWork.DeleteDataBaseAsync();
            await unitOfWork.CreateDataBaseAsync();
            for (int i = 1; i < 10; i++)
            {
                TeamEntity team = new($"Team {i}", $"Creator {i}");
                await unitOfWork.TeamRepository.AddAsync(team);
            }
            await unitOfWork.SaveAllAsync();
            Random random = new Random();
            for (int i = 1; i < 10; i++)
            {
                for (int j = 1; j < 5; j++)
                {
                    CompetitorEntity competitor = new($"Competitor {j}", $"Description {j}");
                    competitor.Score = random.Next(1, 101);
                    competitor.AddToTeam(i);
                    await unitOfWork.CompetitorRepository.AddAsync(competitor);
                }
            }
            await unitOfWork.SaveAllAsync();
        }
    }
}
