using oa.models.Personnel;
using oa.models.Service;
using oa.models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dbModel = oa.db.Model;

namespace oa.models.Schedule
{
    public class ScheduleModel
    {
        public ScheduleModel()
        {

        }
        public ScheduleModel(dbModel.Schedule model)
        {
            Id = model.Id;
            User = new UserListModel(model.User); 
            Personnel = model.Personnel == null  ? null : new PersonnelListModel(model.Personnel);
            ServiceId = model.ServiceId;
            Service = new ServiceModel(model.Service);
            DateTime = model.DateTime;
            Note = model.Note;
            Status = model.Status;
            Comment = model.Comment;
        }

        public dbModel.Schedule ToDBModel()
        {
            dbModel.Schedule model = new dbModel.Schedule()
            {
                ServiceId = ServiceId,
                Note = Note,
                DateTime = DateTime,
                Status = "New",
            };
            return model;
        }

            public dbModel.Schedule ToUpdateDBModel(dbModel.Schedule schedule)
            {
            dbModel.Schedule model = new dbModel.Schedule()
            {
                Id = schedule.Id,
                UserId = schedule.UserId,
                PersonnelId = PersonnelId == null ? schedule.PersonnelId : PersonnelId,
                ServiceId = schedule.ServiceId,
                Note = Note == null ? schedule.Note : Note,
                DateTime = DateTime,
                Status = Status == "Done" || schedule.Status == "Done" ? schedule.Status : "New",
                Comment = Comment == null ? schedule.Comment : Comment,
            };
                return model;
            }

        public int Id { get; set; }
        public UserListModel User { get; set; }

        public int? PersonnelId { get; set; }
        public PersonnelListModel Personnel { get; set; }
        public int ServiceId { get; set; }
        public ServiceModel Service { get; set; }
        public DateTime DateTime { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
    }
}
