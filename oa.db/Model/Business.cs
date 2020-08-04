using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oa.db.Model
{
    
    [Table("tblBusiness")]
    public class Business
   
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barangay { get; set; }
        public string City { get; set; }
        public string NatureOfBusiness { get; set; }

        public string WorkSchedule { get; set; }

        public virtual ICollection<Personnel> Personnel { get; set; }
        public virtual ICollection<Service> Service { get; set; }

    }

}
