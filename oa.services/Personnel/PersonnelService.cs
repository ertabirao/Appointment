using oa.db.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using dbModel = oa.db.Model;

namespace oa.services.Personnel
{
    public class PersonnelService : IPersonnelService
    {
        private readonly IOAContext _db;
        public PersonnelService(IOAContext db)
        {
            _db = db;
        }

        public List<dbModel.Personnel> GetAll(int businessId)
        {
            List<dbModel.Personnel> query = _db.Personnel
                .AsNoTracking()
                .Where(x => x.BusinessId == businessId)
                .ToList();
            return query;
        }

        public dbModel.Personnel Get(int id)
        {
            return _db.Personnel.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
        }

        public bool Create(int businessId, dbModel.Personnel model)
        {
            model.BusinessId = businessId;
            _db.Personnel.Add(model);
            _db.SaveChanges();
            return true;
        }

        public void Update(dbModel.Personnel model)
        {
            _db.SetModified(model);
            _db.SaveChanges();
        }

        public async Task<dbModel.Personnel> FindByCredentials(string emailAddress, string password)
        {
            return await _db.Personnel
                .AsNoTracking()
                .Where(x => x.Email == emailAddress && x.Password == password)
                .FirstOrDefaultAsync();
        }

        public dbModel.Personnel GetByEmail(string emailAddress)
        {
            return _db.Personnel
                .AsNoTracking()
                .Where(x => x.Email == emailAddress)
                .FirstOrDefault();
        }
    }
}
