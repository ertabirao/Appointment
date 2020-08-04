using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dbModel = oa.db.Model;

namespace oa.models.Personnel
{
    public class PersonnelListModel
    {
        public PersonnelListModel(dbModel.Personnel model)
        {
         
                Id = model.Id;
                Name = model.Name;
                Address = model.Address;
                Email = model.Email;
                Position = model.Position;
                IsActive = model.IsActive;
                Role = model.Role;

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }


   
    }

}
