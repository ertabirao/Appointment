using System.Collections.Generic;
using System.Threading.Tasks;
using dbModel = oa.db.Model;
namespace oa.services.Business
{
    public interface IBusinessService
    {
        dbModel.Business FindByName(string name);
        int Create(dbModel.Business model);
        db.Model.Business Get(int id);
        List<dbModel.Business> GetAll();
        void Update(int Id, dbModel.Business model);
    }
}