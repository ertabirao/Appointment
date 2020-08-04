using oa.models;
using oa.models.Schedule;
using oa.models.User;
using oa.services.Personnel;
using oa.services.Schedule;
using oa.services.Service;
using oa.services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace oa.api.Controllers
{
  
    [Authorize(Roles = "User")]
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;
        private readonly IPersonnelService _personnelService;
        private readonly IScheduleService _scheduleService;
        private readonly IServicesService _servicesService;

        public UserController(IUserService userService
            , IPersonnelService personnelService
            , IScheduleService scheduleService
            , IServicesService servicesService)
        {
            _userService = userService;
            _personnelService = personnelService;
            _scheduleService = scheduleService;
            _servicesService = servicesService;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResponseModel Post([FromBody] UserModel userModel)
        {
            ActionResponseModel response = new ActionResponseModel();
            response.Error = new List<string>();
            try
            {

                if (_userService.GetByEmail(userModel.Email) != null 
                    || _personnelService.GetByEmail(userModel.Email) != null)
                    response.Error.Add("Email address already used");

                if (response.Error.Count > 0)
                {
                    response.Success = false;
                }
                else
                {
                    _userService.Create(userModel.ToDBModel());
                    response.Success = true;
                }
       
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error.Add(ex.Message);
            }
            return response;



        }

        [HttpPut]
        public ActionResponseModel Put([FromBody] UserModel userModel)
        {
            ActionResponseModel response = new ActionResponseModel();
            response.Error = new List<string>();

            try
            {
                var user = _userService.GetByEmail(User.Identity.Name);
        
                _userService.Update(user.Id, userModel.ToDBModel());
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error.Add(ex.Message);
            }

            return response;

        }

        [HttpGet]
        public ActionResponseModel Get()
        {
            ActionResponseModel response = new ActionResponseModel();
            response.Error = new List<string>();
            try
            {
                var user = _userService.GetByEmail(User.Identity.Name);
                if (user == null)
                    response.Result = null;
                else
                    response.Result = new UserModel(user);

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error.Add(ex.Message);
            }

            return response;

        }


        [Route("schedule")]
        [HttpPost]
        public ActionResponseModel PostSchedule([FromBody] ScheduleModel scheduleModel)
        {
            ActionResponseModel response = new ActionResponseModel();
            response.Error = new List<string>();

            try
            {
                var emailAddress = User.Identity.Name;
                var user = _userService.GetByEmail(emailAddress);

                if (_servicesService.Get(scheduleModel.ServiceId) == null)
                    response.Error.Add("Service not found");

                if (scheduleModel.DateTime < DateTime.Now)
                    response.Error.Add("Schedule date is invalid");

                if (response.Error.Count() > 0)
                {
                    response.Success = false;
                    response.Result = null;
                }
                else
                {
                    _scheduleService.Create(user.Id, scheduleModel.ToDBModel());
                    response.Success = true;
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error.Add(ex.Message);
            }
            return response;

        }

        [Route("schedule")]
        [HttpGet]
        public ActionResponseModel GetSchedules()
        {
            ActionResponseModel response = new ActionResponseModel();
            response.Error = new List<string>();

            try
            {
                var emailAddress = User.Identity.Name;
                var user = _userService.GetByEmail(emailAddress);

                if (response.Error.Count() > 0)
                {
                    response.Success = false;
                    response.Result = null;
                }
                else
                {
                    List<ScheduleListModel> result = _scheduleService.GetScheduleUser(user.Id)
                      .Select(model => new ScheduleListModel(model)).ToList();

                    response.Success = true;
                    response.Result = result;
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error.Add(ex.Message);
            }
            return response;

        }

        [HttpGet]
        [Route("schedule/{id}")]
        public ActionResponseModel GetSchedule(int id)
        {
            ActionResponseModel response = new ActionResponseModel();
            response.Error = new List<string>();

            try
            {
                var emailAddress = User.Identity.Name;
                var user = _userService.GetByEmail(emailAddress);

                if (_scheduleService.GetSchedule(id).UserId == user.Id)
                    response.Error.Add("Unable to access this schedule");

                if (response.Error.Count() > 0)
                {
                    response.Success = false;
                    response.Result = null;
                }
                else
                {
                    var schedule = _scheduleService.GetSchedule(id);
                    ScheduleModel result = new ScheduleModel(schedule);
                    response.Success = true;
                    response.Result = result;
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error.Add(ex.Message);
            }
            return response;

        }

        [HttpPut]
        [Route("schedule/{id}")]
        public ActionResponseModel PutSchedule(int id, ScheduleModel scheduleModel)
        {
            ActionResponseModel response = new ActionResponseModel();
            response.Error = new List<string>();

            try
            {
                var emailAddress = User.Identity.Name;
                var user = _userService.GetByEmail(emailAddress);

                if (response.Error.Count() > 0)
                {
                    response.Success = false;
                    response.Result = null;
                }
                else
                {
                    var schedule = _scheduleService.GetSchedule(id);
                    _scheduleService.Update(scheduleModel.ToUpdateDBModel(schedule));
                    response.Success = true;

                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error.Add(ex.Message);
            }
            return response;

        }

    }
}