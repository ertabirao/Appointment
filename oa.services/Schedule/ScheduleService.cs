using oa.db.Context;
using dbModel = oa.db.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oa.services.Schedule
{
    public class ScheduleService : IScheduleService
    {
        private readonly IOAContext _db;
        public ScheduleService(IOAContext db)
        {
            _db = db;
        }

        public bool Create(int userId, dbModel.Schedule schedule)
        {
            schedule.UserId = userId;
            _db.Schedule.Add(schedule);
            return _db.SaveChanges() > 0 ;
        }

        public bool Update(dbModel.Schedule schedule)
        {
            _db.Schedule.Add(schedule);
            return _db.SaveChanges() > 0;
        }

        public List<dbModel.Schedule> GetScheduleUser(int userId)
        {
            return _db.Schedule
                .Where(x => x.UserId == userId)
                .ToList();
        }

        public dbModel.Schedule GetSchedule( int id)
        {
            return _db.Schedule
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public List<dbModel.Schedule> GetSchedulePerson(int personnelId)
        {

            return _db.Schedule
                .Where(x => x.PersonnelId == personnelId)
                .ToList();
        }
        public List<dbModel.Schedule> GetScheduleBusiness(int businessId)
        {
            return _db.Schedule
                .Where(x => x.Service.BusinessId == businessId)
                .ToList();
        }
    }
}
