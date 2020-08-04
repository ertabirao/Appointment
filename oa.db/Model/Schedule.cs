using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oa.db.Model
{
    [Table("tblAppointment")]
    public class Schedule
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int? PersonnelId { get; set; }
        public virtual Personnel Personnel { get; set; }

        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }
        public DateTime DateTime { get; set; }
        public string Note { get; set; }

        public string Status { get; set; }

        public string Comment { get; set; }


    }
}
