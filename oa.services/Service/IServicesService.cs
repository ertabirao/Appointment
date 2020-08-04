using System.Collections.Generic;

namespace oa.services.Service
{
    public interface IServicesService
    {
        bool Create(int businessId, db.Model.Service model);
        db.Model.Service Get(int id);
        List<db.Model.Service> GetServices(int businessId);
        void Update(int Id, int businessId, db.Model.Service model);
    }
}