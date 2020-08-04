using oa.models;
using oa.models.Schedule;
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
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/schedule")]
    public class ScheduleController : ApiController
    {
        private readonly IScheduleService _scheduleService;
        private readonly IPersonnelService _personnelService;
        private readonly IUserService _userService;
        private readonly IServicesService _servicesService;

        public ScheduleController(IScheduleService scheduleService
            , IPersonnelService personnelService
            , IUserService userService
            , IServicesService servicesService)
        {
            _scheduleService = scheduleService;
            _personnelService = personnelService;
            _userService = userService;
            _servicesService = servicesService;
        }

        [Authorize(Roles = "Personnel")]
        [HttpGet]
        public ActionResponseModel Get()
        {
            ActionResponseModel response = new ActionResponseModel();
            response.Error = new List<string>();

            try
            {
                var emailAddress = User.Identity.Name;
                var personnel = _personnelService.GetByEmail(emailAddress);

                if (response.Error.Count() > 0)
                {
                    response.Success = false;
                    response.Result = null;
                }
                else
                {
                    List<ScheduleListModel> result = _scheduleService.GetSchedulePerson(personnel.Id)
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

        [Route("list")]
        [HttpGet]
        public ActionResponseModel GetSchedules()
        {
            ActionResponseModel response = new ActionResponseModel();
            response.Error = new List<string>();

            try
            {
                var emailAddress = User.Identity.Name;
                var user = _personnelService.GetByEmail(emailAddress);

                if (response.Error.Count() > 0)
                {
                    response.Success = false;
                    response.Result = null;
                }
                else
                {
                    List<ScheduleListModel> result = _scheduleService.GetScheduleBusiness(user.BusinessId)
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
    }
}
