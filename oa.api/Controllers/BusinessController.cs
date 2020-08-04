using oa.db.Model;
using oa.models;
using oa.models.Business;
using oa.models.Personnel;
using oa.models.Service;
using oa.services.Business;
using oa.services.Personnel;
using oa.services.Service;
using oa.services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Security;

namespace oa.api.Controllers
{
  
    [RoutePrefix("api/business")]
    public class BusinessController : ApiController
    {
        private readonly IBusinessService _businessService;
        private readonly IPersonnelService _personnelService;
        private readonly IServicesService _servicesService;
        private readonly IUserService _userService;

        public BusinessController(IBusinessService businessService
            , IPersonnelService personnelService
            , IServicesService servicesService
            , IUserService userService)
        {
            _businessService = businessService;
            _personnelService = personnelService;
            _servicesService = servicesService;
            _userService = userService;
        }


        [HttpGet]
        [Route("{id}")]
        public ActionResponseModel Get(int id)
        {
            ActionResponseModel response = new ActionResponseModel();
            response.Error = new List<string>();
            try
            {
                BusinessModel businessListModel;
                var business = _businessService.Get(id);
                if (business == null)
                    response.Error.Add("Business not found");

                if (response.Error.Count() > 0)
                {
                    response.Success = false;
                    response.Result = null;
                }
                else
                {
                    businessListModel = new BusinessModel(business);

                    response.Success = true;
                    response.Result = businessListModel;
                }

                return response;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error.Add(ex.Message);
                
            }
             return response;
         
        }

        [HttpGet]
        [Route("{id}/services")]
        public ActionResponseModel GetServices(int id)
        {
            ActionResponseModel response = new ActionResponseModel();
            response.Error = new List<string>();
            try
            {
            
                var business = _businessService.Get(id);
                if (business == null)
                    response.Error.Add("Business not found");

                if (response.Error.Count() > 0)
                {
                    response.Success = false;
                    response.Result = null;
                }
                else
                {
                    List<ServiceListModel> result = _servicesService.GetServices(id)
                        .Select(x => new ServiceListModel(x)).ToList();


                    response.Success = true;
                    response.Result = result;
                }

                return response;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error.Add(ex.Message);

            }
            return response;
      
        }

        [HttpGet]
        [Route("{id}/personnels")]
        public ActionResponseModel GetPersonnel(int id)
        {
            ActionResponseModel response = new ActionResponseModel();
            response.Error = new List<string>();
            try
            {

                var business = _businessService.Get(id);
                if (business == null)
                    response.Error.Add("Business not found");

                if (response.Error.Count() > 0)
                {
                    response.Success = false;
                    response.Result = null;
                }
                else
                {
                    List<PersonnelListModel> result = _personnelService.GetAll(id)
                        .Select(x => new PersonnelListModel(x)).ToList();


                    response.Success = true;
                    response.Result = result;
                }

                return response;

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
                List<BusinessModel> result = _businessService.GetAll()
                 .Select(x => new BusinessModel(x)).ToList();
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

        /// ADMINSIDE
        [HttpPost]
        public ActionResponseModel Post([FromBody] BusinessRegisterModel registerBusiness)
        {
            ActionResponseModel response = new ActionResponseModel();
            response.Error = new List<string>();
            try
            {
                if (_personnelService.GetByEmail(registerBusiness.PersonnelEmail) != null
                    || _userService.GetByEmail(registerBusiness.PersonnelEmail) != null)
                    response.Error.Add("Email already used");

                if (_businessService.FindByName(registerBusiness.Name) != null)
                    response.Error.Add("Business name already used");


                if (response.Error.Count > 0)
                {
                    response.Success = false;
                }
                else
                {
                    //Register business
                    var businessId = _businessService.Create(registerBusiness.ToBusinessDBModel());
                    //business admin add
                    _personnelService.Create(businessId, registerBusiness.ToPersonnelDBModel());
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResponseModel Get()
        {
            ActionResponseModel response = new ActionResponseModel();
            response.Error = new List<string>();
            try
            {
                var user = _personnelService.GetByEmail(User.Identity.Name);
                BusinessModel businessListModel;
                var business = _businessService.Get(user.BusinessId);
                if (business == null)
                    response.Error.Add("Business not found");

                if (response.Error.Count() > 0)
                {
                    response.Success = false;
                    response.Result = null;
                }
                else
                {
                    businessListModel = new BusinessModel(business);

                    response.Success = true;
                    response.Result = businessListModel;
                }

                return response;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error.Add(ex.Message);
            }

            return response;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public ActionResponseModel Put([FromBody] BusinessModel businessListModel)
        {
            ActionResponseModel response = new ActionResponseModel();
            response.Error = new List<string>();
            try
            {
                var user = _personnelService.GetByEmail(User.Identity.Name);
                var business = _businessService.FindByName(businessListModel.Name);
                if (business != null && business.Id != user.BusinessId)
                    response.Error.Add("Business name already used");
               


                if (response.Error.Count > 0)
                {
                    response.Success = false;
                }
                else
                {
                    _businessService.Update(user.BusinessId, businessListModel.ToDBModel());
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
