using oa.db.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using dbModel = oa.db.Model;

namespace oa.services.Business
{
    public class BusinessService : IBusinessService
    {
        private readonly IOAContext _db;
        public BusinessService(IOAContext db)
        {
            _db = db;
        }

        public List<dbModel.Business> GetAll()
        {
            List<dbModel.Business> business = _db.Business.ToList();
            return business;
        }

        public dbModel.Business Get(int id)
        {
            return _db.Business.Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public int Create(dbModel.Business model)
        {
            _db.Business.Add(model);
            _db.SaveChanges();
            return model.Id;
        }

        public void Update(int Id, dbModel.Business model)
        {
            model.Id = Id;

            _db.SetModified(model);
            _db.SaveChanges();
        }

        public dbModel.Business FindByName(string name)
        {
            return _db.Business.Where(x => x.Name == name)
                .AsNoTracking()
                .FirstOrDefault();
        }
    }
}
