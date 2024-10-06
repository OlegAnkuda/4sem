using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Ankuda.Domain.Entities
{
    public class CompetitorEntity : Entity
    {
        private CompetitorEntity() { }
        public CompetitorEntity(string name, string description)
        {
            Name = name;
            Score = 0;
            Description = description;
            Created = DateTime.Now;
        }
        public string Name { get; set; } = "Default_Name";
        public int Score { get; set; } = 0;
        public string Description { get; set; } = "Default_Description";
        public DateTime Created { get; private set; } = DateTime.Now;
        public int? TeamId { get; private set; }
        public TeamEntity Team { get; set; }
        public void AddToTeam(int? teamId)
        {
            if (teamId < 0) return;
            this.TeamId = teamId;
        }
    }
}
