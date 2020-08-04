
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dbModel = oa.db.Model;
namespace oa.models.Business
{
    public class BusinessModel
    {
        public BusinessModel()
        {

        }

        public BusinessModel(dbModel.Business model)
        {
            Id = model.Id;
            Name = model.Name;
            Barangay = model.Barangay;
            City = model.City;
            NatureOfBusiness = model.NatureOfBusiness;
            WorkSchedule = model.WorkSchedule;
        }

        public dbModel.Business ToDBModel()
        {
            dbModel.Business model = new dbModel.Business()
            {
                Id = Id,
                Name = Name,
                Barangay = Barangay,
                City = City,
                NatureOfBusiness = NatureOfBusiness,
                WorkSchedule = WorkSchedule
            };
            return model;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Barangay { get; set; }
        public string City { get; set; }
        public string NatureOfBusiness { get; set; }

        public string WorkSchedule { get; set; }
    }
}
