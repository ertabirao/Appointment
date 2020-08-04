using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oa.db.Model
{
    [Table("tblPersonnel")]
    public class Personnel

    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Position { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }


        public int BusinessId { get; set; }
        public virtual Business Business { get; set; }

    }
}
