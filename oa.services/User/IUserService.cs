
using System.Threading.Tasks;
using dbModel = oa.db.Model;
namespace oa.services.User
{
    public interface IUserService
    {
        bool Create(dbModel.User model);
        dbModel.User Get(int id);
        dbModel.User GetByEmail(string email);
        bool Update(int id, dbModel.User model);
        string Role();
        Task<dbModel.User> FindByCredentials(string username, string password);
    }
}