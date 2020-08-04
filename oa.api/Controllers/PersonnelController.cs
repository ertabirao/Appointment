using oa.models;
using oa.models.Personnel;
using oa.services.Personnel;
using oa.services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace oa.api.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/personnel")]
    public class PersonnelController : ApiController
    {
        private readonly IPersonnelService _personnelService;
        private readonly IUserService _userService;

        public PersonnelController(IPersonnelService personnelService
            , IUserService userService)
        {
            _personnelService = personnelService;
            _userService = userService;
        }

        
        [HttpPost]
        public ActionResponseModel Post([FromBody] PersonnelModel personnelModel)
        {
            ActionResponseModel response = new ActionResponseModel();
            response.Error = new List<string>();

            try
            {
                if (_personnelService.GetByEmail(personnelModel.Email) != null 
                    || _userService.GetByEmail(personnelModel.Email) != null)
                    response.Error.Add("Email already used");

                if (response.Error.Count > 0)
                {
                    response.Success = false;
                }
                else
                {
                    var businessId = _personnelService.GetByEmail(User.Identity.Name).BusinessId;
                    _personnelService.Create(businessId, personnelModel.ToDBModel());
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
        public ActionResponseModel Put([FromBody] PersonnelModel personnelModel)
        {
            ActionResponseModel response = new ActionResponseModel();
            response.Error = new List<string>();

            try
            {

                if (personnelModel.Email != null
                    && personnelModel.Email != User.Identity.Name)
                    response.Error.Add("Email address is not editable");

                if (response.Error.Count > 0)
                {
                    response.Success = false;
                }
                else
                {
                    var personnel = _personnelService.GetByEmail(User.Identity.Name);
                    _personnelService.Update(personnelModel.ToUpdateDBModel(personnel));
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
      
        [HttpGet]
        public ActionResponseModel Get()
        {
            ActionResponseModel response = new ActionResponseModel();
            response.Error = new List<string>();

            try
            {
                PersonnelListModel personnelModel;
                var personnel = _personnelService.GetByEmail(User.Identity.Name);
                if (personnel == null)
                    personnelModel = null;
                else
                    personnelModel = new PersonnelListModel(personnel);

                response.Success = true;
                response.Result = personnelModel;
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
        public ActionResponseModel GetAll()
        {
            ActionResponseModel response = new ActionResponseModel();
            response.Error = new List<string>();

            try
            {
                var personnel = _personnelService.GetByEmail(User.Identity.Name);
                List<PersonnelListModel> result = _personnelService.GetAll(personnel.BusinessId)
                   .Select(x => new PersonnelListModel(x)).ToList();
                
                response.Success = true;
                response.Result = result;
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
