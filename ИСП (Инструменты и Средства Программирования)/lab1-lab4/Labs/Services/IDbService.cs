using Labs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.Services
{
    public interface IDbService
    {
        IEnumerable<HospitalRoom> GetHospitalRooms();
        IEnumerable<Patient> GetPatients(int id);
        void Init();
    }
}
