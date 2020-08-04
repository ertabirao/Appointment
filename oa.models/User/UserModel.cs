using System;
using dbModel = oa.db.Model;

namespace oa.models.User
{
    public class UserModel
    {
        public UserModel()
        {

        }

        public UserModel(dbModel.User model)
        {
            Id = model.Id;
            FirstName = model.FirstName;
            LastName = model.LastName;
            Address = model.Address;
            Email = model.Email;
            Password = model.Password;
            ContactNumber = model.ContactNumber;
            JoinedDate = model.JoinedDate;
        }

        public dbModel.User ToDBModel()
        {
            dbModel.User model = new dbModel.User()
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Address = Address,
                Email = Email,
                Password = Password,
                ContactNumber = ContactNumber,
                JoinedDate = DateTime.Now
            };
            return model;
        }

 
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ContactNumber { get; set; }

        public DateTime JoinedDate { get; set; }
    }
}
