using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oa.db.Context;
using dbModel = oa.db.Model;

namespace oa.services.Service
{
    public class ServicesService : IServicesService
    {
        private readonly IOAContext _db;
        public ServicesService(IOAContext db)
        {
            _db = db;
        }

        public List<dbModel.Service> GetServices(int businessId)
        {
            List<dbModel.Service> query = _db.Service
                .AsNoTracking()
                .Where(x => x.BusinessId == businessId)
                .ToList();
            return query;
        }

        public dbModel.Service Get(int id)
        {
            return _db.Service
                .AsNoTracking()
                .Where(x => x.Id == id).FirstOrDefault();
        }


        public bool Create(int businessId, dbModel.Service model)
        {
            model.BusinessId = businessId;

            _db.Service.Add(model);
            _db.SaveChanges();
            return true;
        }

        public void Update(int Id, int businessId, dbModel.Service model)
        {
            model.Id = Id;
            model.BusinessId = businessId;
            _db.SetModified(model);
            _db.SaveChanges();
        }



    }
}
