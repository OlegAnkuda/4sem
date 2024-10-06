using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.Entities
{
    [Table("Patients")]
    public class Patient
    {
        [PrimaryKey, AutoIncrement, Indexed]
        [Column("Id")]
        public int PatientId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int HospitalRoomId { get; set; }
    }
}
