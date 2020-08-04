using dbModel = oa.db.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace oa.services.Personnel
{
    public interface IPersonnelService
    {
  
        bool Create(int businessId, dbModel.Personnel model);
        dbModel.Personnel Get(int id);
        List<dbModel.Personnel> GetAll(int businessId);
        void Update(dbModel.Personnel model);

        Task<dbModel.Personnel> FindByCredentials(string emailAddress, string password);
        dbModel.Personnel GetByEmail(string emailAddress);
    }
}