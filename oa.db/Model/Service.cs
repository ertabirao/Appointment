using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oa.db.Model
{
    [Table("tblBusinessServices")]
    public class Service
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public bool IsActive { get; set; }

        public int BusinessId { get; set; }
        public virtual Business Business { get; set; }
    }
}
