using System;
using dbModel = oa.db.Model;

namespace oa.models.User
{
    public class UserListModel
    {
  
        public UserListModel(dbModel.User model)
        {
                Id = model.Id;
                FirstName = model.FirstName;
                LastName = model.LastName;
                Address = model.Address;
                Email = model.Email;
                ContactNumber = model.ContactNumber;
                JoinedDate = model.JoinedDate;
        }



        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string ContactNumber { get; set; }

        public DateTime JoinedDate { get; set; }

    }
}
