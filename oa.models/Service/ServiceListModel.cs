using oa.models.Business;
using dbModel = oa.db.Model;

namespace oa.models.Service
{
    public class ServiceListModel
    {
        public ServiceListModel(dbModel.Service model)
        {
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
            Price = model.Price;
            IsActive = model.IsActive;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public bool IsActive { get; set; }


    }
}
