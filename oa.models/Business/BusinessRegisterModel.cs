using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dbModel = oa.db.Model;

namespace oa.models.Business
{
    public class BusinessRegisterModel
    {


        public int Id { get; set; }
        public string Name { get; set; }
        public string Barangay { get; set; }
        public string City { get; set; }
        public string NatureOfBusiness { get; set; }
        public string WorkSchedule { get; set; }
        public string PersonnelName { get; set; }
        public string PersonnelAddress { get; set; }
        public string PersonnelEmail { get; set; }
        public string PersonnelPostion { get; set; }
        public string PersonnelPassword { get; set; }

        public dbModel.Business ToBusinessDBModel()
        {
            dbModel.Business model = new dbModel.Business()
            {
                Name = Name,
                Barangay = Barangay,
                City = City,
                NatureOfBusiness = NatureOfBusiness,
                WorkSchedule = WorkSchedule
            };
            return model;
        }


        public dbModel.Personnel ToPersonnelDBModel()
        {
            dbModel.Personnel model = new dbModel.Personnel()
            {
                Name = PersonnelName,
                Address = PersonnelAddress,
                Email = PersonnelEmail,
                Password = PersonnelPassword,
                Position = PersonnelPostion,
                Role = "Admin"
            };
            return model;

        }
    }
}
