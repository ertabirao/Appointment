using oa.models.Business;
using dbModel = oa.db.Model;

namespace oa.models.Service
{
    public class ServiceModel
    {
        public ServiceModel()
        {

        }
        public ServiceModel(dbModel.Service model)
        {
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
            Price = model.Price;
            IsActive = model.IsActive;
            Business = new BusinessModel(model.Business);
        }

        public dbModel.Service ToDbModel()
        {
            dbModel.Service model = new dbModel.Service()
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Price = Price,
                IsActive = IsActive
            };

            return model;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public bool IsActive { get; set; }

        public BusinessModel Business { get; set; }

    }
}
