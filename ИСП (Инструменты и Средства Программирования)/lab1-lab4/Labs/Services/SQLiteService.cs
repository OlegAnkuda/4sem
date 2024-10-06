using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Labs.Entities;

namespace Labs.Services
{
    public class SQLiteService : IDbService
    {
        private readonly string databaseFilename = "HospitalRoomsDB.db";
        private SQLiteConnection _db;
        public IEnumerable<Patient> GetPatients(int id)
        {
            return _db.Table<Patient>().Where(a => a.HospitalRoomId == id).ToList();
        }

        public IEnumerable<HospitalRoom> GetHospitalRooms()
        {
            return _db.Table<HospitalRoom>().ToList();
        }

        public void Init()
        {

            SQLiteOpenFlags Flags =
                SQLite.SQLiteOpenFlags.ReadWrite |
                SQLite.SQLiteOpenFlags.Create;
            string dbPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), databaseFilename);
            _db = new SQLiteConnection(dbPath, Flags);

            try { _db.Table<HospitalRoom>().Any(); }
            catch
            {
                _db.CreateTable<HospitalRoom>();
                _db.CreateTable<Patient>();

                var hospitalRooms = new List<HospitalRoom>
                {
                new() { Id = 1, Name = "Room 1", Description = "Description 1" },
                new() { Id = 2, Name = "Room 2", Description = "Description 2" },
                new() { Id = 3, Name = "Room 3", Description = "Description 3" },
                new() { Id = 4, Name = "Room 4", Description = "Description 4" }
                };
                _db.InsertAll(hospitalRooms);

                var patients = new List<Patient>();
                foreach (var room in hospitalRooms)
                {
                    for (int i = 1; i <= 7; i++)
                    {
                        patients.Add(new Patient
                        {
                            PatientId = i,
                            Name = $"Patient {i} from {room.Name}",
                            Description = $"Description {i} for {room.Name}",
                            HospitalRoomId = room.Id
                        });
                    }
                }
                _db.InsertAll(patients);
            }

        }
    }
}
