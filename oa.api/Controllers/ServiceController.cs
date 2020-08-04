using oa.models;
using oa.models.Service;
using oa.services.Personnel;
using oa.services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace oa.api.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/service")]
    public class ServiceController : ApiController
    {
        private readonly IServicesService _servicesService;
        private readonly IPersonnelService _personnel;

        public ServiceController(IServicesService servicesService, IPersonnelService personnel)
        {
            _servicesService = servicesService;
            _personnel = personnel;
        }

        [HttpPost]
        public ActionResponseModel Post([FromBody] ServiceModel service)
        {
            ActionResponseModel result = new ActionResponseModel();
            result.Error = new List<string>();
            try
            { 
                var businessId = _personnel.GetByEmail(User.Identity.Name).BusinessId;
                _servicesService.Create(businessId ,service.ToDbModel());

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Error.Add(ex.Message);
                result.Success = false;
            }
            return result;
        }

        [Route("list")]
        [HttpGet]
        public ActionResponseModel Get()
        {

            ActionResponseModel response = new ActionResponseModel();
            response.Error = new List<string>();
            try
            {
                var businessId = _personnel.GetByEmail(User.Identity.Name).BusinessId;
                List<ServiceListModel> result = _servicesService.GetServices(businessId)
                   .Select(x => new ServiceListModel(x)).ToList();

                response.Result = result;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Error.Add(ex.Message);
                response.Success = false;
            }
            return response;
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResponseModel Get(int id)
        {
            ActionResponseModel response = new ActionResponseModel();
            response.Error = new List<string>();

            try
            {
                ServiceListModel result;
                var businessId = _personnel.GetByEmail(User.Identity.Name).BusinessId;
                var service = _servicesService.Get(id);

                if (businessId != service.BusinessId)
                    response.Error.Add("Service not found");


                if (response.Error.Count() > 0)
                {
                    response.Success = false;
                    response.Result = null;
                }
                else
                {
                    result = new ServiceListModel(service);
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

        [Route("{id}")]
        [HttpPut]
        public ActionResponseModel Put(int id, [FromBody]  ServiceModel serviceListModel)
        {
            ActionResponseModel response = new ActionResponseModel();
            response.Error = new List<string>();

            try
            {
                var businessId = _personnel.GetByEmail(User.Identity.Name).BusinessId;
                var service = _servicesService.Get(id);

                if (businessId != service.BusinessId)
                    response.Error.Add("Service not found");

                if (response.Error.Count() > 0)
                {
                    response.Success = false;
                    response.Result = null;
                }
                else
                {
                    _servicesService.Update(id, businessId, serviceListModel.ToDbModel());
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
