using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dbModel = oa.db.Model;

namespace oa.models.Schedule
{
    public class ScheduleListModel
    {

        public ScheduleListModel()
        {

        }
        public ScheduleListModel(dbModel.Schedule schedule)
        {

            Id = schedule.Id;
            Business = schedule.Service.Business.Name;
            Service = schedule.Service.Name;
            Price = schedule.Service.Price.ToString();
            DateTime = schedule.DateTime;
            AssignedPerson = schedule.Personnel == null ? "Not yet assigned" : schedule.Personnel.Name;
            Status = schedule.Status == null ? "" : schedule.Status;

        }
        public int Id { get; set; }
        public string  Business { get; set; }
        public string AssignedPerson { get; set; }
        public string Service { get; set; }
        public string Price { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }

    }
}
