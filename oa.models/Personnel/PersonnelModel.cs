using dbModel = oa.db.Model;

namespace oa.models.Personnel
{
    public class PersonnelModel
    {

        public dbModel.Personnel ToDBModel()
        {
            dbModel.Personnel model = new dbModel.Personnel()
            {
                Id = Id,
                Name = Name,
                Address = Address,
                Email = Email,
                Password = Password,
                Position = Position,
                IsActive = IsActive,
                Role = Role == "Admin" ? "Admin" : "Personnel",
                BusinessId = BusinessId
            };
            return model;

        }

        public dbModel.Personnel ToUpdateDBModel(dbModel.Personnel personnel)
        {
            dbModel.Personnel model = new dbModel.Personnel()
            {
                Id = personnel.Id,
                Name = Name == null ? personnel.Name : Name,
                Address = Address == null ? personnel.Address : Address,
                Email = personnel.Email,
                Password = Password == null ? personnel.Password : Password,
                Position = Position == null ? personnel.Position : Position,
                IsActive = IsActive == false ? personnel.IsActive : IsActive,
                Role = personnel.Role,
                BusinessId = personnel.BusinessId
            };
            return model;

        }

        public int Id { get; set; }
 
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Position { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }
        public int BusinessId { get; set; }

    }
}
