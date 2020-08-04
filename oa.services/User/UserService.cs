using oa.db.Context;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using dbModel = oa.db.Model;

namespace oa.services.User
{

    public class UserService : IUserService
    {
        private readonly IOAContext _db;

        public UserService(IOAContext db)
        {
            _db = db;
        }

        public dbModel.User Get(int id)
        {
            return _db.User.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool Create(dbModel.User model)
        {
            model.JoinedDate = DateTime.Now;
            _db.User.Add(model);
            _db.SaveChanges();
            return true;
        }

        public bool Update(int id, dbModel.User model)
        {
            model.Id = id;
            _db.SetModified(model);
            _db.SaveChanges();
            return true;
        }
        public dbModel.User GetByEmail(string email)
        {
            return _db.User.AsNoTracking().Where(x => x.Email == email).FirstOrDefault();
        }

        public async Task<dbModel.User> FindByCredentials(string emailAddress
            , string password)
        {
          return await _db.User
                .Where(x => x.Email == emailAddress && x.Password == password)
                .FirstOrDefaultAsync();
        }

        public string Role()
        {
            return "User";
        }
    }
}
