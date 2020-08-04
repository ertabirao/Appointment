using oa.db.Model;
using System.Data.Entity;
using System.Threading.Tasks;

namespace oa.db.Context
{
    public interface IOAContext
    {
        DbSet<Business> Business { get; set; }
        DbSet<Personnel> Personnel { get; set; }
        DbSet<Service> Service { get; set; }
        DbSet<User> User { get; set; }
        DbSet<Schedule> Schedule { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
        void SetModified(object entity);
    }
}