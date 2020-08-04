using System.Collections.Generic;

namespace oa.services.Schedule
{
    public interface IScheduleService
    {
        bool Update(db.Model.Schedule schedule);
        bool Create(int userId, db.Model.Schedule schedule);
        db.Model.Schedule GetSchedule(int id);
        List<db.Model.Schedule> GetScheduleUser(int userId);
        List<db.Model.Schedule> GetSchedulePerson(int personId);
        List<db.Model.Schedule> GetScheduleBusiness(int businessId);
    }
}