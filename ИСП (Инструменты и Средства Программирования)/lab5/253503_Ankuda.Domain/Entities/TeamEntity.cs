using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Ankuda.Domain.Entities
{
    public class TeamEntity : Entity
    {
        private TeamEntity() { }
        public TeamEntity(string name, string creator)
        {
            Name = name;
            Creator = creator;
        }
        public string Name { get; set; } = "Default_Name";
        public string Creator { get; private set; } = "Default_Creator";
        private List<CompetitorEntity> _competitors { get; set; } = [];
        public List<CompetitorEntity> Competitors { get => _competitors; }
    }
}
